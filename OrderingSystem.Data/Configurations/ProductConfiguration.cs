using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderingSystem.Domain.Entities;

namespace OrderingSystem.Data.Configurations
{
  public class ProductConfiguration : IEntityTypeConfiguration<Product>
  {
    public void Configure(EntityTypeBuilder<Product> builder)
    {
      builder.ToTable("products");
      builder.HasKey(pre => pre.Id).HasName("pk_products");

      builder.Property(pre => pre.Id)
        .HasColumnName("id");

      builder.Property(pre => pre.Name)
        .HasColumnName("name")
        .IsRequired();

      builder.Property(pre => pre.Price)
        .HasColumnName("price")
        .HasColumnType("decimal(10, 2)")
        .IsRequired();

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

      builder
        .HasOne<Client>(pre => pre.Client)
        .WithMany(pre => pre.Products)
        .HasForeignKey(pre => pre.ClientId)
        .HasConstraintName("fk_client")
        .OnDelete(DeleteBehavior.Cascade);
    }
  }
}