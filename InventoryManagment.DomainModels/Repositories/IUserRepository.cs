using InventoryManagment.DomainModels.Entites;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace InventoryManagment.DomainModels.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByUsernameAsync(string username);
        Task<User> GetByEmailAsync(string email);
        Task<bool> ExistsByUsernameAsync(string username);
        Task<bool> ExistsByEmailAsync(string email);
        Task<IList<User>> GetByRoleAsync(string role);
        // Add more user-specific methods here if needed
    }
}
