namespace InventoryManagment.DomainModels.Repositories
{
    public interface IRepository<T>
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task<long> UpdateAsync(T entity);
        Task<long> DeleteAsync(int id);
    }
}
