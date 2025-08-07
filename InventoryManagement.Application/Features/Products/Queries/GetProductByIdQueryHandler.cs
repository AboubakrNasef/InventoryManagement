using Mediator;
using InventoryManagment.DomainModels.Repositories;


namespace InventoryManagement.Application.Features.Products.Queries
{
    public record GetProductByIdQuery(int Id) : IQuery<ProductDto>;

    public class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByIdQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async ValueTask<ProductDto> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(query.Id);
            if (product == null) return null;
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.Category.Id
            };
        }
    }
}
