using Shopia.Application.DTO_s.AccountDTO_s;
using Shopia.Application.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopia.Application.Services.AccountServices
{
    public class AccountServices : IAccountServices
    {
        private readonly IUserIdentityRepository _userIdentityRepository;

        public AccountServices(IUserIdentityRepository userIdentityRepository )
        {
            _userIdentityRepository = userIdentityRepository;
        }

        public Task<string> ChangePassword()
        {
            throw new NotImplementedException();
        }

        public async Task<string> Login(LoginDTO dto)
        {
            var result = await _userIdentityRepository.LoginAsync(dto);
            return result;  
        }

        public async Task Logout()
        {
            await _userIdentityRepository.LogoutAsync();
        }

        public async Task<string> Register(RegisterDTO dto)
        {
            var result = await _userIdentityRepository.RegisterAsync(dto);
            return result;   
        }
    }
}
