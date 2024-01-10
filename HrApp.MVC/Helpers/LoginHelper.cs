using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Tokens;

namespace HrApp.MVC;

public class LoginHelper
{
    /// <summary>
    /// Verilen JWT token'ını doğrular ve geçerli ise HttpContext üzerinde kullanıcı girişi yapar.
    /// </summary>
    /// <param name="token">Doğrulanacak JWT token'ı.</param>
    /// <param name="httpContext">HttpContext nesnesi, kullanıcı oturum bilgilerini saklamak için kullanılır.</param>
    /// <returns>Token geçerli ise true, aksi takdirde false döner.</returns>
    public static async Task<bool> LoginAsync(string token, HttpContext httpContext)
    {
        var claims = IsTokenValid(token);
        if (claims == null)
        {
            return false;
        }
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties
        {
            AllowRefresh = true,
            IsPersistent = true,
        };
        await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
        return true;
    }

    /// <summary>
    /// Kullanıcının mevcut oturumunu sonlandırır.
    /// </summary>
    /// <param name="httpContext">HttpContext nesnesi, kullanıcının oturumunu sonlandırmak için kullanılır.</param>
    public static async Task LogoutAsync(HttpContext httpContext)
    {
        await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }

    /// <summary>
    /// Verilen JWT token'ını doğrular ve token içindeki claim'leri döndürür.
    /// </summary>
    /// <param name="token">Doğrulanacak JWT token'ı.</param>
    /// <returns>Token geçerliyse içindeki claim'lerin listesini, aksi takdirde null döner.</returns>
    private static List<Claim> IsTokenValid(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(GlobalOptions.TokenOptions.TokenHashKey);

        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateActor = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            return jwtToken.Claims.ToList();
        }
        catch
        {
            return null;
        }
    }
}
