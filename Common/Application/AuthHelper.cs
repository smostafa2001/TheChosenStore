using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Common.Application;

public class AuthHelper(IHttpContextAccessor contextAccessor) : IAuthHelper
{
    public AuthViewModel CurrentAccount
    {
        get
        {
            var result = new AuthViewModel();
            if (!IsAuthenticated()) return result;
            var claims = contextAccessor.HttpContext?.User.Claims.ToList();
            result.Id = long.Parse(claims!.FirstOrDefault(c => c.Type == "AccountId")!.Value);
            result.Username = claims!.FirstOrDefault(c => c.Type == "Username")!.Value;
            result.RoleId = long.Parse(claims!.FirstOrDefault(c => c.Type == ClaimTypes.Role)!.Value);
            result.Fullname = claims!.FirstOrDefault(c => c.Type == ClaimTypes.Name)!.Value;
            result.Role = claims!.FirstOrDefault(c => c.Type == ClaimTypes.Actor)!.Value;
            result.ProfilePhoto = claims!.FirstOrDefault(c => c.Type == "ProfilePhoto")!.Value;
            return result;
        }
    }

    public long CurrentAccountId => CurrentAccount.Id;

    public string CurrentAccountRole() => IsAuthenticated() ? contextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value! : null!;

    public List<int> GetPermissions()
    {
        if (!IsAuthenticated()) return [];
        var permissions = contextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == "Permissions")?.Value;
        return JsonConvert.DeserializeObject<List<int>>(permissions!)!;
    }

    public bool IsAuthenticated() => contextAccessor.HttpContext!.User.Identity!.IsAuthenticated;

    public void SignIn(AuthViewModel account)
    {
        var permissions = JsonConvert.SerializeObject(account.Permissions);
        var claims = new List<Claim>
        {
            new("AccountId", account.Id.ToString()),
            new(ClaimTypes.Name, account.Fullname),
            new(ClaimTypes.Role, account.RoleId.ToString()),
            new(ClaimTypes.Actor, account.Role),
            new("Username", account.Username),
            new("ProfilePhoto", account.ProfilePhoto),
            new("Permissions", permissions)
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties { ExpiresUtc = DateTime.UtcNow.AddDays(1) };
        contextAccessor.HttpContext?.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
    }

    public void SignOut() => contextAccessor.HttpContext?.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
}
