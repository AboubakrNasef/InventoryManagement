using InventoryManagement.Application.Common;

namespace InventoryManagement.Infrastructure.CommonServices
{
    public class PasswordHasher : IPasswordHasher
    {
        public string HashPassword(string pass)
        {
            return BCrypt.Net.BCrypt.HashPassword(pass);
        }
    }
}
