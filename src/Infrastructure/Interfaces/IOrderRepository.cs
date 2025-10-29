using BugStore.Models;

namespace BugStore.Infrastructure.Interfaces;

public interface IOrderRepository
{
    Task Add(Order order);
    Task<Order?> GetById(Guid id);
    Task Delete(Guid id);

    Task Update(
        Guid orderId,
        Guid customerId,
        DateTime updatedAt,
        List<(Guid ProductId, int Quantity, decimal Total)> newLinesData);
}