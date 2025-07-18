using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GradTest.Migrations
{
    /// <inheritdoc />
    public partial class SeedingData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "Description", "Name", "Price", "StockQuantity" },
                values: new object[,]
                {
                    { new Guid("2dbad133-6d76-4abc-b470-796e0bc706c9"), "Book", "Lotr book 2", "Lord of the Rings 2", 99m, 7 },
                    { new Guid("4512c1d2-67d2-4dfe-a7c6-779e95847b12"), "Book", "The witcher book 3", "The witcher 3", 99m, 7 },
                    { new Guid("512b837c-7cd6-499e-b54f-648862908727"), "Book", "The witcher book 2", "The witcher 2", 99m, 7 },
                    { new Guid("52aca56e-603b-458d-ad8f-69a44142ba2a"), "Book", "Lotr book 3", "Lord of the Rings 3", 99m, 7 },
                    { new Guid("54da348a-cd49-406c-a6f3-97f577c996af"), "Book", "The witcher book", "The witcher 1", 99m, 7 },
                    { new Guid("86ceeb4f-32de-4667-9c01-15fbeb35dd77"), "Book", "Lotr book 1", "Lord of the Rings 1", 99m, 7 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("2dbad133-6d76-4abc-b470-796e0bc706c9"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("4512c1d2-67d2-4dfe-a7c6-779e95847b12"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("512b837c-7cd6-499e-b54f-648862908727"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("52aca56e-603b-458d-ad8f-69a44142ba2a"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("54da348a-cd49-406c-a6f3-97f577c996af"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("86ceeb4f-32de-4667-9c01-15fbeb35dd77"));
        }
    }
}
