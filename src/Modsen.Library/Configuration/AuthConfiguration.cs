using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Modsen.Library.AuthConfiguration;

public static class AuthConfiguration
{
    public static string Issuer { get; } = "modsen-library";
    public static string Audience { get; } = "library-user";
    public static string Key { get; } = "TopSecretTokenTopSecretToken1234567890";
    private static string StaticKey { get; } = "TopSecretTokenTopSecretToken1234567890";
    
    public static SymmetricSecurityKey GetSymmetricSecurityKeyStatic()
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(StaticKey));
    }

    public static DateTime Expires { get; set; } = DateTime.Now.AddDays(1);
}