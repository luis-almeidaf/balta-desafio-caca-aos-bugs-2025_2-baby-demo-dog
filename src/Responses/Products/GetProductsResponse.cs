using BugStore.Models;
using MediatR;

namespace BugStore.Responses.Products;

public class GetProductsResponse
{
    public List<Product> Products { get; set; } = [];
}