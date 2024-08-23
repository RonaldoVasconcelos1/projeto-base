namespace Domain.Repositories;

public interface IUnitOfWork
{
    IProductRepository Products { get; }
    IOrdemRepository Orders { get; }
    IOrderItemRepository OrderItems { get; }
    ICustomerRepository Customers { get; }

    Task CommitAsync();
}