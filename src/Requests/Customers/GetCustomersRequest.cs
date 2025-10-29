using BugStore.Responses.Customers;
using MediatR;

namespace BugStore.Requests.Customers;

public class GetCustomersRequest : IRequest<GetCustomersResponse?> { }