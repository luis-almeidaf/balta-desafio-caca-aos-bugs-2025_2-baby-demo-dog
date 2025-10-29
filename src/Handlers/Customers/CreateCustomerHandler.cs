using BugStore.Infrastructure.Interfaces;
using BugStore.Models;
using BugStore.Requests.Customers;
using BugStore.Responses.Customers;
using MediatR;

namespace BugStore.Handlers.Customers;

public class CreateCustomerHandler : IRequestHandler<CreateCustomerRequest, CreateCustomerResponse>
{
    private readonly ICustomerRepository _repository;

    public CreateCustomerHandler(ICustomerRepository repository) => _repository = repository;

    public async Task<CreateCustomerResponse> Handle(CreateCustomerRequest request, CancellationToken cancellationToken)
    {
        var customer = new Customer
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            Name = request.Name,
            Phone = request.Phone,
            BirthDate = request.BirthDate
        };

        await _repository.Add(customer);

        var result = new CreateCustomerResponse
        {
            Id = customer.Id,
            Email = customer.Email,
            Name = customer.Name
        };

        return await Task.FromResult(result);
    }
}