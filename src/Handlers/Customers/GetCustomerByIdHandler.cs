using BugStore.Infrastructure.Interfaces;
using BugStore.Requests.Customers;
using BugStore.Responses.Customers;
using MediatR;

namespace BugStore.Handlers.Customers;

public class GetCustomerByIdHandler : IRequestHandler<GetCustomerByIdRequest, GetCustomerByIdResponse?>
{
    private readonly ICustomerRepository _repository;

    public GetCustomerByIdHandler(ICustomerRepository repository) => _repository = repository;
    
    public async Task<GetCustomerByIdResponse?> Handle(GetCustomerByIdRequest request, CancellationToken cancellationToken)
    {
        var customer = await _repository.GetById(request.Id);

        if (customer is null) return null;

        return new GetCustomerByIdResponse
        {
            Id = customer.Id,
            Name = customer.Name,
            Email = customer.Email
        };
    }
}