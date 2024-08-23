using Domain.Repositories;
using Infra.DbContext;

namespace Infra.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private readonly IProductRepository _products;
    private readonly IOrdemRepository _orders;
    private readonly IOrderItemRepository _orderItems;
    private readonly ICustomerRepository _customers;

    public UnitOfWork(IProductRepository products, IOrdemRepository orders, IOrderItemRepository orderItems,
        ICustomerRepository customers, ApplicationDbContext context)
    {
        _products = products;
        _orders = orders;
        _orderItems = orderItems;
        _customers = customers;
        _context = context;
    }
    public IProductRepository Products => _products;
    public IOrdemRepository Orders => _orders;
    public IOrderItemRepository OrderItems => _orderItems;
    public ICustomerRepository Customers => _customers;

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }
}