namespace InventoryManagement.Application.Features.Products
{
    public class ProductDto: CreateProductDto
    {
        public Guid Id { get; set; }

    }
    public class CreateProductDto
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
