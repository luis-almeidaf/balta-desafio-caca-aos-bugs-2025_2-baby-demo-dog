using BugStore.Infrastructure.Interfaces;
using BugStore.Requests.Orders;
using MediatR;

namespace BugStore.Handlers.Orders;

public class UpdateOrderHandler : IRequestHandler<UpdateOrderRequest, Unit>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;

    public UpdateOrderHandler(
        ICustomerRepository customerRepository,
        IOrderRepository orderRepository,
        IProductRepository productRepository)
    {
        _customerRepository = customerRepository;
        _orderRepository = orderRepository;
        _productRepository = productRepository;
    }

    public async Task<Unit> Handle(UpdateOrderRequest request, CancellationToken cancellationToken)
    {
        // Validate customer if changed - but don't load the order yet!
        var customer = await _customerRepository.GetById(request.CustomerId);
        if (customer == null)
            throw new Exception($"Customer {request.CustomerId} not found");

        var newLinesData = new List<(Guid ProductId, int Quantity, decimal Total)>();
    
        foreach (var orderLine in request.Lines)
        {
            var product = await _productRepository.GetById(orderLine.ProductId);
            if (product == null)
                throw new Exception($"Product {orderLine.ProductId} not found");

            var total = product.Price * orderLine.Quantity;
            newLinesData.Add((product.Id, orderLine.Quantity, total));
        }

        // Let the repository handle loading and updating
        await _orderRepository.Update(request.Id, request.CustomerId, DateTime.Now, newLinesData);

        return Unit.Value;
    }
}