using InventoryManagement.Application.Common;
using InventoryManagment.DomainModels.Interfaces;


namespace InventoryManagement.Application.Features.Categories.Queries
{
    public record GetCategoryByIdQuery(int Id) : IQuery<CategoryDto>;

    public class GetCategoryByIdQueryHandler : IQueryHandler<GetCategoryByIdQuery, CategoryDto>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryDto> Handle(GetCategoryByIdQuery query, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(query.Id);
            if (category == null) return null;
            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }
    }
}
