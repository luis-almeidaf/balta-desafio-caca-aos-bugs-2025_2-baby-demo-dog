using BugStore.Infrastructure.Interfaces;
using BugStore.Models;
using BugStore.Requests.Customers;
using MediatR;

namespace BugStore.Handlers.Customers;

public class UpdateCustomerHandler : IRequestHandler<UpdateCustomerRequest, Unit>
{
    private readonly ICustomerRepository _repository;

    public UpdateCustomerHandler(ICustomerRepository repository) => _repository = repository;

    public Task<Unit> Handle(UpdateCustomerRequest request, CancellationToken cancellationToken)
    {
        var customer = new Customer
        {
            Id = request.Id,
            Name = request.Name,
            Email = request.Email,
            Phone = request.Phone,
            BirthDate = request.BirthDate
        };
        
        _repository.Update(customer);

        return Task.FromResult(Unit.Value);
    }
}