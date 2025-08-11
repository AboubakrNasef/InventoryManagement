
using InventoryManagment.DomainModels.Entites;
using InventoryManagment.DomainModels.Repositories;
using Mediator;
using Microsoft.Extensions.Logging;

namespace InventoryManagement.Application.Features.Users.Queries
{
    public record GetUserByIdQuery(Guid Id) : IQuery<User>;

    public class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, User>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<GetUserByIdQueryHandler> _logger;

        public GetUserByIdQueryHandler(IUserRepository userRepository, ILogger<GetUserByIdQueryHandler> logger)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async ValueTask<User> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Getting user by id: {query.Id}");
            var user = await _userRepository.GetByIdAsync(query.Id);
            return user;
        }
    }
}
