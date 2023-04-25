using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class UpdateKeyOrderDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderDetails",
                table: "OrderDetails");

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumns: new[] { "BookId", "OrderId" },
                keyValues: new object[] { new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000001") });

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumns: new[] { "BookId", "OrderId" },
                keyValues: new object[] { new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000002") });

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumns: new[] { "BookId", "OrderId" },
                keyValues: new object[] { new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000002") });

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderDetails",
                table: "OrderDetails",
                column: "Id");

            migrationBuilder.InsertData(
                table: "OrderDetails",
                columns: new[] { "Id", "BookId", "CreatedBy", "CreationDate", "DeleteBy", "DeletionDate", "IsDeleted", "ModificationBy", "ModificationDate", "OrderId", "Quantity", "ReviewSubmitted" },
                values: new object[,]
                {
                    { new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, null, null, new Guid("00000001-0000-0000-0000-000000000001"), 2, true },
                    { new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, null, null, new Guid("00000002-0000-0000-0000-000000000002"), 1, false },
                    { new Guid("00000003-0000-0000-0000-000000000003"), new Guid("00000003-0000-0000-0000-000000000000"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, null, null, new Guid("00000002-0000-0000-0000-000000000002"), 1, false }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderDetails",
                table: "OrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails");

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumn: "Id",
                keyValue: new Guid("00000001-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-0000-0000-000000000003"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderDetails",
                table: "OrderDetails",
                columns: new[] { "OrderId", "BookId" });

            migrationBuilder.InsertData(
                table: "OrderDetails",
                columns: new[] { "BookId", "OrderId", "CreatedBy", "CreationDate", "DeleteBy", "DeletionDate", "Id", "IsDeleted", "ModificationBy", "ModificationDate", "Quantity", "ReviewSubmitted" },
                values: new object[,]
                {
                    { new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000001"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, new Guid("00000001-0000-0000-0000-000000000000"), false, null, null, 2, true },
                    { new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000002"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, new Guid("00000002-0000-0000-0000-000000000000"), false, null, null, 1, false },
                    { new Guid("00000003-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000002"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, new Guid("00000003-0000-0000-0000-000000000003"), false, null, null, 1, false }
                });
        }
    }
}
