using InventoryManagement.Application.Common;
using InventoryManagment.DomainModels.Entites;
using InventoryManagment.DomainModels.Repositories;
using Microsoft.Extensions.Logging;

namespace InventoryManagement.Application.Features.Categories.Commands
{
    public record CreateCategoryCommand(string Name, string Description) : ICommand<int>;

    public class CreateCategoryCommandHandler : ICommandHandler<CreateCategoryCommand, int>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<CreateCategoryCommandHandler> _logger;

        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, ILogger<CreateCategoryCommandHandler> logger)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        public async Task<int> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Creating category: {command.Name}");
            var category = new Category
            {
                Name = command.Name,
                Description = command.Description
            };
            await _categoryRepository.AddAsync(category);
            return category.Id;
        }
    }
}
