using Microsoft.AspNetCore.Mvc;
using StoreAPI.Entities.Dto;
using StoreAPI.Entities.Interface;
using StoreAPI.Entities.Security;

namespace StoreAPI.Controllers;

[Route("api/v1/products")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _repository;
    private readonly IConfigurationRepository _configurationRepository;

    public ProductController(IProductRepository repository, IConfigurationRepository configurationRepository)
    {
        _repository = repository;
        _configurationRepository = configurationRepository;
    }

    [HttpGet("")]
    [CheckToken]
    public ActionResult<CollectionResponse<ProductDto>> ListProducts(int page = 0, int reg = 10)
    {
        try
        {
            return _configurationRepository.ListProducts(page, reg);
        }
        catch (Exception e)
        {
            return new BadRequestObjectResult(e);
        }
    }
    
    [HttpGet("by_id")]
    [CheckToken]
    public ActionResult<ProductDto> FindById(long idProduct)
    {
        try
        {
            return _repository.FindById(idProduct);
        }
        catch (Exception ex)
        {
            return new BadRequestObjectResult(ex);
        }
    }

    [HttpGet("filters")]
    [CheckToken]
    public ActionResult<CollectionResponse<StoreDto>> ListProducts(string nameProduct = "", double latitude = 0.0,
        double longitude = 0.0, double radio = 0.0, int order = 0,
        int category = 0, decimal min = 0.0m, decimal max = 0.0m,
        int page = 0, int reg = 10, int idStore = 0, int state = 1, int stock = 1)
    {
        try
        {
            return _repository.ListProducts(nameProduct, radio, order, category, min,
                max, latitude, longitude, page, reg, idStore, state, stock);
        }
        catch (Exception ex)
        {
            return new BadRequestObjectResult(ex);
        }
    }
    
    [HttpPost("")]
    [CheckToken]
    public ActionResult<ProductDto> Save(ProductDto product)
    {
        try
        {
            return _repository.Save(product);
        }
        catch (Exception ex)
        {
            return new BadRequestObjectResult(ex);
        }
    }

    [HttpPost("save_list")]
    [CheckToken]
    public ActionResult<List<ProductDto>> SaveList(List<ProductDto> product)
    {
        try
        {
            return _repository.SaveList(product);
        }
        catch (Exception ex)
        {
            return new BadRequestObjectResult(ex);
        }
    }

    [HttpPut("")]
    [CheckToken]
    public ActionResult<List<ProductDto>> Update(List<ProductDto> product)
    {
        try
        {
            return _repository.SaveList(product);
        }
        catch (Exception ex)
        {
            return new BadRequestObjectResult(ex);
        }
    }
    
}