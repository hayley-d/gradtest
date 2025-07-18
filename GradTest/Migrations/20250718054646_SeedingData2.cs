using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GradTest.Migrations
{
    /// <inheritdoc />
    public partial class SeedingData2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "Description", "Name", "Price", "StockQuantity" },
                values: new object[,]
                {
                    { new Guid("27d328de-7853-4186-aa29-d24726acb6c5"), "Book", "Final book in the Witcher Saga.", "Lady of the Lake", 209m, 400 },
                    { new Guid("450a92f2-2e00-48a4-95ed-7ea9e207fe96"), "Book", "Geralt continues his journey amidst political tension.", "Time of Contempt", 189.99m, 400 },
                    { new Guid("48e55bb4-56b8-4c8e-b074-641899ba89a8"), "Book", "The first novel in the Witcher Saga by Andrzej Sapkowski.", "Blood of Elves", 199.99m, 400 },
                    { new Guid("67fb457c-0929-405d-a163-4f31eda363cf"), "Clothing", "T-shirt with the cover art for the The Two Towers book.", "The Two Towers t-shirt M", 189m, 400 },
                    { new Guid("82dc1c3c-e2f6-4857-8765-db2373f466f1"), "Book", "Bilbo’s adventure to the Lonely Mountain.", "The Hobbit", 219.99m, 400 },
                    { new Guid("87a13776-11a1-4f8a-bc63-bbd8b4d609a9"), "Book", "A mythic history of Middle-earth by J.R.R. Tolkien.", "The Silmarillion", 299.99m, 400 },
                    { new Guid("8c0075b2-03fe-4708-861c-691023e8de9f"), "Book", "The second volume of The Lord of the Rings.", "The Two Towers", 249.99m, 400 },
                    { new Guid("9b4f089a-cd4b-4711-9465-a82c97b3e9fa"), "Book", "The epic conclusion of the War of the Ring.", "The Return of the King", 269m, 400 },
                    { new Guid("ac4a4eb2-3a5c-4a58-9fc1-68501e25ae7b"), "Clothing", "T-shirt with the cover art for the Blood of Elves book.", "Blood of Elves t-shirt M", 189m, 400 },
                    { new Guid("b0a89ade-eafd-43aa-b730-2029c9c6fab2"), "Book", "A standalone Witcher novel.", "Season of Storms", 189m, 400 },
                    { new Guid("fdc734f4-c3e6-4034-a3a6-d09ec04aba5f"), "Clothing", "T-shirt with the cover art for the Season of Storms book.", "Season of Storms t-shirt M", 189m, 400 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("27d328de-7853-4186-aa29-d24726acb6c5"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("450a92f2-2e00-48a4-95ed-7ea9e207fe96"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("48e55bb4-56b8-4c8e-b074-641899ba89a8"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("67fb457c-0929-405d-a163-4f31eda363cf"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("82dc1c3c-e2f6-4857-8765-db2373f466f1"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("87a13776-11a1-4f8a-bc63-bbd8b4d609a9"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("8c0075b2-03fe-4708-861c-691023e8de9f"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("9b4f089a-cd4b-4711-9465-a82c97b3e9fa"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("ac4a4eb2-3a5c-4a58-9fc1-68501e25ae7b"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("b0a89ade-eafd-43aa-b730-2029c9c6fab2"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("fdc734f4-c3e6-4034-a3a6-d09ec04aba5f"));

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
    }
}
