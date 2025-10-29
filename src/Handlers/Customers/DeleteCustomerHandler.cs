using BugStore.Infrastructure.Interfaces;
using BugStore.Requests.Customers;
using MediatR;

namespace BugStore.Handlers.Customers;

public class DeleteCustomerHandler : IRequestHandler<DeleteCustomerRequest, Unit>
{
    private readonly ICustomerRepository _repository;

    public DeleteCustomerHandler(ICustomerRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(DeleteCustomerRequest request, CancellationToken cancellationToken)
    {
        await _repository.Delete(request.Id);
        
        return Unit.Value;
    }
}