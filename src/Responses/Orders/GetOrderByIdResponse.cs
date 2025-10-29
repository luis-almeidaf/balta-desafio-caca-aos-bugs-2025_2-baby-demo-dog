using BugStore.Models;

namespace BugStore.Responses.Orders;

public class GetOrderByIdResponse
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public List<OrderLine> Lines { get; set; } = null!;
}