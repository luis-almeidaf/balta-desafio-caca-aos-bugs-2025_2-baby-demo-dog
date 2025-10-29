using BugStore.Data;
using BugStore.Infrastructure.Interfaces;
using BugStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BugStore.Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly AppDbContext _dbContext;

    public CustomerRepository(AppDbContext dbContext) => _dbContext = dbContext;

    public async Task<Customer?> GetById(Guid id)
    {
        return await _dbContext.Customers.FirstOrDefaultAsync(customer => customer.Id == id);
    }

    public async Task<List<Customer>> GetAll()
    {
        return await _dbContext.Customers.ToListAsync();
    }

    public async Task Add(Customer customer)
    {
        await _dbContext.Customers.AddAsync(customer);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(Customer customer)
    {
        _dbContext.Customers.Update(customer);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var customerToRemove = await _dbContext.Customers.FindAsync(id);

        if (customerToRemove is not null)
        {
            _dbContext.Customers.Remove(customerToRemove);
            await _dbContext.SaveChangesAsync();
        }
    }
}