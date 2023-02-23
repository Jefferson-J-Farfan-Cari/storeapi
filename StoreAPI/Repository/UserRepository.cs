using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using StoreAPI.Entities.Dto;
using StoreAPI.Entities.Interface;
using StoreAPI.Entities.Models;
using StoreAPI.Entities.Security;
using StoreAPI.Entities.Utils;
using StoreAPI.Infrastructure.Context;

namespace StoreAPI.Repository;

public class UserRepository: BaseRepository, IUserRepository
{
    public UserRepository(StoreDb context) : base(context)
    {
    }

    public bool Exists(long id)
    {
        return Context.User.Any(x => x.IdUser == id);
    }

    public UserDto FindById(long id)
    {
        try
        {
            var response = (from user in Context.User
                    where user.IdUser == id && user.LogState == (int) Constants.StatusRecord.ACTIVE
                    select new UserDto
                    {
                        IdUser = user.IdUser,
                        DocumentType = user.DocumentType,
                        Name = user.Name ?? string.Empty,
                        FatherLN = user.FatherLN ?? string.Empty,
                        MotherLN = user.MotherLN ?? string.Empty,
                        Document = user.Document ?? string.Empty,
                        Email = user.Email ?? string.Empty,
                        LogDateCrate = user.LogDateCrate,
                        LogDateModified = user.LogDateModified,
                        LogState = user.LogState
                    }
                ).FirstOrDefault();

            return response!;
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public UserDto Save(UserDto user)
    {
        try
        {
            if (!Exists(user.IdUser))
            {
                var newUser = new User
                {
                    IdUser = user.IdUser,
                    DocumentType = user.DocumentType,
                    Password = user.Password,
                    Name = user.Name,
                    FatherLN = user.FatherLN,
                    MotherLN = user.MotherLN,
                    Document = user.Document,
                    Email = user.Email,
                    LogDateCrate = DateTime.Now,
                    LogDateModified = DateTime.Now,
                    LogState = user.LogState
                };
                Context.Add(newUser);
            }
            else
            {
                var myUser = Context.User.FirstOrDefault(x => x.IdUser == user.IdUser && x.LogState == (int) 
                    Constants.StatusRecord.ACTIVE) ?? throw new Exception("User not Found.");
                myUser.Email = user.Email;
                myUser.Document = user.Document;
                myUser.Name = user.Name;
                myUser.FatherLN = user.FatherLN;
                myUser.MotherLN = user.MotherLN;
                myUser.LogState = user.LogState;
            }

            Context.SaveChanges();
            return user;
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public TokenCredentials Login(UserDto userDto)
    {
        try
        {
            var user = Context.User.FirstOrDefault(
                           x => 
                               x.Email == userDto.Email &&
                               x.Password == userDto.Password
                           ) ?? 
                       throw new Exception("Invalid User");
            var credentials = new UserCredentials
            {
                IdUser = user.IdUser,
                Email = user.Email,
            };
            return GenerateAccessTokens(credentials);
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    #region Metodos Privados

    protected virtual TokenCredentials GenerateAccessTokens(UserCredentials user)
    {
        try
        {
            var securityKey = SecurityStoreApi.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            const int tiempoNoAntesMin = 4; 
            const int tiempoExpiraSec = 24  * 60 * 60;


            var current = DateTimeOffset.UtcNow.UtcDateTime;

            var expiry = current.AddHours(24 );
            var notBefore = current.AddMinutes(tiempoNoAntesMin);
            var payload = new JwtPayload("stores", null, new List<Claim>(), notBefore, expiry)
            {
                {"scopes", user}
            };
            var handler = new JwtSecurityTokenHandler();
            var header = new JwtHeader(credentials);
            var secToken = new JwtSecurityToken(header, payload);
            return new TokenCredentials
            {
                AccessToken = handler.WriteToken(secToken),
                RefreshToken = "",
                ExpiresIn = tiempoExpiraSec,
                RequestAt = current,
                StateCode = (int) HttpStatusCode.Created
            };
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    #endregion
}