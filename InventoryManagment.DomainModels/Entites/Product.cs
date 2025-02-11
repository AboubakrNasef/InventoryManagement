namespace InventoryManagment.DomainModels.Entites
{
    public record Product
        (
            int Id,
            string Name,
            string Description,
            decimal Price,
            int Quantity
        );
}
