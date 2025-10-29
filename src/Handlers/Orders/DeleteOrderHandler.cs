using BugStore.Infrastructure.Interfaces;
using BugStore.Requests.Orders;
using MediatR;

namespace BugStore.Handlers.Orders;

public class DeleteOrderHandler : IRequestHandler<DeleteOrderRequest, Unit>
{    
    private readonly IOrderRepository _repository;
    
    public DeleteOrderHandler(IOrderRepository repository)
    {
        _repository = repository;
    }
    public async Task<Unit> Handle(DeleteOrderRequest request, CancellationToken cancellationToken)
    {
        await _repository.Delete(request.Id);
        
        return Unit.Value;
    }
}