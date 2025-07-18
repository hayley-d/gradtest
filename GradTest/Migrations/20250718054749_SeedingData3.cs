using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GradTest.Migrations
{
    /// <inheritdoc />
    public partial class SeedingData3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { new Guid("43d069af-3a6b-4a71-9086-86dccf419c66"), "Book", "A standalone Witcher novel.", "Season of Storms", 189m, 400 },
                    { new Guid("57ef1877-7411-4b7b-b66f-2b347b084405"), "Book", "Final book in the Witcher Saga.", "Lady of the Lake", 209m, 400 },
                    { new Guid("60cc318b-5231-4138-b098-3618955a9ce3"), "Book", "A mythic history of Middle-earth by J.R.R. Tolkien.", "The Silmarillion", 299.99m, 400 },
                    { new Guid("6ebb72a6-bcb9-48d7-a5f1-59b79f29aac3"), "Clothing", "T-shirt with the cover art for the Blood of Elves book.", "Blood of Elves t-shirt M", 189m, 400 },
                    { new Guid("9a880538-161e-4bd9-94b2-5f586e9169c9"), "Clothing", "T-shirt with the cover art for the The Two Towers book.", "The Two Towers t-shirt M", 189m, 400 },
                    { new Guid("c7182d79-e15a-44cd-bef8-0358a885d09f"), "Book", "The second volume of The Lord of the Rings.", "The Two Towers", 249.99m, 400 },
                    { new Guid("dd920e55-c7f4-4695-a7a8-e6b93b90a671"), "Book", "Bilbo’s adventure to the Lonely Mountain.", "The Hobbit", 219.99m, 400 },
                    { new Guid("ec0166b7-12b9-4814-96a5-a1473c6aa6a9"), "Book", "Geralt continues his journey amidst political tension.", "Time of Contempt", 189.99m, 400 },
                    { new Guid("f062a794-6199-4b5d-a363-5f6bb818c3ec"), "Book", "The epic conclusion of the War of the Ring.", "The Return of the King", 269m, 400 },
                    { new Guid("f5865d9f-0408-4ca9-8e14-e74fe3e39a2b"), "Clothing", "T-shirt with the cover art for the Season of Storms book.", "Season of Storms t-shirt M", 189m, 400 },
                    { new Guid("fe22e924-98bc-4458-94de-390633a9af5e"), "Book", "The first novel in the Witcher Saga by Andrzej Sapkowski.", "Blood of Elves", 199.99m, 400 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("43d069af-3a6b-4a71-9086-86dccf419c66"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("57ef1877-7411-4b7b-b66f-2b347b084405"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("60cc318b-5231-4138-b098-3618955a9ce3"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("6ebb72a6-bcb9-48d7-a5f1-59b79f29aac3"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("9a880538-161e-4bd9-94b2-5f586e9169c9"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("c7182d79-e15a-44cd-bef8-0358a885d09f"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("dd920e55-c7f4-4695-a7a8-e6b93b90a671"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("ec0166b7-12b9-4814-96a5-a1473c6aa6a9"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("f062a794-6199-4b5d-a363-5f6bb818c3ec"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("f5865d9f-0408-4ca9-8e14-e74fe3e39a2b"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("fe22e924-98bc-4458-94de-390633a9af5e"));

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
    }
}
