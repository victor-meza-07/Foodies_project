using Microsoft.EntityFrameworkCore.Migrations;

namespace Foodies.Migrations
{
    public partial class AddedAPIMODELSCustomerFacebookLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "507f3a3a-facc-40e7-abb6-8bb59b4820a3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fc615d5b-28ef-470c-8fe5-4a6a1d835509");

            migrationBuilder.CreateTable(
                name: "CustomerFacebookLink",
                columns: table => new
                {
                    CustomerFacebookKey = table.Column<string>(nullable: false),
                    CustomerGUID = table.Column<string>(nullable: true),
                    FacebookProfileId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerFacebookLink", x => x.CustomerFacebookKey);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b6dd5ef4-8bdf-4eee-94a1-4dd7f3f8035d", "1f621b95-6ab9-42d5-a09c-b50a2e9b1ec9", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "39583a29-be99-44fd-a339-d016b2cb50c9", "9395d7c1-efd3-490c-89cd-1d3420eb8e7a", "Employee", "EMPLOYEE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerFacebookLink");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "39583a29-be99-44fd-a339-d016b2cb50c9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b6dd5ef4-8bdf-4eee-94a1-4dd7f3f8035d");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fc615d5b-28ef-470c-8fe5-4a6a1d835509", "a2bcc833-729b-4061-b4e9-474c3904d1df", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "507f3a3a-facc-40e7-abb6-8bb59b4820a3", "c28eb953-371c-459a-bbfa-dfd9504d4f11", "Employee", "EMPLOYEE" });
        }
    }
}
