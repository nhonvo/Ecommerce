using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.FluentAPIs
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(b => b.CreditBalance).HasColumnType("decimal(18,2)");

            builder.Property(x => x.Email).HasMaxLength(100);


            builder.HasData(
                new User
                {
                    Id = new Guid("00000001-1000-0000-0000-000000000000"),
                    Name = "Võ Thương Trường Nhơn",
                    Email = "vothuongtruongnhon2002@gmail.com",
                    Password = "Password",
                    Address = "69 Bùi Thị Xuân",
                    CreditBalance = 10000,
                    Role = Role.Admin
                },
                new User
                {
                    Id = new Guid("00000001-2000-0000-0000-000000000000"),
                    Name = "Võ Nhơn 2:40 còn code",
                    Email = "fptvttnhon2018@gmail.com",
                    Password = "Password",
                    Address = "69 Bùi Thị Xuân",
                    CreditBalance = 500,
                    Role = Role.User
                }
            );
        }
    }
}
