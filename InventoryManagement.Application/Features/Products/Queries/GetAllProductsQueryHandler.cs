using Mediator;
using InventoryManagment.DomainModels.Repositories;


namespace InventoryManagement.Application.Features.Products.Queries
{
    public record GetAllProductsQuery : IQuery<List<ProductDto>>;

    public class GetAllProductsQueryHandler : IQueryHandler<GetAllProductsQuery, List<ProductDto>>
    {
        private readonly IProductRepository _productRepository;

        public GetAllProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async ValueTask<List<ProductDto>> Handle(GetAllProductsQuery query, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllAsync();
            return products.Select(product => new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.Category.Id,
                CategoryName = product.Category.Name
            }).ToList();
        }
    }
}
