using Microsoft.EntityFrameworkCore.Migrations;

namespace Foodies.Migrations
{
    public partial class Apicalls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "39583a29-be99-44fd-a339-d016b2cb50c9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b6dd5ef4-8bdf-4eee-94a1-4dd7f3f8035d");

            migrationBuilder.CreateTable(
                name: "RegisteredApiCalls",
                columns: table => new
                {
                    PrimaryKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(nullable: true),
                    FoodType = table.Column<string>(nullable: true),
                    Cuisine = table.Column<string>(nullable: true),
                    SearchedCity = table.Column<string>(nullable: true),
                    SearchedState = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisteredApiCalls", x => x.PrimaryKey);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "89e08e78-1601-40ca-8c57-9b620fa4c8fa", "8fbf4907-c243-46a1-b05d-1b495adf41d7", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a9f322f3-717d-4d6e-8e3f-dcaf6afdefe1", "26f45bd0-865f-465e-af42-86c7a640e291", "Employee", "EMPLOYEE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegisteredApiCalls");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "89e08e78-1601-40ca-8c57-9b620fa4c8fa");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a9f322f3-717d-4d6e-8e3f-dcaf6afdefe1");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b6dd5ef4-8bdf-4eee-94a1-4dd7f3f8035d", "1f621b95-6ab9-42d5-a09c-b50a2e9b1ec9", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "39583a29-be99-44fd-a339-d016b2cb50c9", "9395d7c1-efd3-490c-89cd-1d3420eb8e7a", "Employee", "EMPLOYEE" });
        }
    }
}
