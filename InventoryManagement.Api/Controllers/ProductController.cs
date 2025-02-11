using InventoryManagement.Application.DTO;
using InventoryManagment.DomainModels.Entites;
using InventoryManagment.DomainModels.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
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

            return CreatedAtAction(nameof(Add), new { id = product.Id }, product);
        }
    }
}
