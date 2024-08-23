using Domain.Entidades;
using Domain.Repositories;
using Infra.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _applicationDbContext;

    public ProductRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        return await _applicationDbContext.Products.FindAsync(id);
    }

    public async Task<List<Product?>> GetAllAsync()
    {
        return await _applicationDbContext
            .Products
            .AsQueryable()
            .ToListAsync();
    }
}