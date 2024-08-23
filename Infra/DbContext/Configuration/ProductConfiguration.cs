namespace Infra.DbContext.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entidades;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products");

        // Configura a chave primária
        builder.HasKey(p => p.Id);

        // Configura a propriedade Name como obrigatória
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100); // Limita o comprimento do nome

        // Configura a propriedade Price
        builder.OwnsOne(p => p.Price, price =>
        {
            price.WithOwner(); // Indica que Price é um ValueObject que não precisa de uma tabela separada
            price.Property(p => p.Amount).HasColumnName("PriceAmount").HasColumnType("decimal(18,2)"); // Define a coluna e o tipo
        });
    }
}
