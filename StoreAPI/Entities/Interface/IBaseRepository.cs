namespace StoreAPI.Entities.Interface;

public interface IBaseRepository <T, K>
{
    bool Exists(K id);
    T FindById(K id);
    T Save(T entity);
}