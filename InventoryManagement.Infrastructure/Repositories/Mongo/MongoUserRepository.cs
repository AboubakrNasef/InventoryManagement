using InventoryManagment.DomainModels.Entites;
using InventoryManagment.DomainModels.Repositories;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace InventoryManagement.Infrastructure.Repositories.Mongo
{
    public class MongoUserRepository : MongoBaseRepository<User>, IUserRepository
    {
        public MongoUserRepository(IMongoDatabase database, IIDCreator iDCreator) : base(database, iDCreator)
        {
        }


        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _collection.Find(u => u.UserName == username).FirstOrDefaultAsync();
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _collection.Find(u => u.Email == email).FirstOrDefaultAsync();
        }

        public async Task<bool> ExistsByUsernameAsync(string username)
        {
            return await _collection.Find(u => u.UserName == username).AnyAsync();
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _collection.Find(u => u.Email == email).AnyAsync();
        }

        public async Task<IList<User>> GetByRoleAsync(string role)
        {
            return await _collection.Find(u => u.Role == role).ToListAsync();
        }
    }
}
