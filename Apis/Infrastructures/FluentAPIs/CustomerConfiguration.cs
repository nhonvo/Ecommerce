using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.FluentAPIs
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.Orders)
                   .WithOne(x => x.Customer)
                   .HasForeignKey(x => x.CustomerId);
            builder.HasData(
                new Customer
                {
                    Id = new Guid("00000001-0000-0000-0000-000000000000"),
                    Name = "Võ Thương Trường Nhơn",
                    Address = "69 Bùi Thị Xuân",
                    Email = "vothuongtruongnhon2002@gmail.com",
                    Phone = "0901238495",
                },
                new Customer
                {
                    Id = new Guid("00000002-0000-0000-0000-000000000000"),
                    Name = "Bùi Quang Thọ",
                    Address = "67 Bùi Thị Xuân",
                    Email = "fptvttnhon2017@gmail.com",
                    Phone = "0901238495",
                }
            );
        }
    }

}
