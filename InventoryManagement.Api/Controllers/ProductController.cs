using InventoryManagement.Application.Features.Products.Queries;
using InventoryManagement.Application.Features.Products.Commands;
using Microsoft.AspNetCore.Mvc;
using InventoryManagement.Application.Features.Products;
using Mediator;
using InventoryManagement.Infrastructure.Messaging.TopicMessages;
using InventoryManagment.DomainModels.Messaging;

namespace InventoryManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMessageBus _messageBus;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IMediator mediator, ILogger<ProductsController> logger, IMessageBus messageBus)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _messageBus = messageBus ?? throw new ArgumentNullException(nameof(messageBus));
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            using var _ = _logger.BeginScope("Getting all products");
            var products = await _mediator.Send(new GetAllProductsQuery());
            _logger.LogInformation("Retrieved {Count} products", products.Count());
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            _logger.LogInformation("Getting product by id: {Id}", id);
            var product = await _mediator.Send(new GetProductByIdQuery(id));
            if (product == null)
            {
                _logger.LogWarning("Product with id: {Id} not found", id);
                return NotFound();
            }
            _logger.LogInformation("Retrieved product: {@Product}", product);

            return Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ProductDto dto)
        {
            if (dto == null)
            {
                _logger.LogWarning("Invalid product data for update. DTO: {Dto}", dto);
                return BadRequest("Invalid product data.");
            }
            _logger.LogInformation("Updating product with id: {Id}", id);
            var command = new UpdateProductCommand(id, dto.Name, dto.Description, dto.Price, dto.Quantity, dto.CategoryId);
            var updated = await _mediator.Send(command);
            if (!updated)
            {
                _logger.LogWarning("Product with id: {Id} not found for update", id);
                return NotFound();
            }
            _logger.LogInformation("Product updated with id: {Id}", id);

            await _messageBus.SendToTopicAsync("ProductAction", new UpdateRedisTopicMessage(id, ProductAction.Update));
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _logger.LogInformation("Deleting product with id: {Id}", id);
            var deleted = await _mediator.Send(new DeleteProductCommand(id));
            if (!deleted)
            {
                _logger.LogWarning("Product with id: {Id} not found for deletion", id);
                return NotFound();
            }
            _logger.LogInformation("Product deleted with id: {Id}", id);

            await _messageBus.SendToTopicAsync("ProductAction", new UpdateRedisTopicMessage(id, ProductAction.Delete));
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProductDto dto)
        {
            if (dto == null)
            {
                _logger.LogWarning("Invalid product data for add. DTO: {Dto}", dto);
                return BadRequest("Invalid product data.");
            }
            _logger.LogInformation("Adding new product");
            var command = new CreateProductCommand(dto.Name, dto.Description, dto.Price, dto.CategoryId, dto.Quantity);
            var productId = await _mediator.Send(command);
            _logger.LogInformation("Product created with id: {Id}", productId);

            await _messageBus.SendToTopicAsync("ProductAction", new UpdateRedisTopicMessage(productId, ProductAction.Add));
            return CreatedAtAction(nameof(GetById), new { id = productId }, null);
        }
    }
}
