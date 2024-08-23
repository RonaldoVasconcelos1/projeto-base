using Domain.ValueObjects;

namespace Domain.Entidades;

public class Customer: BaseEntity
{
    public string Name { get; private set; }
    public Address Address { get; private set; }

    private Customer()
    {
    }

    // Métodos de domínio específicos para Customer
    public void UpdateAddress(Address newAddress)
    {
        Address = newAddress ?? throw new ArgumentException("Address cannot be null.");
    }


    public static class Factories
    {
        public static Customer CreateCostumer(Guid id, string name, Address address)
        {
            return new Customer()
            {
                Id = id,
                Name = name,
                Address = address
            };
        }
    }
}