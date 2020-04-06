using Microsoft.EntityFrameworkCore.Migrations;

namespace Foodies.Migrations
{
    public partial class AddedAddressModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "792cc03e-0f7e-40b4-84a4-d89876d5f481");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8b33c3bc-abb2-4f8c-ab54-201931979078");

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressKey = table.Column<string>(nullable: false),
                    BuildingNumber = table.Column<int>(nullable: false),
                    StreetName = table.Column<string>(nullable: true),
                    ZipCode = table.Column<int>(nullable: false),
                    City = table.Column<string>(nullable: true),
                    StateCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressKey);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "53ec6d0a-aed2-421f-83d2-c63c5a6e6771", "1dbe012a-28fc-4f57-85cf-787ee5cd53f9", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "40d0575c-05e1-4560-829f-faec4e6dda1a", "ff90a708-1de6-4756-b474-05dab6f44e60", "Employee", "EMPLOYEE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "40d0575c-05e1-4560-829f-faec4e6dda1a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "53ec6d0a-aed2-421f-83d2-c63c5a6e6771");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "792cc03e-0f7e-40b4-84a4-d89876d5f481", "8ea72816-a99b-4630-afd7-a879eca7ccfa", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8b33c3bc-abb2-4f8c-ab54-201931979078", "52849c5f-e8f6-4fb6-85d5-741c9934e30a", "Employee", "EMPLOYEE" });
        }
    }
}
