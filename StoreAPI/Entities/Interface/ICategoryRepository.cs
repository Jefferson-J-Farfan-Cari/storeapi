using StoreAPI.Entities.Dto;

namespace StoreAPI.Entities.Interface;

public interface ICategoryRepository : IBaseRepository <CategoryDto, long>
{
    List<CategoryDto> SaveList(List<CategoryDto> listCategory);

}