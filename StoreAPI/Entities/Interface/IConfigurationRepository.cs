using StoreAPI.Entities.Dto;

namespace StoreAPI.Entities.Interface;

public interface IConfigurationRepository
{
    CollectionResponse<CategoryDto> ListCategories(int page, int reg);
    CollectionResponse<StoreDto> ListStores(int page, int reg);
    CollectionResponse<ProductDto> ListProducts(int page, int reg);
    CollectionResponse<UserDto> ListUsers(int page, int reg);

}