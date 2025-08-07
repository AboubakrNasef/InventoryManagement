using InventoryManagement.Application.Common;
using InventoryManagment.DomainModels.Repositories;
using Microsoft.Extensions.Logging;


namespace InventoryManagement.Application.Features.Categories.Commands
{
    public record UpdateCategoryCommand(int Id, string Name, string Description) : ICommand<bool>;

    public class UpdateCategoryCommandHandler : ICommandHandler<UpdateCategoryCommand, bool>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<UpdateCategoryCommandHandler> _logger;

        public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, ILogger<UpdateCategoryCommandHandler> logger)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        public async Task<bool> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Updating category: {command.Id}");
            var category = await _categoryRepository.GetByIdAsync(command.Id);
            if (category == null) return false;
            category.Name = command.Name;
            category.Description = command.Description;
            await _categoryRepository.UpdateAsync(category);
            return true;
        }
    }
}
