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
                LogDateCrate = category.LogDateCrate,
                LogDateModified = category.LogDateModified,
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
                    Longitude = store.Longitude,
                    Products = Context.Product.Where(
                        x => 
                            x.IdStore == store.IdStore && 
                            x.LogState == (int) Constants.StatusRecord.ACTIVE
                    ).Select(
                        y => new ProductDto 
                        {
                            IdProduct = y.IdProduct,
                            IdCategory = y.IdCategory,
                            IdStore = y.IdStore,
                            Name = y.Name,
                            Description = y.Description,
                            Code = y.Code,
                            Stock = y.Stock,
                            Brand = y.Brand,
                            Price = y.Price,
                            DueDate = y.DueDate
                        }).ToList()
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

    public CollectionResponse<ProductDto> ListProducts(int page, int reg)
    {
        try
        {
            var res = Context.Product.Where(
                x => x.LogState == (int) Constants.StatusRecord.ACTIVE
            ).Select(product =>
                new ProductDto
                {
                    IdProduct = product.IdProduct,
                    IdCategory = product.IdCategory,
                    Category = Context.Category.Where(
                        y => y.IdCategory == product.IdProduct
                    ).Select(category => 
                        new CategoryDto 
                        { 
                            IdCategory = category.IdCategory, 
                            Name = category.Name, 
                            Description = category.Description, 
                        }).FirstOrDefault() ?? new CategoryDto(),
                    IdStore = product.IdStore,
                    Code = product.Code,
                    Name = product.Name,
                    Description = product.Description,
                    Stock = product.Stock,
                    Brand = product.Brand,
                    Price = product.Price,
                    DueDate = product.DueDate
                }).ToList().OrderBy(x=>x.IdProduct);

            return new CollectionResponse<ProductDto>
            {
                Count = res.Count(),
                Page = page,
                Data = res.Skip(page * reg).Take(reg).ToList()
            };;
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public CollectionResponse<UserDto> ListUsers(int page, int reg)
    {
        try
        {
            var res = Context.User.Where(
                x => x.LogState == (int) Constants.StatusRecord.ACTIVE
            ).Select(user =>
                new UserDto
                {
                    IdUser = user.IdUser,
                    DocumentType = user.DocumentType,
                    Name = user.Name,
                    FatherLN = user.FatherLN,
                    MotherLN = user.MotherLN,
                    Document = user.Document,
                    Email = user.Email
                }).ToList();

            return new CollectionResponse<UserDto>
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