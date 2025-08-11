using InventoryManagement.Application.Features.Users;
using InventoryManagment.DomainModels.Entites;

namespace InventoryManagement.Application.Common
{
    public interface IJwtTokenService
    {
        JwtTokenResult GenerateToken(User user);
    }
}
