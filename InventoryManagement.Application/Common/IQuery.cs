using MediatR;

namespace InventoryManagement.Application.Common
{
    public interface IQuery<TResponse> : IRequest<TResponse>
    {
    }
}