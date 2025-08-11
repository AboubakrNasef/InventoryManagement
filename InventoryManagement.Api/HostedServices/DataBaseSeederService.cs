
using InventoryManagment.DomainModels.Entites;
using InventoryManagment.DomainModels.Repositories;

namespace InventoryManagement.Api.HostedServices
{
    public class DataBaseSeederService : IHostedService
    {
        private readonly ILogger<DataBaseSeederService> _logger;
        private readonly IProductRepository _productRepo;
        private readonly IUserRepository _userRepo;
        private readonly ICategoryRepository _categoryRepo;
        private readonly IPurchaseOrderRepository _purchaseOrderRepo;
        private readonly List<Category> categories = new List<Category>
{
    new Category
    {
        Id = Guid.NewGuid(),
        Name = "Electronics",
        Description = "Devices, gadgets, and accessories."
    },
    new Category
    {
        Id = Guid.NewGuid(),
        Name = "Books",
        Description = "Printed and digital books of all genres."
    },
    new Category
    {
        Id = Guid.NewGuid(),
        Name = "Clothing",
        Description = "Apparel for men, women, and children."
    },
    new Category
    {
        Id = Guid.NewGuid(),
        Name = "Home & Kitchen",
        Description = "Furniture, appliances, and kitchenware."
    },
    new Category
    {
        Id = Guid.NewGuid(),
        Name = "Sports",
        Description = "Sports equipment and outdoor gear."
    }
};
        public DataBaseSeederService(ILogger<DataBaseSeederService> logger, IProductRepository productRepo, IUserRepository userRepo, ICategoryRepository categoryRepo, IPurchaseOrderRepository purchaseOrderRepo)
        {
            _logger = logger;
            _productRepo = productRepo;
            _userRepo = userRepo;
            _categoryRepo = categoryRepo;
            _purchaseOrderRepo = purchaseOrderRepo;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {// Categories
            var categories = new List<Category>
{
    new Category { Id = Guid.NewGuid(), Name = "Electronics", Description = "Devices, gadgets, and accessories." },
    new Category { Id = Guid.NewGuid(), Name = "Books", Description = "Printed and digital books of all genres." },
    new Category { Id = Guid.NewGuid(), Name = "Clothing", Description = "Apparel for men, women, and children." },
    new Category { Id = Guid.NewGuid(), Name = "Home & Kitchen", Description = "Furniture, appliances, and kitchenware." },
    new Category { Id = Guid.NewGuid(), Name = "Sports", Description = "Sports equipment and outdoor gear." }
};
            foreach (var category in categories)
            {
                _logger.LogInformation($"Adding category: {category.Name}");
                await _categoryRepo.AddAsync(category);
            }
            // Products
            var products = new List<Product>
{
    // Electronics (6)
    new Product { Id = Guid.NewGuid(), Name = "Wireless Headphones", Description = "Noise-cancelling over-ear headphones.", Price = 129.99f, Quantity = 50, IsActive = true, Category = categories[0] },
    new Product { Id = Guid.NewGuid(), Name = "Smartphone", Description = "Latest-gen smartphone with OLED display.", Price = 799.99f, Quantity = 30, IsActive = true, Category = categories[0] },
    new Product { Id = Guid.NewGuid(), Name = "Laptop", Description = "Lightweight laptop for work and play.", Price = 1199.99f, Quantity = 20, IsActive = true, Category = categories[0]},
    new Product { Id = Guid.NewGuid(), Name = "Bluetooth Speaker", Description = "Portable speaker with deep bass.", Price = 59.99f, Quantity = 70, IsActive = true, Category = categories[0] },
    new Product { Id = Guid.NewGuid(), Name = "Smartwatch", Description = "Fitness tracking and notifications.", Price = 199.99f, Quantity = 40, IsActive = true, Category = categories[0] },
    new Product { Id = Guid.NewGuid(), Name = "Gaming Console", Description = "Next-gen console for immersive gaming.", Price = 499.99f, Quantity = 15, IsActive = true, Category = categories[0] },

    // Books (6)
    new Product { Id = Guid.NewGuid(), Name = "Fantasy Novel", Description = "A thrilling fantasy adventure.", Price = 14.99f, Quantity = 200, IsActive = true, Category = categories[1] },
    new Product { Id = Guid.NewGuid(), Name = "Self-Help Guide", Description = "Improve your productivity and mindset.", Price = 19.99f, Quantity = 150, IsActive = true, Category = categories[1] },
    new Product { Id = Guid.NewGuid(), Name = "Cookbook", Description = "Delicious recipes for everyday cooking.", Price = 24.99f, Quantity = 80, IsActive = true, Category = categories[1] },
    new Product { Id = Guid.NewGuid(), Name = "History Book", Description = "A deep dive into ancient civilizations.", Price = 29.99f, Quantity = 60, IsActive = true, Category = categories[1] },
    new Product { Id = Guid.NewGuid(), Name = "Science Textbook", Description = "Comprehensive guide to physics.", Price = 49.99f, Quantity = 40, IsActive = true, Category = categories[1] },
    new Product { Id = Guid.NewGuid(), Name = "Mystery Novel", Description = "A suspenseful whodunit story.", Price = 16.99f, Quantity = 120, IsActive = true, Category = categories[1] },

    // Clothing (6)
    new Product { Id = Guid.NewGuid(), Name = "Men's T-Shirt", Description = "100% cotton, comfortable fit.", Price = 19.99f, Quantity = 150, IsActive = true, Category = categories[2] },
    new Product { Id = Guid.NewGuid(), Name = "Women's Dress", Description = "Elegant evening wear.", Price = 49.99f, Quantity = 60, IsActive = true, Category = categories[2] },
    new Product { Id = Guid.NewGuid(), Name = "Winter Jacket", Description = "Warm and waterproof.", Price = 99.99f, Quantity = 40, IsActive = true, Category = categories[2] },
    new Product { Id = Guid.NewGuid(), Name = "Running Shoes", Description = "Lightweight and comfortable.", Price = 79.99f, Quantity = 90, IsActive = true, Category = categories[2] },
    new Product { Id = Guid.NewGuid(), Name = "Baseball Cap", Description = "Adjustable and stylish.", Price = 14.99f, Quantity = 200, IsActive = true, Category = categories[2] },
    new Product { Id = Guid.NewGuid(), Name = "Jeans", Description = "Slim-fit denim.", Price = 39.99f, Quantity = 120, IsActive = true, Category = categories[2] },

    // Home & Kitchen (6)
    new Product { Id = Guid.NewGuid(), Name = "Non-stick Frying Pan", Description = "Durable 12-inch non-stick cookware.", Price = 29.99f, Quantity = 80, IsActive = true, Category = categories[3] },
    new Product { Id = Guid.NewGuid(), Name = "Coffee Maker", Description = "Brew perfect coffee at home.", Price = 89.99f, Quantity = 40, IsActive = true, Category = categories[3] },
    new Product { Id = Guid.NewGuid(), Name = "Vacuum Cleaner", Description = "High suction power with HEPA filter.", Price = 149.99f, Quantity = 25, IsActive = true, Category = categories[3] },
    new Product { Id = Guid.NewGuid(), Name = "Table Lamp", Description = "Modern design with LED light.", Price = 39.99f, Quantity = 100, IsActive = true, Category = categories[3] },
    new Product { Id = Guid.NewGuid(), Name = "Blender", Description = "Perfect for smoothies and soups.", Price = 59.99f, Quantity = 70, IsActive = true, Category = categories[3] },
    new Product { Id = Guid.NewGuid(), Name = "Cutlery Set", Description = "24-piece stainless steel set.", Price = 49.99f, Quantity = 90, IsActive = true, Category = categories[3] },

    // Sports (6)
    new Product { Id = Guid.NewGuid(), Name = "Basketball", Description = "Official size and weight.", Price = 24.99f, Quantity = 120, IsActive = true, Category = categories[4] },
    new Product { Id = Guid.NewGuid(), Name = "Tennis Racket", Description = "Lightweight with strong strings.", Price = 79.99f, Quantity = 60, IsActive = true, Category = categories[4] },
    new Product { Id = Guid.NewGuid(), Name = "Yoga Mat", Description = "Non-slip and durable.", Price = 29.99f, Quantity = 80, IsActive = true, Category = categories[4] },
    new Product { Id = Guid.NewGuid(), Name = "Football", Description = "Durable for all weather.", Price = 34.99f, Quantity = 70, IsActive = true, Category = categories[4] },
    new Product { Id = Guid.NewGuid(), Name = "Dumbbell Set", Description = "Adjustable weight set.", Price = 99.99f, Quantity = 50, IsActive = true, Category = categories[4] },
    new Product { Id = Guid.NewGuid(), Name = "Cycling Helmet", Description = "Lightweight and ventilated.", Price = 59.99f, Quantity = 40, IsActive = true, Category = categories[4] }
};
            foreach (var product in products)
            {
                _logger.LogInformation($"Adding product: {product.Name}");
                await _productRepo.AddAsync(product);
            }
            string userPasswordHash = BCrypt.Net.BCrypt.HashPassword("User123!");
            string adminPasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!");
            var users = new List<User>
{
    new User
    {
        Id = Guid.NewGuid(),
        UserName = "regularuser",
        Email = "user@example.com",
        PasswordHash = userPasswordHash, // replace with actual hash
        Role = "User"
    },
    new User
    {
        Id = Guid.NewGuid(),
        UserName = "adminuser",
        Email = "admin@example.com",
        PasswordHash = adminPasswordHash, // replace with actual hash
        Role = "Admin"
    }
};
            foreach (var user in users)
            {
                _logger.LogInformation($"Adding user: {user.UserName}");
                await _userRepo.AddAsync(user);
            }

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
