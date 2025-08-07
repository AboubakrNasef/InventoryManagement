using InventoryManagement.Application.Common;
using InventoryManagment.DomainModels.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace InventoryManagement.Application.Features.Users.Commands
{
    public record DeleteUserCommand(int Id) : ICommand<bool>;

    public class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<DeleteUserCommandHandler> _logger;

        public DeleteUserCommandHandler(IUserRepository userRepository, ILogger<DeleteUserCommandHandler> logger)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Deleting user: {command.Id}");
            var result = await _userRepository.DeleteAsync(command.Id);
            if (result == 1)
            {
                _logger.LogInformation($"User deleted with id {command.Id}");
                return true;
            }
            _logger.LogWarning($"User with id {command.Id} not found for deletion");
            return false;
        }
    }
}
