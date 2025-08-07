namespace InventoryManagment.DomainModels.Entites
{
    public class PurchaseOrder : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public DateTimeOffset ExpectedDate { get; set; }
        public float TotalAmount { get; set; }
        public List<int> ProductIds { get; set; } = new List<int>();
    }
}
