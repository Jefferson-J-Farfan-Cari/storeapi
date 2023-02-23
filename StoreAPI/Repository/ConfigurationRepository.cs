using StoreAPI.Entities.Dto;
using StoreAPI.Entities.Interface;
using StoreAPI.Entities.Utils;
using StoreAPI.Infrastructure.Context;

namespace StoreAPI.Repository;

public class ConfigurationRepository : BaseRepository, IConfigurationRepository
{
    public ConfigurationRepository(StoreDb context) : base(context)
    {
    }

    public CollectionResponse<CategoryDto> ListCategories(int page, int reg)
    {
        try
        {
            var res = Context.Category.Where(
                x => x.LogState == (int) Constants.StatusRecord.ACTIVE
                ).Select(category => new CategoryDto
            {
                IdCategory = category.IdCategory,
                Name = category.Name,
                Description = category.Description,
                LogState = category.LogState
            }).ToList();
            
            return new CollectionResponse<CategoryDto>
            {
                Count = res.Count,
                Page = page,
                Data = res.Skip(page * reg).Take(reg).ToList()
            };
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public CollectionResponse<StoreDto> ListStores(int page, int reg)
    {
        try
        {
            var res = Context.Store.Where(
                x => x.LogState == (int) Constants.StatusRecord.ACTIVE
                ).Select(store =>
                new StoreDto
                {
                    IdStore = store.IdStore,
                    Name = store.Name,
                    Address = store.Address,
                    Latitude = store.Latitude,
                    Longitude = store.Longitude 
                }).ToList();

            return new CollectionResponse<StoreDto>
            {
                Count = res.Count,
                Page = page,
                Data = res.Skip(page * reg).Take(reg).ToList()
            };;
        }
        catch (Exception e)
        {
            throw e;
        }
    }
}