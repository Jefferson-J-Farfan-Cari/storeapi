using System.Runtime.Serialization.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace StoreAPI.Entities.Security;

public class CheckToken : ActionFilterAttribute
{
    /*public override void OnActionExecuting(ActionExecutingContext actionContext)
    {
        try
        {
            if (!actionContext.HttpContext.Request.Headers.ContainsKey("Authorization"))
            {
                actionContext.Result = new StatusCodeResult(403);
            }
            else
            {
                var strArray = ((string) actionContext.HttpContext.Request.Headers["Authorization"]).Split(' ');
                if (strArray.Length < 2)
                {
                    actionContext.Result = new StatusCodeResult(403);
                }
                else
                {
                    var token = strArray[1];
                    if (string.IsNullOrEmpty(token))
                        throw new ArgumentException("Given token is null or empty.");

                    var tokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        IssuerSigningKey = SeguridadFarmacia.GetSymmetricSecurityKey()
                    };

                    var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
                    var validatedToken = (SecurityToken) null;

                    var tokenValid = jwtSecurityTokenHandler.ValidateToken(token, tokenValidationParameters, out  validatedToken);
                    var values = jwtSecurityTokenHandler.ReadJwtToken(token).Payload.Values;
                    var usuarioCredentials = Deserialize<UsuarioCredentials>(values.ElementAt(3).ToString());

                    actionContext.HttpContext.Items.Add("userInfo", usuarioCredentials);
                }
            }
        }
        catch (SecurityTokenExpiredException ex)
        {
            actionContext.Result = new UnauthorizedObjectResult(new
            {
                IdError = 403,
                Mensaje = "Token Expirado",
                Traza = ex.StackTrace
            });
        }
        catch (SecurityTokenInvalidSignatureException ex)
        {
            actionContext.Result = new UnauthorizedObjectResult(new
            {
                IdError = 403,
                Mensaje = "Token Invalido",
                Traza = ex.StackTrace
            });
        }
    }

    private static T Deserialize<T>(string json)
    {
        var instance = Activator.CreateInstance<T>();
        var memoryStream = new MemoryStream(Encoding.Unicode.GetBytes(json));
        var obj = (T) new DataContractJsonSerializer(instance.GetType()).ReadObject(memoryStream);
        memoryStream.Close();
        return obj;
    }*/
}