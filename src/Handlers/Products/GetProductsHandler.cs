using BugStore.Infrastructure.Interfaces;
using BugStore.Requests.Products;
using BugStore.Responses.Products;
using MediatR;

namespace BugStore.Handlers.Products;

public class GetProductsHandler : IRequestHandler<GetProductsRequest, GetProductsResponse?>
{
    private readonly IProductRepository _repository;

    public GetProductsHandler(IProductRepository repository) => _repository = repository;


    public async Task<GetProductsResponse?> Handle(GetProductsRequest request, CancellationToken cancellationToken)
    {
        var products = await _repository.GetAll();

        return new GetProductsResponse
        {
            Products = products
        };
    }
}