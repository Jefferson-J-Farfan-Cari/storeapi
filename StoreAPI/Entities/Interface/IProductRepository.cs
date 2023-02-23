using StoreAPI.Entities.Dto;

namespace StoreAPI.Entities.Interface;

public interface IProductRepository : IBaseRepository<ProductDto, long>
{
    List<ProductDto> SaveList(List<ProductDto> listProduct);

    CollectionResponse<StoreDto> ListProducts(string name, double radio, int order, int category,
        decimal min, decimal max, double latitude, double longitude, int page,
        int reg, int idStore, int state, int stock);
}