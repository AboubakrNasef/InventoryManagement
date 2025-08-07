using InventoryManagement.Application.Common;
using InventoryManagment.DomainModels.Repositories;
using Microsoft.Extensions.Logging;


namespace InventoryManagement.Application.Features.Products.Commands
{
    public record DeleteProductCommand(int Id) : ICommand<bool>;

    public class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand, bool>
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<DeleteProductCommandHandler> _logger;

        public DeleteProductCommandHandler(IProductRepository productRepository, ILogger<DeleteProductCommandHandler> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task<bool> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Deleting product: {command.Id}");
            var product = await _productRepository.GetByIdAsync(command.Id);
            if (product == null) return false;
            await _productRepository.DeleteAsync(command.Id);
            return true;
        }
    }
}
