using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.FluentAPIs
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.HasKey(x => new { x.OrderId, x.ProductId });
            builder.HasOne(x => x.Product)
                   .WithMany(x => x.OrderDetails)
                   .HasForeignKey(x => x.ProductId);
            builder.HasOne(x => x.Order)
                   .WithMany(x => x.OrderDetails)
                   .HasForeignKey(x => x.OrderId);
            builder.HasData(
                new OrderDetail
                {
                    ProductId = new Guid("00000001-0000-0000-0000-000000000000"),
                    OrderId = new Guid("00000001-0000-0000-0000-000000000000"),
                    Quantity = 2
                }
            );
        }
    }

}
