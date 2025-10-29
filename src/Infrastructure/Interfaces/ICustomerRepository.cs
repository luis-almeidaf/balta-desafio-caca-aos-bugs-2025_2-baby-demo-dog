using BugStore.Models;
using BugStore.Requests.Customers;

namespace BugStore.Infrastructure.Interfaces;

public interface ICustomerRepository
{
    Task<List<Customer>> GetAll();
    Task<Customer?> GetById(Guid id);
    Task Add(Customer customer);
    Task Update(Customer customer);
    Task Delete(Guid id);
}