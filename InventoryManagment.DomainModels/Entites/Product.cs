namespace InventoryManagment.DomainModels.Entites
{
    public class Product : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
        public bool IsActive { get; set; }
        public Category Category { get; set; }
    }
}
