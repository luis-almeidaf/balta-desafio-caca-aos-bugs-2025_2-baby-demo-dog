using MediatR;

namespace BugStore.Requests.Orders;

public class DeleteOrderRequest : IRequest<Unit>
{
    public Guid Id { get; set; }
}