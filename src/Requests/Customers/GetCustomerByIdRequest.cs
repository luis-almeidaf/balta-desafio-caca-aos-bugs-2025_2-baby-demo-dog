using BugStore.Responses.Customers;
using MediatR;

namespace BugStore.Requests.Customers;

public class GetCustomerByIdRequest : IRequest<GetCustomerByIdResponse?>
{
    public Guid Id { get; set; }
}