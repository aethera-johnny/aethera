using AuthService.Contexts;
using AuthService.Entities;
using AuthService.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace AuthService.Services
{
    public class UserService : IUserService
    {
        private readonly AetheraDbContext aethera;
        private readonly ITokenService _tokenService;

        private const int SALT_SIZE = 16;
        private const int ITERATIONS = 100000;
        private const int OUTPUT_SIZE = 32;

        public UserService(AetheraDbContext context, ITokenService tokenService)
        {
            aethera = context;
            _tokenService = tokenService;
        }

        public async Task<AuthResponse> AddUserAsync(Tb_User_Info newUser)
        {
            try
            {
                var existingUser = await aethera.Users.FirstOrDefaultAsync(u => u.UserAccount == newUser.UserAccount);
                if (existingUser != null)
                {
                    return null;
                }

                byte[] salt = RandomNumberGenerator.GetBytes(SALT_SIZE);
                byte[] hash = Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(newUser.UserPassword), salt, ITERATIONS, HashAlgorithmName.SHA256, OUTPUT_SIZE);

                newUser.UserPassword = Convert.ToHexString(hash);
                newUser.PasswordSalt = salt;
                newUser.CreatedDatetime = DateTime.UtcNow;
                newUser.UpdatedDatetime = DateTime.UtcNow;

                var jwtToken = _tokenService.GenerateJwtToken(newUser);
                var refreshToken = _tokenService.GenerateRefreshToken();

                newUser.RefreshToken = refreshToken;
                newUser.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

                await aethera.Users.AddAsync(newUser);
                int changes = await aethera.SaveChangesAsync();

                if (changes == 0)
                {
                    return null;
                }

                return new AuthResponse
                {
                    UserId = newUser.User_Id,
                    UserAccount = newUser.UserAccount,
                    UserName = newUser.UserName,
                    UserEmail = newUser.UserEmail,
                    AccessToken = jwtToken,
                    RefreshToken = refreshToken,
                    RefreshTokenExpiryTime = newUser.RefreshTokenExpiryTime
                };
            }
            catch (DbUpdateException ex)
            {
                Debug.WriteLine($"Database update error: {ex.Message}");
                return null;
            }
        }

        public async Task<AuthResponse> AuthenticateAsync(string userAccount, string userPassword)
        {
            var user = await aethera.Users.SingleOrDefaultAsync(u => u.UserAccount == userAccount);

            if (user == null)
            {
                return null;
            }

            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(userPassword),
                user.PasswordSalt,
                ITERATIONS,
                HashAlgorithmName.SHA256,
                OUTPUT_SIZE);

            if (Convert.ToHexString(hash) != user.UserPassword)
            {
                return null;
            }

            var jwtToken = _tokenService.GenerateJwtToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken();

            await _tokenService.SaveRefreshTokenAsync(user.User_Id, refreshToken);

            return new AuthResponse
            {
                UserId = user.User_Id,
                UserAccount = user.UserAccount,
                UserName = user.UserName,
                UserEmail = user.UserEmail,
                AccessToken = jwtToken,
                RefreshToken = refreshToken,
                RefreshTokenExpiryTime = user.RefreshTokenExpiryTime
            };
        }

        public async Task<Tb_User_Info> GetUserByRefreshTokenAsync(string refreshToken)
        {
            return await aethera.Users.SingleOrDefaultAsync(u => u.RefreshToken == refreshToken && u.RefreshTokenExpiryTime > DateTime.UtcNow);
        }
    }
}
