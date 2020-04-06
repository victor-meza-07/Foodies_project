using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Foodies.Migrations
{
    public partial class AddedDislikesModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1f6b2daf-83e2-4e44-ad4d-bbcd2cdcb233");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "abbe18de-e7c8-439d-9157-e131ed1bed73");

            migrationBuilder.CreateTable(
                name: "Dislikes",
                columns: table => new
                {
                    DislikeHistoryModelPrimaryKey = table.Column<string>(nullable: false),
                    RestaurantModelPrimaryKey = table.Column<string>(nullable: true),
                    CustomerModelPrimaryKey = table.Column<string>(nullable: true),
                    TimeStamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dislikes", x => x.DislikeHistoryModelPrimaryKey);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7e83cb69-85f2-48ce-a6b7-36e6fe0ff790", "a8cc1942-5290-4d22-9b76-327ae9e6336b", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "573af9ec-72a7-44f7-aa6a-75f2d7fdf1bc", "56d04ff5-cf59-4e95-bc4a-3ba59bf47b81", "Employee", "EMPLOYEE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dislikes");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "573af9ec-72a7-44f7-aa6a-75f2d7fdf1bc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7e83cb69-85f2-48ce-a6b7-36e6fe0ff790");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "abbe18de-e7c8-439d-9157-e131ed1bed73", "84864fbc-a112-4309-977a-b3dbc09098c6", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1f6b2daf-83e2-4e44-ad4d-bbcd2cdcb233", "2b9f6eec-a161-4625-bfc8-52dd0b3bfce4", "Employee", "EMPLOYEE" });
        }
    }
}
