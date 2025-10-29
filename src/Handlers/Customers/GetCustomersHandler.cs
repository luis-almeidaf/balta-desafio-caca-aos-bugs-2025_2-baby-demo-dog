using BugStore.Infrastructure.Interfaces;
using BugStore.Requests.Customers;
using BugStore.Responses.Customers;
using MediatR;

namespace BugStore.Handlers.Customers;

public class GetCustomersHandler : IRequestHandler<GetCustomersRequest, GetCustomersResponse?>
{
    private readonly ICustomerRepository _repository;

    public GetCustomersHandler(ICustomerRepository repository)
    {
        _repository = repository;
    }

    public async Task<GetCustomersResponse?> Handle(GetCustomersRequest? request, CancellationToken cancellationToken)
    {
        var customers = await _repository.GetAll();

        return new GetCustomersResponse
        {
            Customers = customers
        };
    }
}