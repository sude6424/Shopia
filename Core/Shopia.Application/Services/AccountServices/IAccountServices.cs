using Shopia.Application.DTO_s.AccountDTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Shopia.Application.Services.AccountServices
{
    public interface IAccountServices
    {
        Task<string> Login(LoginDTO DTO);
        Task<string> Register(RegisterDTO DTO);
        Task<string> ChangePassword(ChangePasswordDTO dto);
        Task Logout();
        Task<bool> UpdateUser(string userId, string name, string surname);
        Task<string> GetUserIdAsync(ClaimsPrincipal user);
    }
}
