using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LibraryMgmt.EFCore.DataAccess;

public  class SecurityKeyGenerator
{
    public static SymmetricSecurityKey GetSymmetricSecurityKey(IConfiguration config)
    {
        byte[] keyBytes = Encoding.UTF8.GetBytes(config.GetSection("JWT:Key").Value);
        if (keyBytes.Length < 32)
        {
            var paddedKeyBytes = new byte[32];
            keyBytes.CopyTo(paddedKeyBytes, 0);
            keyBytes = paddedKeyBytes;
        }
        SymmetricSecurityKey symmetricKey = new SymmetricSecurityKey(keyBytes);
        return symmetricKey;
    }
}
