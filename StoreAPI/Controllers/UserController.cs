using Microsoft.AspNetCore.Mvc;
using StoreAPI.Entities.Dto;
using StoreAPI.Entities.Interface;

namespace StoreAPI.Controllers;

[Route("api/v1/users")]
//[Route("api/v1/users")]
[ApiController]
public class UserController : ControllerBase
{
    
    private readonly IUserRepository _repository;

    public UserController(IUserRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("")]
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
    //  [VerificaToken]
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
    
}