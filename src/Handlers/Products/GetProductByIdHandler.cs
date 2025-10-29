using BugStore.Infrastructure.Interfaces;
using BugStore.Requests.Products;
using BugStore.Responses.Products;
using MediatR;

namespace BugStore.Handlers.Products;

public class GetProductByIdHandler : IRequestHandler<GetProductsByIdRequest, GetProductByIdResponse?>
{
    private readonly IProductRepository _repository;

    public GetProductByIdHandler(IProductRepository repository) => _repository = repository;


    public async Task<GetProductByIdResponse?> Handle(GetProductsByIdRequest request,
        CancellationToken cancellationToken)
    {
        var product = await _repository.GetById(request.Id);

        if (product is null) return null;

        return new GetProductByIdResponse
        {
            Id = product.Id,
            Title = product.Title,
            Description = product.Description,
            Slug = product.Slug,
            Price = product.Price
        };
    }
}