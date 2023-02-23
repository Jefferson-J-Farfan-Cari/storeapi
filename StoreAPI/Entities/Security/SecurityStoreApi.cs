using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace StoreAPI.Entities.Security;

public class SecurityStoreApi
{
    private const string TokenPassword = "S3gUi49sPQh36klm";

    private const string Key =
        "MIIC3DCCAcSgAwIBAgIQYQChp1q02LlC+D08qDnW/zANBgkqhkiG9w0BAQsFADAXMRUwEwYDVQQDEwxTTVJBUVBTUlYwMDIwHhcNMjAxMj" +
        "A1MTUxNjI3WhcNMjExMjA1MDAwMDAwWjAXMRUwEwYDVQQDEwxTTVJBUVBTUlYwMDIwggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAwggEKAoIB" +
        "AQCtPVRtmVPQ6ohuk+pIGvRycFgs1shRkAoFPJdafLy1fyazjHF7UjnHI7Yavy39bhKxwFbaa0GicBqD/TxSGVGWbxAshYfD4qdlVs66y+" +
        "YdNkmK/9o4/nNQuC3IsLzO2tbJS7b0jadHJrYu+KQSDKfGvi++oWabe8H4JKPnvBVBfzHx+PlenGhdJGFCfKG4aFghikYynZKgLcVoYGpQ" +
        "eJ88fWLqwzh3xTDaLB1tQPeiMsC8YQmvOk0Hdimyg5lAFh0FtLZAcZl6vZknfrC8y0JKD6Aw1VrmtiMGSKiG5mN21bbG553uVtrl6u8/9t" +
        "MS/Dpd3SP3R/o/bm51Cc2v4MzBAgMBAAGjJDAiMAsGA1UdDwQEAwIEMDATBgNVHSUEDDAKBggrBgEFBQcDATANBgkqhkiG9w0BAQsFAAOC" +
        "AQEASNZnXY5rZAmP66hFZnqNFti7NA6eFKNjSF0poExFKn9wV8jEZtC2vqRaXpHc0ZuAN/iHt8A+iZzkwUxFRduZJW0YLBHVnSiYxy3Jxn" +
        "cTg8eSeT9quWqC+wzx3Mfs/TX+qduIOzgGpkRJy/m1xe0EpwujwKsITOnx5z7XrYcLhLicbRp5aS05E5SHSkOOXjvtDlQVcV4PCxAXhpCA" +
        "Wb9qgAf7HzfUJBs5hFVgcMdMMawxgbcBMW0maX2GnHDxVFathhI6b+YF5btY/tjxq7TVj6vjp60eWrTM17sxEIPaHRFefEptg3Aa58DlSf" +
        "kdJGalnjdJeh6qjp1YfofQ1x0quw==";
    
    public static SymmetricSecurityKey GetSymmetricSecurityKey() => new (Encoding.UTF8.GetBytes(Key));
    
    public static string Encrypt(string cadena)
    {
        var bytes = Encoding.UTF8.GetBytes(cadena);
        var cryptoServiceProvider1 = new MD5CryptoServiceProvider();
        var hash = cryptoServiceProvider1.ComputeHash(Encoding.UTF8.GetBytes(TokenPassword));
        cryptoServiceProvider1.Clear();
        var cryptoServiceProvider2 = new TripleDESCryptoServiceProvider
        {
            Key = hash, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7
        };
        var cryptoServiceProvider3 = cryptoServiceProvider2;
        var inArray = cryptoServiceProvider3.CreateEncryptor().TransformFinalBlock(bytes, 0, bytes.Length);
        cryptoServiceProvider3.Clear();
        return Convert.ToBase64String(inArray, 0, inArray.Length);
    }

    public static string DeEncrypt(string cadena)
    {
        var inputBuffer = Convert.FromBase64String(cadena);
        var cryptoServiceProvider1 = new MD5CryptoServiceProvider();
        var hash = cryptoServiceProvider1.ComputeHash(Encoding.UTF8.GetBytes(TokenPassword));
        cryptoServiceProvider1.Clear();
        var cryptoServiceProvider2 = new TripleDESCryptoServiceProvider {Key = hash, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7};
        var cryptoServiceProvider3 = cryptoServiceProvider2;
        var bytes = cryptoServiceProvider3.CreateDecryptor().TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
        cryptoServiceProvider3.Clear();
        return Encoding.UTF8.GetString(bytes);
    }
}