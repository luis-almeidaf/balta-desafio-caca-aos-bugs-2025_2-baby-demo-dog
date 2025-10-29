using BugStore.Models;

namespace BugStore.Responses.Customers;

public class GetCustomersResponse
{
    public List<Customer> Customers { get; set; } = [];
}