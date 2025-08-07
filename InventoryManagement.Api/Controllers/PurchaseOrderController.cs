using InventoryManagement.Application.Features.PurchaseOrders.Commands;
using InventoryManagement.Application.Features.PurchaseOrders.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace InventoryManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchaseOrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<PurchaseOrderController> _logger;

        public PurchaseOrderController(IMediator mediator, ILogger<PurchaseOrderController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Getting all purchase orders");
            var orders = await _mediator.Send(new GetAllPurchaseOrdersQuery());
            _logger.LogInformation("Retrieved {Count} purchase orders", orders.Count);
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation("Getting purchase order by id: {Id}", id);
            var order = await _mediator.Send(new GetPurchaseOrderByIdQuery(id));
            if (order == null)
            {
                _logger.LogWarning("Purchase order with id: {Id} not found", id);
                return NotFound();
            }
            _logger.LogInformation("Retrieved purchase order: {@Order}", order);
            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreatePurchaseOrderCommand command)
        {
            if (command == null)
            {
                _logger.LogWarning("Invalid purchase order data for add. DTO: {Command}", command);
                return BadRequest("Invalid purchase order data.");
            }
            _logger.LogInformation("Adding new purchase order");
            var orderId = await _mediator.Send(command);
            _logger.LogInformation("Purchase order created with id: {Id}", orderId);
            return CreatedAtAction(nameof(GetById), new { id = orderId }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePurchaseOrderCommand command)
        {
            if (command == null || id != command.Id)
            {
                _logger.LogWarning("Invalid purchase order data for update. Id: {Id}, Command: {Command}", id, command);
                return BadRequest("Invalid purchase order data.");
            }
            _logger.LogInformation("Updating purchase order with id: {Id}", id);
            var updated = await _mediator.Send(command);
            if (!updated)
            {
                _logger.LogWarning("Purchase order with id: {Id} not found for update", id);
                return NotFound();
            }
            _logger.LogInformation("Purchase order updated with id: {Id}", id);
            return NoContent();
        }
    }
}
