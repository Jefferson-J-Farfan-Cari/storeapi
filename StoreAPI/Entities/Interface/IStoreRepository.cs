using StoreAPI.Entities.Dto;

namespace StoreAPI.Entities.Interface;

public interface IStoreRepository: IBaseRepository<StoreDto, long>
{
    List<long> GetIds();
    List<string> GetNames();

    List<StoreDto> SaveList(List<StoreDto> listStore);

    CollectionResponse<StoreDto> ListStore(string name, string address,
        double radio, int order, double latitude, double longitude, int page, int reg);
}