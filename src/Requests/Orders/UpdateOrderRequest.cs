using BugStore.Models;
using MediatR;

namespace BugStore.Requests.Orders;

public class UpdateOrderRequest : IRequest<Unit>
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public List<OrderLine> Lines { get; set; } = null!;
}