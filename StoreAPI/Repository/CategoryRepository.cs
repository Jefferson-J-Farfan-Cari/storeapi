using StoreAPI.Entities.Dto;
using StoreAPI.Entities.Interface;
using StoreAPI.Entities.Models;
using StoreAPI.Entities.Utils;
using StoreAPI.Infrastructure.Context;

namespace StoreAPI.Repository;

public class CategoryRepository : BaseRepository, ICategoryRepository
{
    public CategoryRepository(StoreDb context) : base(context)
    {
    }

    public bool Exists(long id)
    {
        return Context.Category.Any(x => x.IdCategory == id);
    }

    public CategoryDto FindById(long id)
    {
        try
        {
            var result = (from category in Context.Category
                    where category.IdCategory == id && category.LogState == (int) Constants.StatusRecord.ACTIVE
                    select new CategoryDto
                    {
                        IdCategory = category.IdCategory,
                        Name = category.Name,
                        Description = category.Description,
                        LogDateCrate = category.LogDateCrate,
                        LogDateModified = category.LogDateModified,
                        LogState = category.LogState
                    }
                ).FirstOrDefault();
            return result ?? new CategoryDto();
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public CategoryDto Save(CategoryDto category)
    {
        try
        {
            if (!Exists(category.IdCategory))
            {
                var newCategory = new Category
                {
                    IdCategory = category.IdCategory,
                    Name = category.Name,
                    Description = category.Description,
                    LogDateCrate = DateTime.Now,
                    LogDateModified = DateTime.Now,
                    LogState = category.LogState
                };
                Context.Add(newCategory);
            }
            else
            {
                var actCategory = Context.Category.FirstOrDefault(x =>
                    x.IdCategory == category.IdCategory &&
                    x.LogState == (int)Constants.StatusRecord.ACTIVE) ?? throw new Exception("");
                actCategory.Name = category.Name;
                actCategory.Description = category.Description;
                actCategory.LogDateModified = DateTime.Now;
                actCategory.LogState = category.LogState;
            }
            Context.SaveChanges();
            return category;
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public List<CategoryDto> SaveList(List<CategoryDto> listCategory)
    {
        throw new NotImplementedException();
    }
    
}