using InventoryManagement.Application.RedisSearch;
using InventoryManagment.DomainModels.Interfaces;
using Redis.OM;
using Redis.OM.Contracts;
using Redis.OM.Searching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Infrastructure.Repositories.Redis
{
    public class ProductSearchRepository : RedisBaseRepository<ProductSearchModel>, IProductSearchRepository
    {
        public ProductSearchRepository(IRedisConnectionProvider redisConnectionProvider) : base(redisConnectionProvider)
        {
        }
    }
}
