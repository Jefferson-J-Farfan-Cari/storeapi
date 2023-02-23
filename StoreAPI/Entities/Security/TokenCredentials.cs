namespace StoreAPI.Entities.Security;

public class TokenCredentials
{
    public int StateCode { get; set; }
    public DateTime RequestAt { get; set; }

    public int ExpiresIn { get; set; }

    public string AccessToken { get; set; }

    public string RefreshToken { get; set; }
}