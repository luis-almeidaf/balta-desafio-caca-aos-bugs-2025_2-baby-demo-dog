using BugStore.Models;

namespace BugStore.Infrastructure.Interfaces;

public interface IProductRepository
{
    Task Add(Product product);
    Task<List<Product>> GetAll();
    Task<Product?> GetById(Guid id);
    Task Update(Product product);
    Task Delete(Guid id);
}