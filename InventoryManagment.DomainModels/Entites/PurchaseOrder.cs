namespace InventoryManagment.DomainModels.Entites
{
    public class PurchaseOrder : IEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public DateTimeOffset ExpectedDate { get; set; }
        public float TotalAmount { get; set; }
        public List<int> ProductIds { get; set; } = new List<int>();
    }
}
