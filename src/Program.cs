using BugStore.Data;
using BugStore.Infrastructure.Interfaces;
using BugStore.Infrastructure.Repositories;
using BugStore.Requests.Customers;
using BugStore.Requests.Orders;
using BugStore.Requests.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(config => config.UseSqlite(connectionString));

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(typeof(Program).Assembly); });

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/v1/customers", async (IMediator mediator) =>
{
    var result = await mediator.Send(new GetCustomersRequest());
    return result is null ? Results.NotFound() : Results.Ok(result);
});

app.MapGet("/v1/customers/{id:guid}", async (Guid id, IMediator mediator) =>
{
    var result = await mediator.Send(new GetCustomerByIdRequest { Id = id });
    return result is null ? Results.NotFound() : Results.Ok(result);
});

app.MapPost("/v1/customers", async (CreateCustomerRequest request, IMediator mediator) =>
{
    var response = await mediator.Send(request);
    return Results.Created($"/v1/customers/{response.Id}", response);
});

app.MapPut("/v1/customers/{id:guid}", async (Guid id, UpdateCustomerRequest request, IMediator mediator) =>
{
    request.Id = id;
    await mediator.Send(request);
    return Results.NoContent();
});

app.MapDelete("/v1/customers/{id:guid}", async (Guid id, IMediator mediator) =>
{
    await mediator.Send(new DeleteCustomerRequest { Id = id });
    return Results.NoContent();
});

app.MapGet("/v1/products", async (IMediator mediator) =>
{
    var result = await mediator.Send(new GetProductsRequest());
    return result is null ? Results.NotFound() : Results.Ok(result);
});

app.MapGet("/v1/products/{id:guid}", async (Guid id, IMediator mediator) =>
{
    var result = await mediator.Send(new GetProductsByIdRequest { Id = id });
    return result is null ? Results.NotFound() : Results.Ok(result);
});

app.MapPost("/v1/products", async (CreateProductRequest request, IMediator mediator) =>
{
    var response = await mediator.Send(request);
    return Results.Created($"/v1/products/{response.Id}", response);
});

app.MapPut("/v1/products/{id:guid}", async (Guid id, UpdateProductRequest request, IMediator mediator) =>
{
    request.Id = id;
    await mediator.Send(request);
    return Results.NoContent();
});

app.MapDelete("/v1/products/{id:guid}", async (Guid id, IMediator mediator) =>
{
    await mediator.Send(new DeleteProductRequest { Id = id });
    return Results.NoContent();
});

app.MapGet("/v1/orders/{id:guid}", async (Guid id, IMediator mediator) =>
{
    var result = await mediator.Send(new GetOrderByIdRequest { Id = id });
    return result is null ? Results.NotFound() : Results.Ok(result);
});

app.MapPost("/v1/orders", async (CreateOrderRequest request, IMediator mediator) =>
{
    var response = await mediator.Send(request);
    return Results.Created($"v1/orders/{response.Id}", response);
});

app.MapPut("/v1/orders/{id:guid}", async (Guid id, UpdateOrderRequest request, IMediator mediator) =>
{
    request.Id = id;
    await mediator.Send(request);
    return Results.NoContent();
});

app.MapDelete("/v1/orders/{id:guid}", async (Guid id, IMediator mediator) =>
{
    await mediator.Send(new DeleteOrderRequest() { Id = id });
    return Results.NoContent();
});

app.Run();