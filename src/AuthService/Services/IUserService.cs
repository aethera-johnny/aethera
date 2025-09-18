using AuthService.Common;
using AuthService.Entities;
using System.Threading.Tasks;

namespace AuthService.Services
{
    public interface IUserService
    {
        Task<Result<Tb_User_Info>> AddUserAsync(Tb_User_Info newUser);
    }
}
