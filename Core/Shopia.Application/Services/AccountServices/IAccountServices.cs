using Shopia.Application.DTO_s.AccountDTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopia.Application.Services.AccountServices
{
    public interface IAccountServices
    {
        Task<string> Login(LoginDTO DTO);
        Task<string> Register(RegisterDTO DTO);
        Task<string> ChangePassword();
        Task Logout();

    }
}
