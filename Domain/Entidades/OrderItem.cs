using Domain.ValueObjects;

namespace Domain.Entidades;

public class OrderItem : BaseEntity
{
    public Product Product { get; private set; }
    public int Quantity { get; private set; }
    public Money TotalPrice => Product.Price.Add(new Money(Product.Price.Amount * (Quantity - 1)));

    private OrderItem()
    {
    }

    public OrderItem(Guid id, Product product, int quantity)
    {
        Id = id;
        Product = product;
        Quantity = quantity;
    }
    
    public static class Factories
    {
        public static OrderItem CreateOrderItem(Guid id, Product product, int quantity)
        {
            return new OrderItem()
            {
                Id = id,
                Product = product,
                Quantity = quantity
            };
        }
    }
}