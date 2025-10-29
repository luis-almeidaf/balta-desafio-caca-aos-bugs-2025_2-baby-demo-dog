using BugStore.Responses.Products;
using MediatR;

namespace BugStore.Requests.Products;

public class CreateProductRequest : IRequest<CreateProductResponse>
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public decimal Price { get; set; }
}