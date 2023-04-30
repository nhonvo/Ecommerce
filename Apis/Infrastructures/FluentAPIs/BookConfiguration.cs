using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.FluentAPIs
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.OrderDetails)
                   .WithOne(x => x.Product)
                   .HasForeignKey(x => x.ProductId);
            builder.HasData(
                new Product
                {
                    Id = new Guid("00000001-0000-0000-0000-000000000000"),
                    Name = "Headphone",
                    Description = "the headphone is the most popular product",
                    Price = 500000m
                },
                new Product
                {
                    Id = new Guid("00000002-0000-0000-0000-000000000000"),
                    Name = "Telephone",
                    Description = "the telephone is the most popular product",
                    Price = 540000m
                }
            );
        }
    }
}
