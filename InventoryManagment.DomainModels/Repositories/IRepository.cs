namespace InventoryManagment.DomainModels.Interfaces
{
    public interface IRepository<T>
    {
        Task<T> GetByIdAsync(int id);
        IEnumerable<T> GetAllAsync();
        Task AddAsync(T entity);
        Task<long> UpdateAsync(T entity);
        Task<long> DeleteAsync(int id);
    }
}
