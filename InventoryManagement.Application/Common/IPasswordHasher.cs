namespace InventoryManagement.Application.Common
{
    public interface IPasswordHasher
    {
        string HashPassword(string pass);
    }
}
