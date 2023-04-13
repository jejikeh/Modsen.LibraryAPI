using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Modsen.Library.Configuration;

public static class AuthConfiguration
{
    public static string Issuer { get; } = "modsen-library";
    public static string Audience { get; } = "library-user";
    private static string StaticKey { get; } = "TopSecretTokenTopSecretToken1234567890";
    public static DateTime Expires { get; set; } = DateTime.Now.AddDays(1);
    
    public static SymmetricSecurityKey GetSymmetricSecurityKeyStatic()
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(StaticKey));
    }
}