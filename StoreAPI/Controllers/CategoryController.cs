using Microsoft.AspNetCore.Mvc;
using StoreAPI.Entities.Dto;
using StoreAPI.Entities.Interface;
using StoreAPI.Entities.Security;

namespace StoreAPI.Controllers;

[Route("api/v1/categories")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryRepository _repository;
    private readonly IConfigurationRepository _configurationRepository;

    public CategoryController(ICategoryRepository repository, IConfigurationRepository configurationRepository)
    {
        _repository = repository;
        _configurationRepository = configurationRepository;
    }

    [HttpGet("")]
    [CheckToken]
    public ActionResult<CollectionResponse<CategoryDto>> ListCategories(int page = 0, int reg = 10)
    {
        try
        {
            return _configurationRepository.ListCategories(page, reg);
        }
        catch (Exception e)
        {
            return new BadRequestObjectResult(e);
        }
    }
    
    [HttpGet("by_id")]
    [CheckToken]
    public ActionResult<CategoryDto> FindById(long idCategory)
    {
        try
        {
            return _repository.FindById(idCategory);
        }
        catch (Exception ex)
        {
            return new BadRequestObjectResult(ex);
        }
    }
    
    [HttpPost("")]
    [CheckToken]
    public ActionResult<CategoryDto> Save(CategoryDto category)
    {
        try
        {
            return _repository.Save(category);
        }
        catch (Exception ex)
        {
            return new BadRequestObjectResult(ex);
        }
    }

    [HttpPost("save_list")]
    [CheckToken]
    public ActionResult<List<CategoryDto>> SaveList(List<CategoryDto> category)
    {
        try
        {
            return _repository.SaveList(category);
        }
        catch (Exception ex)
        {
            return new BadRequestObjectResult(ex);
        }
    }

    [HttpPut("")]
    [CheckToken]
    public ActionResult<List<CategoryDto>> Update(List<CategoryDto> category)
    {
        try
        {
            return _repository.SaveList(category);
        }
        catch (Exception ex)
        {
            return new BadRequestObjectResult(ex);
        }
    }
}