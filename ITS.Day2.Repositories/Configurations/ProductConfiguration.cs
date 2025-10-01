using ITS.Day2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITS.Day2.Repositories.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> typeBuilder)
        {
            typeBuilder
                .ToTable(nameof(Product));

            typeBuilder
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            typeBuilder
                .Property(p => p.Name)
                .HasMaxLength(200)
                .IsRequired();

            typeBuilder
                .Property(p => p.Price)
                .HasPrecision(18, 2)
                .IsRequired(false);

            typeBuilder
                .Property(p => p.Stock)
                .IsRequired(true);

            typeBuilder
                .HasKey(p => p.Id);

            typeBuilder
                .HasIndex(p => p.Name)
                .IsUnique();
        }
    }
}
