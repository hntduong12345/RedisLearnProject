using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using RedisAndUoW.Domain.Models;
using RedisAndUoW.Repository.Interfaces;
using RedisAndUoW.Services.Interfaces;
using System.Threading;

namespace RedisAndUoW.Services.Implements
{
    public class ProductService : BaseService<ProductService>, IProductService
    {
        private readonly IDistributedCache _distributedCache;

        public ProductService(IUnitOfWork<DecorationShopDBContext> unitOfWork, ILogger<ProductService> logger, IMapper mapper,
            IHttpContextAccessor httpContextAccessor, IDistributedCache distributedCache) 
            : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
            _distributedCache = distributedCache;
        }

        public async Task<Product> GetProductsById(string id, CancellationToken cancellationToken = default)
        {
            string key = $"product-{id}";

            string? cachedProduct = await _distributedCache.GetStringAsync(key, cancellationToken);

            Product? product;

            if(string.IsNullOrEmpty(cachedProduct)) 
            {
                //Get product in database
                product = await _unitOfWork.GetRepository<Product>().SingleOrDefaultAsync();

                if(product is null)
                {
                    return product;
                }

                await _distributedCache.SetStringAsync(
                    key, 
                    JsonConvert.SerializeObject(product),
                    cancellationToken);
                return product;
            }

            product = JsonConvert.DeserializeObject<Product>(cachedProduct,
                new JsonSerializerSettings  //Setting get the non public constrcutor
                {
                    ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
                });
            return product;
        }

        public async Task<List<Product>> GetProductListByName(string name, CancellationToken cancellationToken)
        {
            string key = $"product-{name}";

            string? cachedProducts = await _distributedCache.GetStringAsync(key, cancellationToken);

            List<Product>? products;

            if (string.IsNullOrEmpty(cachedProducts))
            {
                //Get product in database
                if (string.IsNullOrEmpty(name))
                {
                    products = (List<Product>?)await _unitOfWork.GetRepository<Product>().GetListAsync();
                }
                else
                {
                    products = (List<Product>?)await _unitOfWork.GetRepository<Product>().GetListAsync(
                        predicate: p => p.Name.Equals(name));
                }

                if (products is null)
                {
                    return products;
                }

                await _distributedCache.SetStringAsync(
                    key,
                    JsonConvert.SerializeObject(products),
                    cancellationToken);
                return products;
            }

            products = JsonConvert.DeserializeObject<List<Product>>(cachedProducts,
                new JsonSerializerSettings  //Setting get the non public constrcutor
                {
                    ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
                });
            return products;
        }
    }
}
