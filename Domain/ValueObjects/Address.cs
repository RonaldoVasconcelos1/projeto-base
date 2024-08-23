namespace Domain.ValueObjects;

public class Address : ValueObject
{
    public string Street { get; }
    public string City { get; }
    public string State { get; }
    public string PostalCode { get; }

    public Address(string street, string city, string state, string postalCode)
    {
        if (string.IsNullOrWhiteSpace(street)) throw new ArgumentException("Street is required.");
        if (string.IsNullOrWhiteSpace(city)) throw new ArgumentException("City is required.");
        if (string.IsNullOrWhiteSpace(state)) throw new ArgumentException("State is required.");
        if (string.IsNullOrWhiteSpace(postalCode)) throw new ArgumentException("Postal Code is required.");

        Street = street;
        City = city;
        State = state;
        PostalCode = postalCode;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Street;
        yield return City;
        yield return State;
        yield return PostalCode;
    }
}
