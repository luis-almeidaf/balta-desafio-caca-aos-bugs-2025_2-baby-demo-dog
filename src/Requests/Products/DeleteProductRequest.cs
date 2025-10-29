using MediatR;

namespace BugStore.Requests.Products;

public class DeleteProductRequest : IRequest<Unit>
{
    public Guid Id { get; set; }
}