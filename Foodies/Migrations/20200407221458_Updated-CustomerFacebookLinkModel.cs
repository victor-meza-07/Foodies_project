using Microsoft.EntityFrameworkCore.Migrations;

namespace Foodies.Migrations
{
    public partial class UpdatedCustomerFacebookLinkModel : Migration
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

            migrationBuilder.DropColumn(
                name: "FacebookProfileId",
                table: "CustomerFacebookLink");

            migrationBuilder.AddColumn<int>(
                name: "UserAccessToken",
                table: "CustomerFacebookLink",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8008e247-4a55-4853-9f05-937b7d4796ef", "c4249271-dbd5-4383-b4e6-5877666b1438", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "36a7aeb2-d625-434e-bac7-cef41b8fe79a", "e01c87ec-f139-4b1f-8506-a6edc9b7c007", "Employee", "EMPLOYEE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "36a7aeb2-d625-434e-bac7-cef41b8fe79a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8008e247-4a55-4853-9f05-937b7d4796ef");

            migrationBuilder.DropColumn(
                name: "UserAccessToken",
                table: "CustomerFacebookLink");

            migrationBuilder.AddColumn<int>(
                name: "FacebookProfileId",
                table: "CustomerFacebookLink",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
