using Microsoft.EntityFrameworkCore.Migrations;

namespace Foodies.Migrations
{
    public partial class EditedCustomerFacebookLinkModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "36a7aeb2-d625-434e-bac7-cef41b8fe79a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8008e247-4a55-4853-9f05-937b7d4796ef");

            migrationBuilder.AlterColumn<string>(
                name: "UserAccessToken",
                table: "CustomerFacebookLink",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b552030a-40c8-4d47-b92f-e19e86d4e937", "7ac3f066-5df5-4814-9b5d-f1eae30d27ee", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bcd5360f-8693-4d1a-a19a-3b42683df46c", "5a1d4b30-0c07-4435-800d-b6d9652c9504", "Employee", "EMPLOYEE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b552030a-40c8-4d47-b92f-e19e86d4e937");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bcd5360f-8693-4d1a-a19a-3b42683df46c");

            migrationBuilder.AlterColumn<int>(
                name: "UserAccessToken",
                table: "CustomerFacebookLink",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8008e247-4a55-4853-9f05-937b7d4796ef", "c4249271-dbd5-4383-b4e6-5877666b1438", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "36a7aeb2-d625-434e-bac7-cef41b8fe79a", "e01c87ec-f139-4b1f-8506-a6edc9b7c007", "Employee", "EMPLOYEE" });
        }
    }
}
