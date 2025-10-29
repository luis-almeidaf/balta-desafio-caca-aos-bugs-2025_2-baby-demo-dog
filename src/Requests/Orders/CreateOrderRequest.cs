using BugStore.Models;
using BugStore.Responses.Orders;
using MediatR;

namespace BugStore.Requests.Orders;

public class CreateOrderRequest : IRequest<CreateOrderResponse>
{
    public Guid CustomerId { get; set; }
    public List<OrderLine> Lines { get; set; } = null!;
}