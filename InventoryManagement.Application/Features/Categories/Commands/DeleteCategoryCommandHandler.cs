using InventoryManagement.Application.Common;
using InventoryManagment.DomainModels.Interfaces;
using Microsoft.Extensions.Logging;


namespace InventoryManagement.Application.Features.Categories.Commands
{
    public record DeleteCategoryCommand(int Id) : ICommand<bool>;

    public class DeleteCategoryCommandHandler : ICommandHandler<DeleteCategoryCommand, bool>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<DeleteCategoryCommandHandler> _logger;

        public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository, ILogger<DeleteCategoryCommandHandler> logger)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        public async Task<bool> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Deleting category: {command.Id}");
            var category = await _categoryRepository.GetByIdAsync(command.Id);
            if (category == null) return false;
            await _categoryRepository.DeleteAsync(command.Id);
            return true;
        }
    }
}
