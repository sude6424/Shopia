using Shopia.Application.DTO_s.AccountDTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Shopia.Application.Interface
{
    public interface IUserIdentityRepository
    {
        Task<string> LoginAsync(LoginDTO DTO);
        Task<string> RegisterAsync(RegisterDTO DTO);
        Task<string> ChangePasswordAsync();
        Task LogoutAsync();
        Task<bool> IsUserAuthenticated();
        Task<string> GetUserIdOnAuth(ClaimsPrincipal user);


    }
}
