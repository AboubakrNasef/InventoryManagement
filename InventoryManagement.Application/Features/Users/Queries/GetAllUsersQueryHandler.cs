
using InventoryManagment.DomainModels.Entites;
using InventoryManagment.DomainModels.Repositories;
using Mediator;
using Microsoft.Extensions.Logging;

namespace InventoryManagement.Application.Features.Users.Queries
{
    public record GetAllUsersQuery() : IQuery<List<User>>;

    public class GetAllUsersQueryHandler : IQueryHandler<GetAllUsersQuery, List<User>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<GetAllUsersQueryHandler> _logger;

        public GetAllUsersQueryHandler(IUserRepository userRepository, ILogger<GetAllUsersQueryHandler> logger)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async ValueTask<List<User>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Getting all users");
            var users = await _userRepository.GetAllAsync();
            return users.ToList();
        }
    }
}
