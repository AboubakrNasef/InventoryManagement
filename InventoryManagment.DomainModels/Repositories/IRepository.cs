namespace InventoryManagment.DomainModels.Repositories
{
    public interface IRepository<T>
    {
        Task<T> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task<long> UpdateAsync(T entity);
        Task<long> DeleteAsync(Guid id);
    }
}
