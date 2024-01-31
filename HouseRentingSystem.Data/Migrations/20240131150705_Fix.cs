using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRentingSystem.Data.Migrations
{
    public partial class Fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: new Guid("01a7b4c5-db28-43fd-a63c-ed3a8070f5fe"));

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: new Guid("34d08a33-4a56-4cb1-9713-820fdce86ddd"));

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: new Guid("a0ab2b2d-d6f4-4ac4-894e-c040e2041aea"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Houses",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 14, 18, 30, 20, 483, DateTimeKind.Utc).AddTicks(8237));

            migrationBuilder.InsertData(
                table: "Houses",
                columns: new[] { "Id", "Address", "AgentId", "CategoryId", "Description", "ImageUrl", "PricePerMounth", "RenterId", "Title" },
                values: new object[] { new Guid("46028e75-49be-4df5-8043-a66abb42d7a9"), "North London, UK (near the border)", new Guid("6dd681b2-3bda-4b30-9755-0e12c7f27ce4"), 3, "A big house for your whole family. Don't miss to buy a house with three bedrooms.", "https://www.luxury-architecture.net/wp-content/uploads/2017/12/1513217889-7597-FAIRWAYS-010.jpg", 2100.00m, new Guid("c7b6e1a6-954b-47e6-8cf7-a488a810ce96"), "Big House Marina" });

            migrationBuilder.InsertData(
                table: "Houses",
                columns: new[] { "Id", "Address", "AgentId", "CategoryId", "Description", "ImageUrl", "PricePerMounth", "RenterId", "Title" },
                values: new object[] { new Guid("71458b18-3657-4e04-8c89-82d19ce3bf11"), "Near the Sea Garden in Burgas, Bulgaria", new Guid("6dd681b2-3bda-4b30-9755-0e12c7f27ce4"), 2, "It has the best comfort you will ever ask for. With two bedrooms, it is great for your family.", "https://cf.bstatic.com/xdata/images/hotel/max1024x768/179489660.jpg?k=2029f6d9589b49c95dcc9503a265e292c2cdfcb5277487a0050397c3f8dd545a&o=&hp=1", 1200.00m, null, "Family House Comfort" });

            migrationBuilder.InsertData(
                table: "Houses",
                columns: new[] { "Id", "Address", "AgentId", "CategoryId", "Description", "ImageUrl", "PricePerMounth", "RenterId", "Title" },
                values: new object[] { new Guid("877aefcf-c34b-4878-9524-51125106649a"), "Boyana Neighbourhood, Sofia, Bulgaria", new Guid("6dd681b2-3bda-4b30-9755-0e12c7f27ce4"), 2, "This luxurious house is everything you will need. It is just excellent.", "https://i.pinimg.com/originals/a6/f5/85/a6f5850a77633c56e4e4ac4f867e3c00.jpg", 2000.00m, null, "Grand House" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: new Guid("46028e75-49be-4df5-8043-a66abb42d7a9"));

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: new Guid("71458b18-3657-4e04-8c89-82d19ce3bf11"));

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: new Guid("877aefcf-c34b-4878-9524-51125106649a"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Houses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 14, 18, 30, 20, 483, DateTimeKind.Utc).AddTicks(8237),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.InsertData(
                table: "Houses",
                columns: new[] { "Id", "Address", "AgentId", "CategoryId", "CreatedOn", "Description", "ImageUrl", "PricePerMounth", "RenterId", "Title" },
                values: new object[] { new Guid("01a7b4c5-db28-43fd-a63c-ed3a8070f5fe"), "Boyana Neighbourhood, Sofia, Bulgaria", new Guid("6dd681b2-3bda-4b30-9755-0e12c7f27ce4"), 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This luxurious house is everything you will need. It is just excellent.", "https://i.pinimg.com/originals/a6/f5/85/a6f5850a77633c56e4e4ac4f867e3c00.jpg", 2000.00m, null, "Grand House" });

            migrationBuilder.InsertData(
                table: "Houses",
                columns: new[] { "Id", "Address", "AgentId", "CategoryId", "CreatedOn", "Description", "ImageUrl", "PricePerMounth", "RenterId", "Title" },
                values: new object[] { new Guid("34d08a33-4a56-4cb1-9713-820fdce86ddd"), "Near the Sea Garden in Burgas, Bulgaria", new Guid("6dd681b2-3bda-4b30-9755-0e12c7f27ce4"), 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "It has the best comfort you will ever ask for. With two bedrooms, it is great for your family.", "https://cf.bstatic.com/xdata/images/hotel/max1024x768/179489660.jpg?k=2029f6d9589b49c95dcc9503a265e292c2cdfcb5277487a0050397c3f8dd545a&o=&hp=1", 1200.00m, null, "Family House Comfort" });

            migrationBuilder.InsertData(
                table: "Houses",
                columns: new[] { "Id", "Address", "AgentId", "CategoryId", "CreatedOn", "Description", "ImageUrl", "PricePerMounth", "RenterId", "Title" },
                values: new object[] { new Guid("a0ab2b2d-d6f4-4ac4-894e-c040e2041aea"), "North London, UK (near the border)", new Guid("6dd681b2-3bda-4b30-9755-0e12c7f27ce4"), 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A big house for your whole family. Don't miss to buy a house with three bedrooms.", "https://www.luxury-architecture.net/wp-content/uploads/2017/12/1513217889-7597-FAIRWAYS-010.jpg", 2100.00m, new Guid("c7b6e1a6-954b-47e6-8cf7-a488a810ce96"), "Big House Marina" });
        }
    }
}
