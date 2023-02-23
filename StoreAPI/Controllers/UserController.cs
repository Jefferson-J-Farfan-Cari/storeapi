using Microsoft.AspNetCore.Mvc;
using StoreAPI.Entities.Dto;
using StoreAPI.Entities.Interface;
using StoreAPI.Entities.Security;

namespace StoreAPI.Controllers;

[Route("api/v1/users")]
[ApiController]
public class UserController : ControllerBase
{
    
    private readonly IUserRepository _repository;
    private readonly IConfigurationRepository _configurationRepository;

    public UserController(IUserRepository repository, IConfigurationRepository configurationRepository)
    {
        _repository = repository;
        _configurationRepository = configurationRepository;
    }

    [HttpGet("")]
    [CheckToken]
    public ActionResult<CollectionResponse<UserDto>> ListUsers(int page = 0, int reg = 10)
    {
        try
        {
            return _configurationRepository.ListUsers(page, reg);
        }
        catch (Exception e)
        {
            return new BadRequestObjectResult(e);
        }
    }
    
    [HttpGet("by_id")]
    [CheckToken]
    public ActionResult<UserDto> FindById(long idUser)
    {
        try
        {
            return _repository.FindById(idUser);
        }
        catch (Exception ex)
        {
            return new BadRequestObjectResult(ex);
        }
    }
    
    [HttpPost("")]
    public ActionResult<UserDto> Save(UserDto user)
    {
        try
        {
            return _repository.Save(user);
        }
        catch (Exception ex)
        {
            return new BadRequestObjectResult(ex);
        }
    }
    
    [HttpPost("login")]
    public ActionResult<TokenCredentials> Login(string email, string password)
    {
        try
        {
            var u = new UserDto {Email = email, Password = password};
            
            return _repository.Login(u);
        }
        catch (Exception ex)
        {
            return new BadRequestObjectResult(ex);
        }
    }
    
}