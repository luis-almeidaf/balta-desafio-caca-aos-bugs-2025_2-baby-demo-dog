using BugStore.Data;
using BugStore.Infrastructure.Interfaces;
using BugStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BugStore.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _dbContext;

    public ProductRepository(AppDbContext dbContext) => _dbContext = dbContext;

    public async Task Add(Product product)
    {
        await _dbContext.Products.AddAsync(product);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<Product>> GetAll()
    {
        return await _dbContext.Products.ToListAsync();
    }

    public async Task<Product?> GetById(Guid id)
    {
        return await _dbContext.Products.FirstOrDefaultAsync(product => product.Id == id);
    }

    public async Task Update(Product product)
    {
        _dbContext.Products.Update(product);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var productToRemove = await _dbContext.Products.FindAsync(id);

        if (productToRemove is not null)
        {
            _dbContext.Products.Remove(productToRemove);
            await _dbContext.SaveChangesAsync();
        }
    }
}