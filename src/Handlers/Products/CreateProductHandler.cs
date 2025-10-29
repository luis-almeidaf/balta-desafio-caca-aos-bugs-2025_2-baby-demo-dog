using BugStore.Infrastructure.Interfaces;
using BugStore.Models;
using BugStore.Requests.Products;
using BugStore.Responses.Products;
using MediatR;

namespace BugStore.Handlers.Products;

public class CreateProductHandler : IRequestHandler<CreateProductRequest, CreateProductResponse>
{
    private readonly IProductRepository _repository;

    public CreateProductHandler(IProductRepository repository) => _repository = repository;

    public async Task<CreateProductResponse> Handle(CreateProductRequest request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            Slug = request.Slug,
            Price = request.Price
        };

        await _repository.Add(product);

        var result = new CreateProductResponse
        {
            Id = product.Id,
            Title = product.Title,
            Slug = product.Slug,
            Price = product.Price
        };

        return result;
    }
}