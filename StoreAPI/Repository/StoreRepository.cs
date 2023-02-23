using StoreAPI.Entities.Dto;
using StoreAPI.Entities.Interface;
using StoreAPI.Entities.Models;
using StoreAPI.Entities.Utils;
using StoreAPI.Infrastructure.Context;
using static System.Decimal;

namespace StoreAPI.Repository;

public class StoreRepository : BaseRepository, IStoreRepository
{
    private readonly IProductRepository _productRepository;
    
    public StoreRepository(StoreDb context, IProductRepository productRepository) : base(context)
    {
        this._productRepository = productRepository;
    }

    public bool Exists(long id)
    {
        return Context.Store.Any(x => x.IdStore == id);
    }

    public StoreDto FindById(long id)
    {
        try
        {
            var res = (from str in Context.Store
                    where str.IdStore == id && str.LogState == (int) Constants.StatusRecord.ACTIVE
                    select new StoreDto
                    {
                        IdStore = str.IdStore,
                        Name = str.Name,
                        Address = str.Address,
                        Latitude = str.Latitude,
                        Longitude = str.Longitude,
                        LogDateCrate = str.LogDateCrate,
                        LogDateModified = str.LogDateModified,
                        LogState = str.LogState,

                        Products = str.Product.Where(
                            x => x.LogState == (int) Constants.StatusRecord.ACTIVE
                        ).Select(y => new ProductDto
                        {
                            IdProduct = y.IdProduct,
                            IdCategory = y.IdCategory,
                            IdStore = y.IdStore,
                            Name = y.Name,
                            Code = y.Code,
                            Stock = y.Stock,
                            Brand = y.Brand,
                            DueDate = y.DueDate
                        }).ToList()
                    }
                ).FirstOrDefault();
            return res ?? new StoreDto();
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public StoreDto Save(StoreDto store)
    {
        try
        {
            if (!Exists(store.IdStore))
            {
                var newStore = new Store
                {
                    IdStore = store.IdStore,
                    Name = store.Name,
                    Address = store.Address,
                    Latitude = store.Latitude,
                    Longitude = store.Longitude,
                    LogDateCrate = DateTime.Now,
                    LogDateModified = DateTime.Now,
                    LogState = store.LogState
                };
                Context.Add(newStore);
                _productRepository.SaveList(store.Products);
            }
            else
            {
                var actStore = Context.Store.FirstOrDefault(x =>
                                   x.IdStore == store.IdStore &&
                                   x.LogState == (int) Constants.StatusRecord.ACTIVE) ??
                               throw new Exception("");
                actStore.IdStore = actStore.IdStore;
                actStore.Name = actStore.Name;
                actStore.Address = actStore.Address;
                actStore.Latitude = actStore.Latitude;
                actStore.Longitude = actStore.Longitude;
                actStore.LogDateCrate = actStore.LogDateCrate;
            }
            Context.SaveChanges();
            return store;
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public List<StoreDto> SaveList(List<StoreDto> listStore)
    {
        try
        {
            var tmpStore = new List<Store>();

            foreach (var store in listStore)
            {
                if (Exists(store.IdStore))
                {
                    var storeUpdate = Context.Store.First(x => x.IdStore == store.IdStore);
                    storeUpdate.Name = store.Name;
                    storeUpdate.Address = store.Address;
                    storeUpdate.Latitude = store.Latitude;
                    storeUpdate.Longitude = store.Longitude;
                    storeUpdate.LogDateModified = DateTime.Now;
                    storeUpdate.LogState = store.LogState;
                    continue;
                }

                var newStore = new Store
                {
                    Name = store.Name,
                    Address = store.Address,
                    Latitude = store.Latitude,
                    Longitude = store.Longitude,
                    LogDateCrate = store.LogDateCrate,
                    LogDateModified = store.LogDateModified,
                    LogState = store.LogState,
                };

                tmpStore.Add(newStore);
                Context.Add(newStore);

                _productRepository.SaveList(store.Products);

            }

            Context.SaveChanges();

            for (var i = 0; i < tmpStore.Count; i++)
            {
                listStore[i].IdStore = tmpStore[i].IdStore;
            }

            return listStore;
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public List<long> GetIds()
    {
        return Context.Store.ToList().Select(x => (long) x.IdStore).ToList();
    }

    public List<string> GetNames()
    {
        return Context.Store.ToList().Select(x => x.Address).ToList();
    }

    public CollectionResponse<StoreDto> ListStore(string name, string address, double radio, int order, 
        double latitude, double longitude, int page, int reg)
    {
        try
        {
            var query = (from store in Context.Store
                    where (name == "" || store.Name.ToLower().Contains(name.ToLower())) &&
                          (address == "" || store.Address.ToLower().Contains(address.ToLower()))
                    select new StoreDto
                    {
                        IdStore = store.IdStore,
                        Name = store.Name,
                        Address = store.Address,
                        Latitude = store.Latitude,
                        Longitude = store.Longitude,
                        LogDateCrate = store.LogDateCrate,
                        LogDateModified = store.LogDateModified,
                        LogState = store.LogState
                    }
                );
            
            var query2 = query.ToList().Where(x => ObtainDistanceInMeters(latitude, 
                longitude, x) < radio);
            
            query2 = order switch
            {
                1 => query2.OrderBy(x => x.Distance),
                2 => query2.OrderByDescending(x => x.Address),
                _ => query2
            };

            var res2 = new List<StoreDto>();

            foreach (var store in query2)
            {
                var e = res2.FirstOrDefault(x => x.IdStore == store.IdStore);
                if (e == null)
                {
                    res2.Add(store);
                }
            }

            return new CollectionResponse<StoreDto>
            {
                Count = res2.Count,
                Page = page,
                Data = res2.Skip(page * reg).Take(reg).ToList()
            };            
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    
    private static int ObtainDistanceInMeters(double latitude1, double longitude1, StoreDto store)
    {
        double distance;
        const int earthRadio = 6371;

        try
        {
            var latitude = (ToDouble(store.Latitude) - latitude1) * (Math.PI / 180);
            var longitude = (ToDouble(store.Longitude) - longitude1) * (Math.PI / 180);
            
            var a = Math.Sin(latitude / 2) * Math.Sin(latitude / 2) + Math.Cos(latitude1 * (Math.PI / 180)) *
                Math.Cos(ToDouble(store.Latitude) * (Math.PI / 180)) * Math.Sin(longitude / 2) *
                Math.Sin(longitude / 2);
            
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            
            distance = (earthRadio * c) * 1000;
        }
        catch (Exception)
        {
            distance = -1;
        }

        var distanceFinal = (int) Math.Round(distance, MidpointRounding.AwayFromZero);

        store.Distance = distanceFinal;
        
        return distanceFinal;
    }
}