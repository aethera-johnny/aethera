using AuthService.Entities;
using System.Threading.Tasks;

namespace AuthService.Services
{
    public interface ITokenService
    {
        string GenerateJwtToken(Tb_User_Info user);
        string GenerateRefreshToken();
        Task SaveRefreshTokenAsync(int userId, string refreshToken);
        Task<string> RefreshAccessTokenAsync(string refreshToken);
    }
}