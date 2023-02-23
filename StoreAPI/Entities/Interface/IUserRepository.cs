using StoreAPI.Entities.Dto;
using StoreAPI.Entities.Security;

namespace StoreAPI.Entities.Interface;

public interface IUserRepository : IBaseRepository<UserDto, long>
{
    TokenCredentials Login(UserDto userDto);
}