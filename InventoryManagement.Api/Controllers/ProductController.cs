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
        private readonly IMessageBus _messageBus;

        public ProductsController(IProductRepository productRepository, IMessageBus messageBus)
        {
            _productRepository = productRepository;
            _messageBus = messageBus;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {

            var product = await _productRepository.GetByIdAsync(id);

            return Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductDto dto)
        {
            if (dto == null || id != dto.Id)
            {
                return BadRequest("Invalid product data.");
            }
            var product = new Product(id, dto.Name, dto.description, dto.Quantity, dto.Price);
            await _productRepository.UpdateAsync(product);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productRepository.DeleteAsync(id);
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProductDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid product data.");
            }
            var product = new Product(dto.Id, dto.Name, dto.description, dto.Quantity, dto.Price);
            await _productRepository.AddAsync(product);
            await _messageBus.SendToTopicAsync("UpdateRedisDB", new UpdateRedisTopicMessage(product.Id));
            return CreatedAtAction(nameof(Add), new { id = product.Id }, product);
        }
    }
}
