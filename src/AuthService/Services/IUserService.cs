using AuthService.Entities;
using AuthService.Models;

namespace AuthService.Services
{
    public interface IUserService
    {
        Task<AuthResponse> AddUserAsync(Tb_User_Info newUser);
        Task<AuthResponse> AuthenticateAsync(string userAccount, string userPassword);
        Task<Tb_User_Info> GetUserByRefreshTokenAsync(string refreshToken);
    }
}
