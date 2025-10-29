using BugStore.Infrastructure.Interfaces;
using BugStore.Requests.Products;
using MediatR;

namespace BugStore.Handlers.Products;

public class DeleteProductHandler : IRequestHandler<DeleteProductRequest, Unit>
{
    private readonly IProductRepository _repository;

    public DeleteProductHandler(IProductRepository repository) => _repository = repository;

    public async Task<Unit> Handle(DeleteProductRequest request, CancellationToken cancellationToken)
    {
        await _repository.Delete(request.Id);

        return Unit.Value;
    }
}