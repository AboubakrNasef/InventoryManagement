using InventoryManagement.Application.Features.Users.Queries;

using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Mediator;
using InventoryManagement.Application.Common;
using InventoryManagment.DomainModels.Repositories;

namespace InventoryManagement.Application.Features.Users.Commands
{
    public record LoginUserCommand(string UserName, string Password) : IRequest<JwtTokenResult?>;


    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, JwtTokenResult?>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IPasswordHasher _passwordHasher;

        public LoginUserCommandHandler(IUserRepository userRepository, IJwtTokenService jwtTokenService, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _jwtTokenService = jwtTokenService ?? throw new ArgumentNullException(nameof(jwtTokenService));
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        }

        public async ValueTask<JwtTokenResult?> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByUsernameAsync(request.UserName);
            if (user == null || !VerifyPassword(request.Password, user.PasswordHash))
                throw new InvalidOperationException("Invalid username or password.");
            var token = _jwtTokenService.GenerateToken(user);
            return token;
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            var hash = _passwordHasher.HashPassword(password);
            return hash == storedHash;
        }
    }
}
