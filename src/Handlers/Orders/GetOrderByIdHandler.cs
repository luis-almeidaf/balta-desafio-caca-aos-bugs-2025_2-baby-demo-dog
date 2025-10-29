using BugStore.Infrastructure.Interfaces;
using BugStore.Requests.Orders;
using BugStore.Responses.Orders;
using MediatR;

namespace BugStore.Handlers.Orders;

public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdRequest, GetOrderByIdResponse?>
{
    private readonly IOrderRepository _repository;

    public GetOrderByIdHandler(IOrderRepository repository) => _repository = repository;

    public async Task<GetOrderByIdResponse?> Handle(GetOrderByIdRequest request, CancellationToken cancellationToken)
    {
        var order = await _repository.GetById(request.Id);

        if (order is null) return null;

        return new GetOrderByIdResponse
        {
            Id = order.Id,
            CustomerId = order.CustomerId,
            CreatedAt = order.CreatedAt,
            UpdatedAt = order.UpdatedAt,
            Lines = order.Lines
        };
    }
}