using InventoryManagment.DomainModels.Entites;
using InventoryManagment.DomainModels.Repositories;
using Mediator;
using Microsoft.Extensions.Logging;


namespace InventoryManagement.Application.Features.Products.Commands
{
    public record CreateProductCommand(string Name, string Description, float Price, int CategoryId, int Quantity) : ICommand<int>;

    public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, int>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<CreateProductCommandHandler> _logger;

        public CreateProductCommandHandler(IProductRepository productRepository, ILogger<CreateProductCommandHandler> logger, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }


        public async ValueTask<int> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Creating product: {command.Name}");

            // Check if the category exists
            var categoryExists = await _productRepository.GetByIdAsync(command.CategoryId) != null;
            if (!categoryExists)
            {
                _logger.LogWarning($"Category with ID {command.CategoryId} does not exist.");
                throw new ArgumentException($"Category with ID {command.CategoryId} does not exist.");
            }
            var category = await _categoryRepository.GetByIdAsync(command.CategoryId);
            var product = new Product
            {
                Name = command.Name,
                Description = command.Description,
                Price = command.Price,
                Category = category
            };
            await _productRepository.AddAsync(product);
            return product.Id;
        }
    }
}
