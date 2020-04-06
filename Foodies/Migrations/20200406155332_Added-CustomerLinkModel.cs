using Microsoft.EntityFrameworkCore.Migrations;

namespace Foodies.Migrations
{
    public partial class AddedCustomerLinkModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "40d0575c-05e1-4560-829f-faec4e6dda1a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "53ec6d0a-aed2-421f-83d2-c63c5a6e6771");

            migrationBuilder.CreateTable(
                name: "Foodies",
                columns: table => new
                {
                    CustomerLinkModelPrimaryKey = table.Column<string>(nullable: false),
                    CustomerOneKey = table.Column<string>(nullable: true),
                    CustomerTwoKey = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foodies", x => x.CustomerLinkModelPrimaryKey);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fc615d5b-28ef-470c-8fe5-4a6a1d835509", "a2bcc833-729b-4061-b4e9-474c3904d1df", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "507f3a3a-facc-40e7-abb6-8bb59b4820a3", "c28eb953-371c-459a-bbfa-dfd9504d4f11", "Employee", "EMPLOYEE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Foodies");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "507f3a3a-facc-40e7-abb6-8bb59b4820a3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fc615d5b-28ef-470c-8fe5-4a6a1d835509");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "53ec6d0a-aed2-421f-83d2-c63c5a6e6771", "1dbe012a-28fc-4f57-85cf-787ee5cd53f9", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "40d0575c-05e1-4560-829f-faec4e6dda1a", "ff90a708-1de6-4756-b474-05dab6f44e60", "Employee", "EMPLOYEE" });
        }
    }
}
