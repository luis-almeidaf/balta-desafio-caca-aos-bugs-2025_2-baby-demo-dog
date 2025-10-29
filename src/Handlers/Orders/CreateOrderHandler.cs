using BugStore.Infrastructure.Interfaces;
using BugStore.Models;
using BugStore.Requests.Orders;
using BugStore.Responses.Orders;
using MediatR;

namespace BugStore.Handlers.Orders;

public class CreateOrderHandler : IRequestHandler<CreateOrderRequest, CreateOrderResponse>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;

    public CreateOrderHandler(
        IOrderRepository orderRepository,
        ICustomerRepository customerRepository,
        IProductRepository productRepository
        )
    {
        _customerRepository = customerRepository;
        _productRepository = productRepository;
        _orderRepository = orderRepository;
    }

    public async Task<CreateOrderResponse> Handle(CreateOrderRequest request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetById(request.CustomerId);
        if (customer == null)
            throw new Exception($"Customer {request.CustomerId} not found");

        var orderLines = new List<OrderLine>();

        foreach (var orderLine in request.Lines)
        {
            var product = await _productRepository.GetById(orderLine.ProductId);

            var total = product!.Price * orderLine.Quantity;
            
            orderLines.Add(new OrderLine
            {
                Id = Guid.NewGuid(),
                Product = product,
                ProductId = product.Id,
                Quantity = orderLine.Quantity,
                Total = total
            });
        }
        
        var order = new Order
        {
            Id = Guid.NewGuid(),
            CustomerId = customer.Id,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            Lines = orderLines
        };
        
        await _orderRepository.Add(order);

        return new CreateOrderResponse
        {
            Id = order.Id,
            CustomerId = order.CustomerId,
            CreatedAt = order.CreatedAt,
            Lines = order.Lines
        };
    }
}