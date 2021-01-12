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

      builder.Property(pre => pre.Name)
        .HasColumnName("name")
        .IsRequired();

      builder.Property(pre => pre.Email)
        .HasColumnName("email")
        .IsRequired();

      builder.Property(pre => pre.Password)
        .HasColumnName("password")
        .IsRequired();

      builder.Property(pre => pre.Telephone)
        .HasColumnName("telephone")
        .IsRequired();

      builder.Property(pre => pre.CreatedAt)
        .IsRequired();

      builder.Property(pre => pre.UpdatedAt)
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