using InventoryManagement.Application.Features.Users.Queries;
using Mediator;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using InventoryManagement.Application.Common;
using InventoryManagement.Application.Features.Users.Commands;
using InventoryManagement.Application.Features.Users;
using InventoryManagment.DomainModels.Entites;
using InventoryManagment.DomainModels.Repositories;

namespace InventoryManagement.Application.Features.Users.Commands
{
    public record RegisterUserCommand(string UserName, string Email, string Password) : IRequest<JwtTokenResult?>;
}

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, JwtTokenResult?>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterUserCommandHandler(IJwtTokenService jwtTokenService, IPasswordHasher passwordHasher, IUserRepository userRepository)
    {
        _jwtTokenService = jwtTokenService ?? throw new ArgumentNullException(nameof(jwtTokenService));
        _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async ValueTask<JwtTokenResult?> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {

        var existingUsers = await _userRepository.ExistsByEmailOrUserNameAsync(request.Email, request.UserName);
        if (existingUsers)
        {
            throw new InvalidOperationException("User with this email or username already exists.");
        }

        var passwordHash = _passwordHasher.HashPassword(request.Password);
        var user = new User() { Email = request.Email, UserName = request.UserName, PasswordHash = passwordHash, Role = "user" };
        await _userRepository.AddAsync(user);

        var token = _jwtTokenService.GenerateToken(user);
        return token;
    }

}
