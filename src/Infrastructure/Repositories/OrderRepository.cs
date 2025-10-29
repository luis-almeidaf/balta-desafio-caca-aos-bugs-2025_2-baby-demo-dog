using BugStore.Data;
using BugStore.Infrastructure.Interfaces;
using BugStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BugStore.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _dbContext;

    public OrderRepository(AppDbContext dbContext) => _dbContext = dbContext;

    public async Task Add(Order order)
    {
        await _dbContext.Orders.AddAsync(order);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Order?> GetById(Guid id)
    {
        return await GetOrderLines().FirstOrDefaultAsync(order => order.Id == id);
    }

    public async Task Delete(Guid id)
    {
        var orderToRemove = await _dbContext.Orders.FindAsync(id);

        if (orderToRemove is not null)
        {
            _dbContext.Orders.Remove(orderToRemove);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task Update(
        Guid orderId,
        Guid customerId,
        DateTime updatedAt,
        List<(Guid ProductId, int Quantity, decimal Total)> newLinesData)
    {
        var order = await _dbContext.Orders
            .Include(o => o.Lines)
            .FirstOrDefaultAsync(o => o.Id == orderId);

        if (order == null)
            throw new Exception($"Order {orderId} not found");

        order.CustomerId = customerId;
        order.UpdatedAt = updatedAt;
        
        using var transaction = await _dbContext.Database.BeginTransactionAsync();

        try
        {
            // 1️⃣ Remove todas as linhas antigas
            if (order.Lines.Any())
            {
                _dbContext.OrderLines.RemoveRange(order.Lines);
                await _dbContext.SaveChangesAsync();
            }

            // 2️⃣ Desanexa as entidades antigas do tracking do EF
            _dbContext.Entry(order).Collection(o => o.Lines).CurrentValue = new List<OrderLine>();
            _dbContext.ChangeTracker.DetectChanges();

            // 3️⃣ Adiciona as novas linhas
            var newLines = newLinesData.Select(line => new OrderLine
            {
                Id = Guid.NewGuid(),
                OrderId = order.Id,
                ProductId = line.ProductId,
                Quantity = line.Quantity,
                Total = line.Total
            }).ToList();

            await _dbContext.OrderLines.AddRangeAsync(newLines);

            // 4️⃣ Atualiza o pedido
            await _dbContext.SaveChangesAsync();

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
    
    private IQueryable<Order> GetOrderLines()
    {
        return _dbContext.Orders.Include(order => order.Lines).ThenInclude(line => line.Product);
    }
}