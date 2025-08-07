using Mediator;
using InventoryManagment.DomainModels.Repositories;
using Microsoft.Extensions.Logging;


namespace InventoryManagement.Application.Features.Users.Commands
{
    public record UpdateUserCommand(int Id, string UserName, string Email) : ICommand<bool>;

    public class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UpdateUserCommandHandler> _logger;

        public UpdateUserCommandHandler(IUserRepository userRepository, ILogger<UpdateUserCommandHandler> logger)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _logger = logger;
        }

        public async ValueTask<bool> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Updating user: {command.Id}");
            var user = await _userRepository.GetByIdAsync(command.Id);
            if (user == null)
            {
                return false;
            }

            user.UserName = command.UserName;
            user.Email = command.Email;

            await _userRepository.UpdateAsync(user);
            return true;
        }
    }
}
