using BugStore.Infrastructure.Interfaces;
using BugStore.Models;
using BugStore.Requests.Products;
using MediatR;

namespace BugStore.Handlers.Products;

public class UpdateProductHandler : IRequestHandler<UpdateProductRequest, Unit>
{
    private readonly IProductRepository _repository;
    public UpdateProductHandler(IProductRepository repository) => _repository = repository;

    public Task<Unit> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Id = request.Id,
            Title = request.Title,
            Description = request.Description,
            Slug = request.Slug,
            Price = request.Price
        };

        _repository.Update(product);

        return Task.FromResult(Unit.Value);
    }
}