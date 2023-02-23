using Microsoft.AspNetCore.Mvc;
using StoreAPI.Entities.Dto;
using StoreAPI.Entities.Interface;

namespace StoreAPI.Controllers;

[Route("api/v1/products")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _repository;

    public ProductController(IProductRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("by_id")]
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
    //  [VerificaToken]
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
    //  [VerificaToken]
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
    //   [VerificaToken]
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