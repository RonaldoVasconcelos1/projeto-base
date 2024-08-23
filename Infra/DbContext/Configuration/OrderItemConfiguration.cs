namespace Infra.DbContext.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entidades;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("order_items");

        // Configura a chave primária
        builder.HasKey(oi => oi.Id);

        // Configura a propriedade Quantity como obrigatória
        builder.Property(oi => oi.Quantity)
            .IsRequired();

        // Configura o relacionamento entre OrderItem e Product
        builder.HasOne<Product>() // A configuração aqui assume que Product não possui uma coleção de OrderItems
            .WithMany() // Um Product pode estar em muitos OrderItems
            .HasForeignKey("ProductId") // Define a chave estrangeira na tabela OrderItem
            .OnDelete(DeleteBehavior.Restrict); // Evita a exclusão em cascata do Product

        // Configura a coluna ProductId como uma chave estrangeira
        builder.Property<Guid>("ProductId");

        // Configura a propriedade TotalPrice para não ser mapeada (é uma propriedade calculada)
        builder.Ignore(oi => oi.TotalPrice);
    }
}