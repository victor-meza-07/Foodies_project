using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Foodies.Migrations
{
    public partial class AddedLikesModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "573af9ec-72a7-44f7-aa6a-75f2d7fdf1bc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7e83cb69-85f2-48ce-a6b7-36e6fe0ff790");

            migrationBuilder.CreateTable(
                name: "Likes",
                columns: table => new
                {
                    LikeHistoryKey = table.Column<string>(nullable: false),
                    RestaurantModelPrimaryKey = table.Column<string>(nullable: true),
                    CustomerModelPrimaryKey = table.Column<string>(nullable: true),
                    TimeStamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Likes", x => x.LikeHistoryKey);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "792cc03e-0f7e-40b4-84a4-d89876d5f481", "8ea72816-a99b-4630-afd7-a879eca7ccfa", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8b33c3bc-abb2-4f8c-ab54-201931979078", "52849c5f-e8f6-4fb6-85d5-741c9934e30a", "Employee", "EMPLOYEE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Likes");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "792cc03e-0f7e-40b4-84a4-d89876d5f481");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8b33c3bc-abb2-4f8c-ab54-201931979078");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7e83cb69-85f2-48ce-a6b7-36e6fe0ff790", "a8cc1942-5290-4d22-9b76-327ae9e6336b", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "573af9ec-72a7-44f7-aa6a-75f2d7fdf1bc", "56d04ff5-cf59-4e95-bc4a-3ba59bf47b81", "Employee", "EMPLOYEE" });
        }
    }
}
