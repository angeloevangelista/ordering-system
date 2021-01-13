using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderingSystem.Domain.Entities;

namespace OrderingSystem.Data.Configurations
{
  public class ClientConfiguration : IEntityTypeConfiguration<Client>
  {
    public void Configure(EntityTypeBuilder<Client> builder)
    {
      builder.ToTable("clients");
      builder.HasKey(pre => pre.Id).HasName("pk_clients");

      builder.Property(pre => pre.Id)
        .HasColumnName("id");

      builder.Property(pre => pre.Name)
        .HasColumnName("name")
        .HasColumnType("VARCHAR(255)")
        .IsRequired();

      builder.Property(pre => pre.Email)
        .HasColumnName("email")
        .HasColumnType("VARCHAR(255)")
        .IsRequired();

      builder.Property(pre => pre.Password)
        .HasColumnName("password")
        .HasColumnType("VARCHAR(255)")
        .IsRequired();

      builder.Property(pre => pre.Telephone)
        .HasColumnName("telephone")
        .HasColumnType("VARCHAR(50)")
        .IsRequired();

      builder.Property(pre => pre.Active)
        .HasColumnName("active");

      builder.Property(pre => pre.CreatedAt)
        .HasColumnName("created_at")
        .IsRequired();

      builder.Property(pre => pre.UpdatedAt)
        .HasColumnName("updated_at")
        .IsRequired();

      builder
        .HasMany<Order>(pre => pre.Orders)
        .WithOne(pre => pre.Client)
        .HasForeignKey(pre => pre.ClientId)
        .HasConstraintName("fk_client_order")
        .OnDelete(DeleteBehavior.SetNull);

      builder
        .HasMany<Product>(pre => pre.Products)
        .WithOne(pre => pre.Client)
        .HasForeignKey(pre => pre.ClientId)
        .HasConstraintName("fk_client_product")
        .OnDelete(DeleteBehavior.SetNull);
    }
  }
}