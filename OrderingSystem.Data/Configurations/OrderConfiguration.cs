using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderingSystem.Domain.Entities;

namespace OrderingSystem.Data.Configurations
{
  public class OrderConfiguration : IEntityTypeConfiguration<Order>
  {
    public void Configure(EntityTypeBuilder<Order> builder)
    {
      builder.ToTable("orders");
      builder.HasKey(pre => pre.Id).HasName("pk_orders");

      builder.Property(pre => pre.Id)
        .HasColumnName("id");

      builder.Property(pre => pre.Amount)
        .HasColumnName("amount")
        .IsRequired();

      builder.Property(pre => pre.Discount)
        .HasColumnName("discount")
        .HasColumnType("decimal(10, 2)")
        .IsRequired();

      builder.Property(pre => pre.CanceledAt)
        .HasColumnName("canceled_at")
        .IsRequired(false);

      builder.Property(pre => pre.Active)
        .HasColumnName("active");

      builder.Property(pre => pre.CreatedAt)
        .HasColumnName("created_at")
        .IsRequired();

      builder.Property(pre => pre.UpdatedAt)
        .HasColumnName("updated_at")
        .IsRequired();

      builder.Property(pre => pre.ClientId)
        .HasColumnName("client_id");

      builder.Property(pre => pre.ProductId)
        .HasColumnName("product_id");

      builder
        .HasOne<Client>(pre => pre.Client)
        .WithMany(pre => pre.Orders)
        .HasForeignKey(pre => pre.ClientId)
        .HasConstraintName("fk_orders")
        .OnDelete(DeleteBehavior.SetNull);

      builder
        .HasOne<Product>(pre => pre.Product)
        .WithMany()
        .HasForeignKey(pre => pre.ProductId)
        .HasConstraintName("fk_product")
        .OnDelete(DeleteBehavior.SetNull);
    }
  }
}