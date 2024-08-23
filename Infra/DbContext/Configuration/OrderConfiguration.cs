namespace Infra.DbContext.Configuration;


using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entidades;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("orders");

        // Configura a chave primária
        builder.HasKey(x => x.Id);

        // Configura o ID para não ser gerado automaticamente
        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        // Configura a propriedade Status com conversão para string
        builder.Property(x => x.Status)
            .IsRequired()
            .HasConversion<string>();

        // Configura o relacionamento entre Order e Customer
        builder.HasOne(o => o.Customer)
            .WithMany() // Um Customer pode ter muitos Orders, mas não tem uma coleção de Orders diretamente
            .HasForeignKey("CustomerId") // Define a chave estrangeira na tabela Order
            .OnDelete(DeleteBehavior.Restrict); // Evita a exclusão em cascata do Customer

        // Configura o relacionamento entre Order e OrderItem
        builder.HasMany(o => o.Items)
            .WithOne() // OrderItem não possui uma referência direta para Order
            .HasForeignKey("OrderId") // Define a chave estrangeira na tabela OrderItem
            .OnDelete(DeleteBehavior.Cascade); // Define exclusão em cascata dos itens quando o pedido é excluído
    }
}