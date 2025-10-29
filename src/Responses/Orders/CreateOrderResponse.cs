using BugStore.Models;

namespace BugStore.Responses.Orders;

public class CreateOrderResponse
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<OrderLine> Lines { get; set; } = [];
}