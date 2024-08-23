using Domain.Entidades;

namespace Domain.Repositories;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(Guid id);
    Task<List<Product?>> GetAllAsync();
}