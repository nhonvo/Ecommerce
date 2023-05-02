using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "CreatedBy", "CreationDate", "DeleteBy", "DeletionDate", "Description", "IsDeleted", "ModificationBy", "ModificationDate", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("00000001-0000-0000-0000-000000000000"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "the headphone is the most popular product", false, null, null, "Headphone", 500000m },
                    { new Guid("00000002-0000-0000-0000-000000000000"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "the telephone is the most popular product", false, null, null, "Telephone", 540000m }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Address", "CreatedBy", "CreationDate", "DeleteBy", "DeletionDate", "Email", "IsDeleted", "ModificationBy", "ModificationDate", "Name", "Phone" },
                values: new object[,]
                {
                    { new Guid("00000001-0000-0000-0000-000000000000"), "69 Bùi Thị Xuân", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "vothuongtruongnhon2002@gmail.com", false, null, null, "Võ Thương Trường Nhơn", "0901238495" },
                    { new Guid("00000002-0000-0000-0000-000000000000"), "67 Bùi Thị Xuân", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "fptvttnhon2017@gmail.com", false, null, null, "Bùi Quang Thọ", "0901238495" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CreatedBy", "CreationDate", "CustomerId", "DeleteBy", "DeletionDate", "IsDeleted", "ModificationBy", "ModificationDate", "OrderDate", "TotalAmount" },
                values: new object[,]
                {
                    { new Guid("00000001-0000-0000-0000-000000000000"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000002-0000-0000-0000-000000000000"), null, null, false, null, null, new DateTime(2023, 4, 18, 1, 1, 1, 0, DateTimeKind.Unspecified), 1000000m },
                    { new Guid("00000002-0000-0000-0000-000000000000"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000001-0000-0000-0000-000000000000"), null, null, false, null, null, new DateTime(2023, 4, 18, 1, 1, 1, 0, DateTimeKind.Unspecified), 1000000m }
                });

            migrationBuilder.InsertData(
                table: "OrderDetails",
                columns: new[] { "OrderId", "ProductId", "CreatedBy", "CreationDate", "DeleteBy", "DeletionDate", "Id", "IsDeleted", "ModificationBy", "ModificationDate", "Quantity" },
                values: new object[] { new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, new Guid("00000000-0000-0000-0000-000000000000"), false, null, null, 2 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000") });

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("00000001-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("00000001-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("00000001-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0000-0000-0000-000000000000"));
        }
    }
}
