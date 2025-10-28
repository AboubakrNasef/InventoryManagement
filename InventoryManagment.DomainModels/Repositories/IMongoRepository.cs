namespace InventoryManagment.DomainModels.Repositories
{
    public interface IMongoRepository<T>: IRepository<T>
    {
        Task<long> UpdateByDictionary(Guid entityId, Dictionary<string, object> updateDictionary);
    }
}
