using InventoryManagement.Application.RedisSearch;
using InventoryManagment.DomainModels.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagment.DomainModels.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<ProductSearchModel> GetSearchModelByIdAsync(Guid productId);
    }
}
