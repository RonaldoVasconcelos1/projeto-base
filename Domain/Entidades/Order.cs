using Domain.Enums;
using Domain.ValueObjects;

namespace Domain.Entidades;

public class Order : BaseEntity
{
    public Customer Customer { get; private set; }
    public IList<OrderItem> Items { get; private set; } = Enumerable.Empty<OrderItem>().ToList();
    public OrderStatus Status { get; private set; } = OrderStatus.Pending;

    private Order()
    {
    }
    // Método de domínio para adicionar um item ao pedido
    public void AddItem(Product product, int quantity)
    {
        if (quantity <= 0) throw new ArgumentException("Quantity must be greater than zero.");
        var orderItem = new OrderItem(Guid.NewGuid(), product, quantity);
        Items.Add(orderItem);
    }

    public Money GetTotal()
    {
        return Items.Aggregate(new Money(0), (total, item) => total.Add(item.TotalPrice));
    }

    public void MarkAsPaid()
    {
        Status = OrderStatus.Paid;
    }

    public void MarkAsShipped()
    {
        Status = OrderStatus.Shipped;
    }


    public static class Factories
    {
        public static Order CreateOrder(Guid id, Customer customer)
        {
            return new Order()
            {
                Id = id,
                Customer = customer
            };
        } 
    }
}