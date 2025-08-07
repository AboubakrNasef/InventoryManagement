using InventoryManagement.Application.DTO;
using InventoryManagement.Infrastructure.Messaging.TopicMessages;
using InventoryManagment.DomainModels.Entites;
using InventoryManagment.DomainModels.Interfaces;
using InventoryManagment.DomainModels.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductSearchRepository _productSearchRepository;
        private readonly IMessageBus _messageBus;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductRepository productRepository, IProductSearchRepository productSearchRepository, IMessageBus messageBus, ILogger<ProductsController> logger)
        {
            _productRepository = productRepository;
            _productSearchRepository = productSearchRepository;
            _messageBus = messageBus;
            _logger = logger;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Getting all products");
            var products = await _productSearchRepository.GetAllAsync();
            _logger.LogInformation("Retrieved {Count} products", products.Count);

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation("Getting product by id: {Id}", id);
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                _logger.LogWarning("Product with id: {Id} not found", id);
                return NotFound();
            }
            _logger.LogInformation("Retrieved product: {Product}", product);

            return Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductDto dto)
        {
            if (dto == null || id != dto.Id)
            {
                _logger.LogWarning("Invalid product data for update. Id: {Id}, DTO: {Dto}", id, dto);
                return BadRequest("Invalid product data.");
            }
            _logger.LogInformation("Updating product with id: {Id}", id);
            var product = new Product()
            {
                Name = dto.Name,
                Description = dto.description,
                Quantity = dto.Quantity,
                Price = dto.Price
            };
            await _productRepository.UpdateAsync(product);
            _logger.LogInformation("Sending Updated product with id: {Id}", id);
            await _messageBus.SendToTopicAsync("ProductAction", new UpdateRedisTopicMessage(id, ProductAction.Update));

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Deleting product with id: {Id}", id);
            await _productRepository.DeleteAsync(id);
            _logger.LogInformation("Sending Deleted product with id: {Id}", id);
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
            var product = new Product(dto.Id, dto.Name, dto.description, dto.Quantity, dto.Price);
            await _productRepository.AddAsync(product);
            _logger.LogInformation("Sending Added new product with id: {Id}", product.Id);
            await _messageBus.SendToTopicAsync("ProductAction", new UpdateRedisTopicMessage(product.Id, ProductAction.Add));

            return CreatedAtAction(nameof(Add), new { id = product.Id }, product);
        }
    }
}
