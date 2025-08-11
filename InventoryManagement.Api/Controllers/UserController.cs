using InventoryManagement.Application.Features.Users.Commands;
using InventoryManagement.Application.Features.Users.Queries;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using InventoryManagement.Application.Features.Users;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography;
using System.Text;
using InventoryManagement.Application.Common;

namespace InventoryManagement.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UserController> _logger;
        private readonly IJwtTokenService _jwtTokenService;

        public UserController(IMediator mediator, ILogger<UserController> logger, IJwtTokenService jwtTokenService)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _jwtTokenService = jwtTokenService ?? throw new ArgumentNullException(nameof(jwtTokenService));
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
        public async Task<IActionResult> GetById(Guid id)
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


        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.UserName) || string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
                return BadRequest("Invalid registration data.");
            var token = await _mediator.Send(new RegisterUserCommand(dto.UserName, dto.Email, dto.Password));
            if (token == null)
                return Conflict("Username or email already exists.");
            return Ok(token);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginUserDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.UserName) || string.IsNullOrWhiteSpace(dto.Password))
                return BadRequest("Invalid login data.");
            var token = await _mediator.Send(new LoginUserCommand(dto.UserName, dto.Password));
            if (token == null)
                return Unauthorized("Invalid username or password.");
            return Ok(token);
        }
    }
}

