using BugStore.Responses.Customers;
using MediatR;

namespace BugStore.Requests.Customers;

public class CreateCustomerRequest : IRequest<CreateCustomerResponse>
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
}