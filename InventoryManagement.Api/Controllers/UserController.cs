using InventoryManagement.Application.Features.Users.Commands;
using InventoryManagement.Application.Features.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UserController> _logger;

        public UserController(IMediator mediator, ILogger<UserController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Getting all users");
            var users = await _mediator.Send(new GetAllUsersQuery());
            _logger.LogInformation("Retrieved {Count} users", users.Count);
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation("Getting user by id: {Id}", id);
            var user = await _mediator.Send(new GetUserByIdQuery(id));
            if (user == null)
            {
                _logger.LogWarning("User with id: {Id} not found", id);
                return NotFound();
            }
            _logger.LogInformation("Retrieved user: {@User}", user);
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateUserCommand command)
        {
            if (command == null)
            {
                _logger.LogWarning("Invalid user data for add. DTO: {Command}", command);
                return BadRequest("Invalid user data.");
            }
            _logger.LogInformation("Adding new user");
            var userId = await _mediator.Send(command);
            _logger.LogInformation("User created with id: {Id}", userId);
            return CreatedAtAction(nameof(GetById), new { id = userId }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUserCommand command)
        {
            if (command == null || id != command.Id)
            {
                _logger.LogWarning("Invalid user data for update. Id: {Id}, Command: {Command}", id, command);
                return BadRequest("Invalid user data.");
            }
            _logger.LogInformation("Updating user with id: {Id}", id);
            var updated = await _mediator.Send(command);
            if (!updated)
            {
                _logger.LogWarning("User with id: {Id} not found for update", id);
                return NotFound();
            }
            _logger.LogInformation("User updated with id: {Id}", id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Deleting user with id: {Id}", id);
            var deleted = await _mediator.Send(new DeleteUserCommand(id));
            if (!deleted)
            {
                _logger.LogWarning("User with id: {Id} not found for deletion", id);
                return NotFound();
            }
            _logger.LogInformation("User deleted with id: {Id}", id);
            return NoContent();
        }
    }
}
