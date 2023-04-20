using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Framework.Application
{
    public class AuthHelper : IAuthHelper
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public AuthHelper(IHttpContextAccessor contextAccessor) => _contextAccessor = contextAccessor;

        public AuthViewModel CurrentAccount
        {
            get
            {
                var result = new AuthViewModel();
                if (!IsAuthenticated()) return result;
                var claims = _contextAccessor.HttpContext.User.Claims.ToList();
                result.Id = long.Parse(claims.FirstOrDefault(c => c.Type == "AccountId").Value);
                result.Username = claims.FirstOrDefault(c => c.Type == "Username").Value;
                result.RoleId = long.Parse(claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value);
                result.Fullname = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
                result.Role = claims.FirstOrDefault(c => c.Type == ClaimTypes.Actor).Value;
                result.ProfilePhoto = claims.FirstOrDefault(c => c.Type == "ProfilePhoto").Value;
                return result;
            }
        }


        public string CurrentAccountRole()
        {
            if (IsAuthenticated())
                return _contextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
            return null;
        }


        public bool IsAuthenticated() => _contextAccessor.HttpContext.User.Claims.ToList().Count > 0;

        public void SignIn(AuthViewModel account)
        {
            var claims = new List<Claim>
            {
                new Claim("AccountId", account.Id.ToString()),
                new Claim(ClaimTypes.Name, account.Fullname),
                new Claim(ClaimTypes.Role, account.RoleId.ToString()),
                new Claim(ClaimTypes.Actor, account.Role),
                new Claim("Username", account.Username),
                new Claim("ProfilePhoto", account.ProfilePhoto)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties { ExpiresUtc = DateTime.UtcNow.AddDays(1) };
            _contextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
        }

        public void SignOut() => _contextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }
}
