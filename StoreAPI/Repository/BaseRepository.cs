using StoreAPI.Infrastructure.Context;

namespace StoreAPI.Repository;

public class BaseRepository
{

    protected readonly StoreDb Context;

    protected BaseRepository(StoreDb context)
    {
        Context = context;
    }

}