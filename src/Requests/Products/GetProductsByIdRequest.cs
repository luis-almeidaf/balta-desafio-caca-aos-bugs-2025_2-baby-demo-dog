using BugStore.Responses.Products;
using MediatR;

namespace BugStore.Requests.Products;

public class GetProductsByIdRequest : IRequest<GetProductByIdResponse?>
{
    public Guid Id { get; set; }
}