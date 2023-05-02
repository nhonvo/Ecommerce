using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class CreateDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Books_BookId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_UserId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "WishLists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderDetails",
                table: "OrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails");

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000000"));

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

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("00000001-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("00000001-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0000-0000-0000-000000000002"));

            migrationBuilder.DropColumn(
                name: "ShippingAddress",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ReviewSubmitted",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "Author",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "AverageRating",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Genre",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Inventory",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "PublicationDate",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "RatingCount",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "TotalRating",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Orders",
                newName: "CustomerId");

            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "Orders",
                newName: "TotalAmount");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                newName: "IX_Orders_CustomerId");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "OrderDetails",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetails_BookId",
                table: "OrderDetails",
                newName: "IX_OrderDetails_ProductId");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Books",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Books",
                newName: "Description");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderDetails",
                table: "OrderDetails",
                columns: new[] { "OrderId", "ProductId" });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModificationBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Books_ProductId",
                table: "OrderDetails",
                column: "ProductId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Books_ProductId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderDetails",
                table: "OrderDetails");

            migrationBuilder.RenameColumn(
                name: "TotalAmount",
                table: "Orders",
                newName: "TotalPrice");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Orders",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                newName: "IX_Orders_UserId");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "OrderDetails",
                newName: "BookId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetails_ProductId",
                table: "OrderDetails",
                newName: "IX_OrderDetails_BookId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Books",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Books",
                newName: "Image");

            migrationBuilder.AddColumn<string>(
                name: "ShippingAddress",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "ReviewSubmitted",
                table: "OrderDetails",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<float>(
                name: "AverageRating",
                table: "Books",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "Genre",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Inventory",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "PublicationDate",
                table: "Books",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "RatingCount",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalRating",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderDetails",
                table: "OrderDetails",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModificationBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => new { x.UserId, x.BookId });
                    table.ForeignKey(
                        name: "FK_Carts_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Carts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModificationBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WishLists",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModificationBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishLists", x => new { x.UserId, x.BookId });
                    table.ForeignKey(
                        name: "FK_WishLists_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WishLists_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "AverageRating", "CreatedBy", "CreationDate", "DeleteBy", "DeletionDate", "Genre", "Image", "Inventory", "IsDeleted", "ModificationBy", "ModificationDate", "Price", "PublicationDate", "RatingCount", "Title", "TotalRating" },
                values: new object[,]
                {
                    { new Guid("00000001-0000-0000-0000-000000000000"), "Alex Xu", 0f, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Programming", "Image1", 10, false, null, null, 500m, new DateTime(2023, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "System design interview", 0 },
                    { new Guid("00000002-0000-0000-0000-000000000000"), "David", 5f, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Programming", "Image2", 100, false, null, null, 500m, new DateTime(2022, 9, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Code Writing ", 50 },
                    { new Guid("00000003-0000-0000-0000-000000000000"), "Mark", 0f, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "English", "Image3", 150, false, null, null, 600m, new DateTime(2022, 8, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Science Applications", 0 },
                    { new Guid("00000004-0000-0000-0000-000000000000"), "Bob", 0f, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Programming", "Image3", 10, false, null, null, 700m, new DateTime(2023, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Education of Computer Science", 0 }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CreatedBy", "CreationDate", "DeleteBy", "DeletionDate", "IsDeleted", "ModificationBy", "ModificationDate", "OrderDate", "ShippingAddress", "Status", "TotalPrice", "UserId" },
                values: new object[,]
                {
                    { new Guid("00000001-0000-0000-0000-000000000001"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, null, null, new DateTime(2022, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "1234 Main St, New York, NY", "Delivered", 1500m, new Guid("00000001-1000-0000-0000-000000000000") },
                    { new Guid("00000002-0000-0000-0000-000000000002"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, null, null, new DateTime(2022, 4, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "5678 Main St, New York, NY", "Shipped", 500m, new Guid("00000001-2000-0000-0000-000000000000") },
                    { new Guid("00000003-0000-0000-0000-000000000003"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, null, null, new DateTime(2022, 4, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "9012 Main St, New York, NY", "Processing", 300m, new Guid("00000001-2000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "OrderDetails",
                columns: new[] { "Id", "BookId", "CreatedBy", "CreationDate", "DeleteBy", "DeletionDate", "IsDeleted", "ModificationBy", "ModificationDate", "OrderId", "Quantity", "ReviewSubmitted" },
                values: new object[,]
                {
                    { new Guid("00000001-0000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, null, null, new Guid("00000001-0000-0000-0000-000000000001"), 2, true },
                    { new Guid("00000002-0000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, null, null, new Guid("00000002-0000-0000-0000-000000000002"), 1, false },
                    { new Guid("00000003-0000-0000-0000-000000000003"), new Guid("00000003-0000-0000-0000-000000000000"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, null, null, new Guid("00000002-0000-0000-0000-000000000002"), 1, false }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "BookId", "Comments", "CreatedBy", "CreationDate", "DeleteBy", "DeletionDate", "IsDeleted", "ModificationBy", "ModificationDate", "Rating", "UserId" },
                values: new object[,]
                {
                    { new Guid("00000001-1000-0000-0000-000000000000"), new Guid("00000001-0000-0000-0000-000000000000"), "Great book!", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, null, null, 4, new Guid("00000001-1000-0000-0000-000000000000") },
                    { new Guid("00000002-1000-0000-0000-000000000000"), new Guid("00000002-0000-0000-0000-000000000000"), "It's ok", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, null, null, 3, new Guid("00000001-1000-0000-0000-000000000000") },
                    { new Guid("00000003-1000-0000-0000-000000000000"), new Guid("00000003-0000-0000-0000-000000000000"), "Love it!", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, null, null, 5, new Guid("00000001-2000-0000-0000-000000000000") },
                    { new Guid("00000004-1000-0000-0000-000000000000"), new Guid("00000004-0000-0000-0000-000000000000"), "Very informative", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, null, null, 4, new Guid("00000001-2000-0000-0000-000000000000") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_BookId",
                table: "Carts",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_BookId",
                table: "Reviews",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WishLists_BookId",
                table: "WishLists",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Books_BookId",
                table: "OrderDetails",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
