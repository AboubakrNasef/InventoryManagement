using InventoryManagement.Application.Common;
using InventoryManagment.DomainModels.Interfaces;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Features.Products.Commands
{
    public record UpdateProductCommand(int Id, string Name, string Description, float Price, int Quantity, int CategoryId) : ICommand<bool>;

    public class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand, bool>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<UpdateProductCommandHandler> _logger;

        public UpdateProductCommandHandler(IProductRepository productRepository, ILogger<UpdateProductCommandHandler> logger, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }

        public async Task<bool> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Updating product: {command.Id}");
            var product = await _productRepository.GetByIdAsync(command.Id);
            var category = await _categoryRepository.GetByIdAsync(command.CategoryId);
            if (product == null) return false;
            product.Name = command.Name;
            product.Description = command.Description;
            product.Price = command.Price;
            product.Category = category;
            product.Quantity = command.Quantity;
            var result = await _productRepository.UpdateAsync(product);
            return result == 1;
        }
    }
}
