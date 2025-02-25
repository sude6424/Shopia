using Microsoft.AspNetCore.Identity;
using Shopia.Application.DTO_s.AccountDTO_s;
using Shopia.Application.Interface;
using Shopia.DataAccess.Context.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Shopia.DataAccess.Repositories
{
    public class UserIdentityRepository : IUserIdentityRepository
    {
        private readonly UserManager<AppIdentityUser> _userManager;
        private readonly SignInManager<AppIdentityUser> _signInManager;

        public UserIdentityRepository(UserManager<AppIdentityUser> userManager, SignInManager<AppIdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public Task<string> ChangePasswordAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetUserIdOnAuth(ClaimsPrincipal user)
        {
            string userId = _userManager.GetUserId(user);
            return userId;
        }

        public Task<bool> IsUserAuthenticated()
        {
            throw new NotImplementedException();
        }

        public async Task<string> LoginAsync(LoginDTO dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                return "Kullanıcı Bulunamadı.";
            }
            var result = await _signInManager.PasswordSignInAsync(dto.Email, dto.Password, true, false);
            if (result.Succeeded)
            {
                return "Giriş Başarılı";
            }
            if (result.IsLockedOut)
            {
                return "Hesabınız Kilitlendi";
            }
            if (result.IsNotAllowed)
            {
                return "Hesabınız Aktif Değil";
            }
            if (result.RequiresTwoFactor)
            {
                return "İki Adımlı Doğrulama Gerekiyor";
            }
            return "Giriş Başarısız";
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<string> RegisterAsync(RegisterDTO dto)
        {
            if (dto.Password != dto.RePassword)
            {
                return "Şifreler Uyuşmuyor";
            }
            var user = new AppIdentityUser
            {
                Email = dto.Email,
                UserName = dto.Email,
                Name = dto.Name,
                Lastname = dto.Lastname,
                PhoneNumber = dto.PhoneNumber
            };
            var result = await _userManager.CreateAsync(user,dto.Password);
            if (result.Succeeded)
            {
                var result1 = await _signInManager.PasswordSignInAsync(dto.Email, dto.Password, true, false);
                if (result1.Succeeded)
                {
                    return user.Id;
                }
                return "üye olundu. Giriş yapılamadı";

            }
            else
            {
                return result.Errors.ToString();
            }
            
        }
    }
}
