using InventoryManagement.Application.Features.Categories.Commands;
using InventoryManagement.Application.Features.Categories.Queries;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(IMediator mediator, ILogger<CategoryController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Getting all categories");
            var categories = await _mediator.Send(new GetAllCategoriesQuery());
            _logger.LogInformation("Retrieved {Count} categories", categories.Count);
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            _logger.LogInformation("Getting category by id: {Id}", id);
            var category = await _mediator.Send(new GetCategoryByIdQuery(id));
            if (category == null)
            {
                _logger.LogWarning("Category with id: {Id} not found", id);
                return NotFound();
            }
            _logger.LogInformation("Retrieved category: {@Category}", category);
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateCategoryCommand command)
        {
            if (command == null)
            {
                _logger.LogWarning("Invalid category data for add. DTO: {Command}", command);
                return BadRequest("Invalid category data.");
            }
            _logger.LogInformation("Adding new category");
            var categoryId = await _mediator.Send(command);
            _logger.LogInformation("Category created with id: {Id}", categoryId);
            return CreatedAtAction(nameof(GetById), new { id = categoryId }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCategoryCommand command)
        {
            if (command == null || id != command.Id)
            {
                _logger.LogWarning("Invalid category data for update. Id: {Id}, Command: {Command}", id, command);
                return BadRequest("Invalid category data.");
            }
            _logger.LogInformation("Updating category with id: {Id}", id);
            var updated = await _mediator.Send(command);
            if (!updated)
            {
                _logger.LogWarning("Category with id: {Id} not found for update", id);
                return NotFound();
            }
            _logger.LogInformation("Category updated with id: {Id}", id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _logger.LogInformation("Deleting category with id: {Id}", id);
            var deleted = await _mediator.Send(new DeleteCategoryCommand(id));
            if (!deleted)
            {
                _logger.LogWarning("Category with id: {Id} not found for deletion", id);
                return NotFound();
            }
            _logger.LogInformation("Category deleted with id: {Id}", id);
            return NoContent();
        }
    }
}
