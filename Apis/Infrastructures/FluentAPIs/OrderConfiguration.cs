using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.FluentAPIs
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.OrderDetails)
                   .WithOne(x => x.Order)
                   .HasForeignKey(x => x.OrderId);
            builder.HasOne(x => x.Customer)
                   .WithMany(x => x.Orders)
                   .HasForeignKey(x => x.CustomerId);
            builder.HasData(
                new Order
                {
                    Id = new Guid("00000001-0000-0000-0000-000000000000"),
                    CustomerId = new Guid("00000002-0000-0000-0000-000000000000"),
                    OrderDate = new DateTime(2023, 4, 18, 1, 1, 1),
                    TotalAmount = 1000000m
                },
                new Order
                {
                    Id = new Guid("00000002-0000-0000-0000-000000000000"),
                    CustomerId = new Guid("00000001-0000-0000-0000-000000000000"),
                    OrderDate = new DateTime(2023, 4, 18, 1, 1, 1),
                    TotalAmount = 1000000m
                }
            );

        }
    }

}
