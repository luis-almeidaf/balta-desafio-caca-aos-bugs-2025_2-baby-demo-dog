using BugStore.Infrastructure.Interfaces;
using MediatR;

namespace BugStore.Requests.Customers;

public class DeleteCustomerRequest : IRequest<Unit>
{
    public Guid Id { get; set; }
}