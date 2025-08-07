using MediatR;

namespace InventoryManagement.Application.Common
{
    public interface ICommand<TResponse> : IRequest<TResponse>
    {
    }
}