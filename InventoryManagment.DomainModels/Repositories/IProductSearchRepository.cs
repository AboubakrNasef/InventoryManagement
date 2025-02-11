using InventoryManagement.Application.RedisSearch;

namespace InventoryManagment.DomainModels.Interfaces
{
    public interface IProductSearchRepository
    {
        Task<ProductSearchModel> GetByIdAsync(int id);
        Task<IList<ProductSearchModel>> GetAllAsync();
        Task AddAsync(ProductSearchModel product);
        Task UpdateAsync(ProductSearchModel product);
        Task DeleteAsync(int id);
    }
}
