using StoreAPI.Entities.Dto;
using StoreAPI.Entities.Interface;
using StoreAPI.Entities.Models;
using StoreAPI.Entities.Utils;
using StoreAPI.Infrastructure.Context;
using static System.Decimal;

namespace StoreAPI.Repository;

public class ProductRepository: BaseRepository, IProductRepository
{

    public ProductRepository(StoreDb context) : base(context)
    {
    }

    public bool Exists(long id)
    {
        return Context.Product.Any(x => x.IdProduct == id);
    }

    public ProductDto FindById(long id)
    {
        try
        {
            var result = (from product in Context.Product
                    join category in Context.Category on product.IdCategory equals category.IdCategory
                    where product.IdProduct == id && product.LogState == (int) Constants.StatusRecord.ACTIVE
                    select new ProductDto
                    {
                        IdProduct = product.IdProduct,
                        IdCategory = product.IdCategory,
                        IdStore = product.IdStore,
                        Code = product.Code,
                        Name = product.Name,
                        Description = product.Description,
                        Stock = product.Stock,
                        Brand = product.Brand,
                        Price = product.Price,
                        DueDate = product.DueDate,
                        LogState = product.LogState,
                        Category = new CategoryDto
                        {
                            IdCategory = category.IdCategory,
                            Name = category.Name,
                            Description = category.Description,
                            LogState = category.LogState
                        }
                    }
                ).FirstOrDefault();
            return result ?? new ProductDto();
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public ProductDto Save(ProductDto product)
    {
        try
        {
            if (!Exists(product.IdProduct))
            {
                var newProduct = new Product
                {
                    IdProduct = product.IdProduct,
                    Name = product.Name,
                    Description = product.Description,
                    Stock = product.Stock,
                    Brand = product.Brand,
                    Price = product.Price,
                    DueDate = product.DueDate,
                    LogDateCrate = DateTime.Now,
                    LogDateModified = DateTime.Now,
                    LogState = product.LogState
                };
                Context.Add(newProduct);
            }
            else
            {
                var actProduct = Context.Product.FirstOrDefault(x =>
                    x.IdProduct == product.IdProduct &&
                    x.LogState == (int)Constants.StatusRecord.ACTIVE) ?? throw new Exception("");
                actProduct.IdCategory = product.IdCategory;
                actProduct.Name = product.Name;
                actProduct.Description = product.Description;
                actProduct.Stock = product.Stock;
                actProduct.Brand = product.Brand;
                actProduct.Price = product.Price;
                actProduct.DueDate = product.DueDate;
                actProduct.LogDateModified = DateTime.Now;
                actProduct.LogState = product.LogState;
            }
            Context.SaveChanges();
            return product;
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public List<ProductDto> SaveList(List<ProductDto> listProducts)
    {
        try
        {
            var tmpProducts = new List<Product>();
            
            foreach (var product in listProducts)
            {
                if (Exists(product.IdProduct))
                {
                    var productUpdate = Context.Product.First(x => x.IdProduct == product.IdProduct);

                    productUpdate.Code = product.Code;
                    productUpdate.Name = product.Name;
                    productUpdate.Stock = product.Stock;
                    productUpdate.Price = product.Price;
                    productUpdate.Brand = product.Brand;
                    productUpdate.Description = product.Description;
                    productUpdate.LogDateModified = DateTime.Now;
                    productUpdate.LogState = product.LogState;

                }
                else
                {
                    var newProduct = new Product
                    {
                        IdCategory = product.IdCategory,
                        IdStore = product.IdStore,
                        Code = product.Code,
                        Name = product.Name,
                        Stock = product.Stock,
                        Price = product.Price,
                        Brand = product.Brand,
                        Description = product.Description,
                        DueDate = product.DueDate,
                        LogDateCrate = DateTime.Now,
                        LogDateModified = DateTime.Now,
                        LogState = product.LogState,
                    };
                    tmpProducts.Add(newProduct);
                    Context.Add(newProduct);
                }

            }
            Context.SaveChanges();
            for (var i = 0; i < tmpProducts.Count; i++)
            {
                listProducts[i].IdProduct = tmpProducts[i].IdProduct;
            }

            return listProducts;
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public CollectionResponse<StoreDto> ListProducts(string name, double radio, int order, int category, 
        decimal min, decimal max, double latitude, double longitude, int page, int reg, int idStore,
        int state, int stock)
    {
        try
        {
            var query = (from p in Context.Product
                    where (name == "" || p.Description.Contains(name)) &&
                          (category == 0 || p.IdCategory == category) &&
                          (idStore == 0 || p.IdStore == idStore) &&
                          (min == 0.0m || p.Price > min) &&
                          (max == 0.0m || p.Price < max) &&
                          (state == 1 || p.LogState == state) &&
                          (stock == 0 || p.Stock > 0) &&
                          p.Store.LogState == (int)Constants.StatusRecord.ACTIVE
                    select new StoreDto
                    {
                        IdStore = p.Store.IdStore,
                        Address = p.Store.Address,
                        Name = p.Store.Name,
                        Latitude = p.Store.Latitude,
                        Longitude = p.Store.Longitude,
                        LogDateCrate = p.Store.LogDateCrate,
                        LogDateModified = p.Store.LogDateModified,
                        LogState = p.Store.LogState,
                        Products = new List<ProductDto>
                        {
                            new ()
                            {
                                IdProduct = p.IdProduct,
                                IdCategory = p.IdCategory,
                                IdStore = p.IdStore,
                                Code = p.Code,
                                Name = p.Name,
                                Stock = p.Stock,
                                Price = p.Price,
                                Brand = p.Brand,
                                Description = p.Description,
                                DueDate = p.DueDate,
                                LogState = p.LogState,
                                Category = new CategoryDto
                                {
                                    IdCategory = p.Category.IdCategory,
                                    Name = p.Category.Name,
                                    Description = p.Category.Description,
                                    LogState = p.Category.LogState
                                }
                            }
                        }
                    }
                );

            var query2 = query.ToList().Where(x => ObtainDistanceInMeters(latitude, 
                longitude, x) < radio);
            
            query2 = order switch
            {
                1 => query2.OrderByDescending(x => x.Distance),
                2 => query2.OrderByDescending(x => x.Products[0].Price),
                _ => query2
            };

            var count = query2.Count();

            query2 = query2.Skip(page * reg).Take(reg);

            var res2 = new List<StoreDto>();
            foreach (var store in query2)
            {
                var e = res2.FirstOrDefault(x => x.IdStore == store.IdStore);
                if (e == null)
                {
                    res2.Add(store);
                }
                else
                {
                    e.Products = e.Products.Concat(store.Products).ToList();
                }
            }

            return new CollectionResponse<StoreDto>
            {
                Count = count,
                Page = page,
                Data = res2
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