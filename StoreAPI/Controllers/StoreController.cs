using Microsoft.AspNetCore.Mvc;
using StoreAPI.Entities.Dto;
using StoreAPI.Entities.Interface;
using StoreAPI.Entities.Security;

namespace StoreAPI.Controllers;

[Route("api/v1/stores")]
//[Route("api/v1/stores")]
[ApiController]
public class StoreController : ControllerBase
{
    
    private readonly IStoreRepository _repository;
    private readonly IConfigurationRepository _configurationRepository;

    public StoreController(IStoreRepository repository, IConfigurationRepository configurationRepository)
    {
        _repository = repository;
        _configurationRepository = configurationRepository;
    }

    [HttpGet("")]
    [CheckToken]
    public ActionResult<CollectionResponse<StoreDto>> ListStores(int page = 0, int reg = 10)
    {
        try
        {
            return _configurationRepository.ListStores(page, reg);
        }
        catch (Exception e)
        {
            return new BadRequestObjectResult(e);
        }
    }

    [HttpGet("by_id")]
    [CheckToken]
    public ActionResult<StoreDto> FindById(long idStore)
    {
        try
        {
            return _repository.FindById(idStore);
        }
        catch (Exception ex)
        {
            return new BadRequestObjectResult(ex);
        }
    }
    
    [HttpGet("filters")]
    [CheckToken]
    public ActionResult<CollectionResponse<StoreDto>> ListStores(string storeName = "", string address = "",
        double radio = 0.0, int order = 0, double latitude = 0.0, double longitude = 0.0, int page = 0, int reg = 10)
    {
        try
        {
            return _repository.ListStore(storeName, address, radio, order, latitude, longitude, page, reg);
        }
        catch (Exception e)
        {
            return new BadRequestObjectResult(e);
        }
    }
    
    [HttpPost("")]
    [CheckToken]
    public ActionResult<StoreDto> Save(StoreDto store)
    {
        try
        {
            return _repository.Save(store);
        }
        catch (Exception ex)
        {
            return new BadRequestObjectResult(ex);
        }
    }

    [HttpPut("")]
    [CheckToken]
    public ActionResult<StoreDto> Update(StoreDto store)
    {
        try
        {
            return _repository.Save(store);
        }
        catch (Exception ex)
        {
            return new BadRequestObjectResult(ex);
        }
    }
    
}