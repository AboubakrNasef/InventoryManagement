namespace InventoryManagement.Application.Features.Users
{
    public class JwtTokenResult
    {
        public string Token { get; set; }
        public int ExpiresIn { get; set; }
    }
}
