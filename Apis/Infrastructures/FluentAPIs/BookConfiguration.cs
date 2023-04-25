using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.FluentAPIs
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasQueryFilter(x => x.Inventory > 0);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();


            builder.Property(b => b.Price).HasColumnType("decimal(18,2)");

         
            builder.HasData(
                new Book
                {
                    Id = new Guid("00000001-0000-0000-0000-000000000000"),
                    Title = "System design interview",
                    Author = "Alex Xu",
                    Image = "Image1",
                    Price = 500,
                    AverageRating = 0,
                    RatingCount = 0,
                    TotalRating = 0,
                    Inventory = 10,
                    PublicationDate = new DateTime(2023, 10, 2),
                    Genre = "Programming"
                },
                new Book
                {
                    Id = new Guid("00000002-0000-0000-0000-000000000000"),
                    Title = "Code Writing ",
                    Author = "David",
                    Image = "Image2",
                    Price = 500,
                    AverageRating = 5,
                    RatingCount = 5,
                    TotalRating = 50,
                    Inventory = 100,
                    PublicationDate = new DateTime(2022, 9, 2),
                    Genre = "Programming"
                },
                new Book
                {
                    Id = new Guid("00000003-0000-0000-0000-000000000000"),
                    Title = "Science Applications",
                    Author = "Mark",
                    Image = "Image3",
                    Price = 600,
                    AverageRating = 0,
                    RatingCount = 0,
                    TotalRating = 0,
                    Inventory = 150,
                    PublicationDate = new DateTime(2022, 8, 2),
                    Genre = "English"
                },
                new Book
                {
                    Id = new Guid("00000004-0000-0000-0000-000000000000"),
                    Title = "Education of Computer Science",
                    Author = "Bob",
                    Image = "Image3",
                    Price = 700,
                    AverageRating = 0,
                    RatingCount = 0,
                    TotalRating = 0,
                    Inventory = 10,
                    PublicationDate = new DateTime(2023, 9, 20),
                    Genre = "Programming"
                }
            );
        }
    }
}
