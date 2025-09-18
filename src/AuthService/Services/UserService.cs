using AuthService.Common;
using AuthService.Contexts;
using AuthService.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Services
{
    public class UserService : IUserService
    {
        private readonly AetheraDbContext aethera;
        public UserService(AetheraDbContext context)
        {
            aethera = context;
        }

        public async Task<Result<Tb_User_Info>> AddUserAsync(Tb_User_Info newUser)
        {
            const int SALT_SIZE = 16;
            const int ITERATIONS = 100000;
            const int OUTPUT_SIZE = 32;

            try
            {
                byte[] salt = RandomNumberGenerator.GetBytes(SALT_SIZE);
                byte[] hash = Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(newUser.UserPassword), salt, ITERATIONS, HashAlgorithmName.SHA256, OUTPUT_SIZE);

                newUser.UserPassword = Convert.ToHexString(hash);
                newUser.PasswordSalt = salt;
                newUser.CreatedDatetime = DateTime.UtcNow;
                newUser.UpdatedDatetime = DateTime.UtcNow;

                await aethera.AddAsync(newUser);
                return await aethera.SaveChangesAsync() switch
                {
                    1 => Result<Tb_User_Info>.Success(newUser),
                    _ => Result<Tb_User_Info>.Failure("ERROR")
                };
            }
            catch (DbUpdateException ex)
            {
                return Result<Tb_User_Info>.Failure(ex.Message);
            }
        }
    }
}
