using BugStore.Responses.Orders;
using MediatR;

namespace BugStore.Requests.Orders;

public class GetOrderByIdRequest : IRequest<GetOrderByIdResponse?>
{
    public Guid Id { get; set; }
}