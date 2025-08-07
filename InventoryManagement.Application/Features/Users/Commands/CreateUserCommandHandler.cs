using InventoryManagement.Application.Common;
using InventoryManagment.DomainModels.Entites;
using InventoryManagment.DomainModels.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace InventoryManagement.Application.Features.Users.Commands
{
    public record CreateUserCommand(string UserName, string Email, string Password) : ICommand<int>;

    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, int>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<CreateUserCommandHandler> _logger;

        public CreateUserCommandHandler(IUserRepository userRepository, ILogger<CreateUserCommandHandler> logger)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<int> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Creating user: {command.UserName}");
            var user = new User
            {
                UserName = command.UserName,
                Email = command.Email,
                PasswordHash = command.Password
            };
            await _userRepository.AddAsync(user);
            _logger.LogInformation($"User created with id {user.Id}");
            return user.Id;
        }
    }
}
