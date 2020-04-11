using Microsoft.EntityFrameworkCore.Migrations;

namespace Foodies.Migrations
{
    public partial class solvingResgisterModelBug : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b552030a-40c8-4d47-b92f-e19e86d4e937");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bcd5360f-8693-4d1a-a19a-3b42683df46c");

            migrationBuilder.AddColumn<int>(
                name: "AgeRange",
                table: "Customers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Customers",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5d26d905-d9fd-4581-b583-d9c1b5fb642e", "b4fd6bdf-314d-45f5-8733-61823ebfa87c", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "84015730-4eee-42d3-a601-4da67ea4e3e4", "53cab464-8d0d-4a32-9044-521c28dfd70a", "Employee", "EMPLOYEE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5d26d905-d9fd-4581-b583-d9c1b5fb642e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "84015730-4eee-42d3-a601-4da67ea4e3e4");

            migrationBuilder.DropColumn(
                name: "AgeRange",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Customers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b552030a-40c8-4d47-b92f-e19e86d4e937", "7ac3f066-5df5-4814-9b5d-f1eae30d27ee", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bcd5360f-8693-4d1a-a19a-3b42683df46c", "5a1d4b30-0c07-4435-800d-b6d9652c9504", "Employee", "EMPLOYEE" });
        }
    }
}
