namespace InventoryManagment.DomainModels.Entites
{
    public class PurchaseOrder : IEntity
    {
        public int Id { get; set; }
        public int OrderNumber { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public DateTimeOffset ExpectedDate { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
