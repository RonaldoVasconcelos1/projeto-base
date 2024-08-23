namespace Infra.DbContext.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entidades;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("customers");

        // Configura a chave primária
        builder.HasKey(c => c.Id);

        // Configura a propriedade Name como obrigatória
        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100); // Limita o comprimento do nome

        // Configura a propriedade Address
        builder.OwnsOne(c => c.Address, a =>
        {
            a.WithOwner(); // Indica que Address é um ValueObject que não precisa de uma tabela separada
            a.Property(p => p.Street).HasColumnName("Street").HasMaxLength(200);
            a.Property(p => p.City).HasColumnName("City").HasMaxLength(100);
            a.Property(p => p.State).HasColumnName("State").HasMaxLength(50);
            a.Property(p => p.PostalCode).HasColumnName("PostalCode").HasMaxLength(20);
        });
        
    }
}