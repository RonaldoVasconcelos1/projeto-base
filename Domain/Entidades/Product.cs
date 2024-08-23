using Domain.ValueObjects;

namespace Domain.Entidades;

public class Product : BaseEntity
{
    public string Name { get; private set; }
    public Money Price { get; private set; }

    private Product()
    {
    }

    public static class Factories
    {
        public static Product CreateProduct(Guid Id, string name, Money money)
        {
            return new Product()
            {
                Id = Id,
                Name = name,
                Price = money
            };
        }
    }
}