using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Foodies.Migrations
{
    public partial class addedCustomerCuisinePreferenceEditRestaurantModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5a2380a5-1f3a-4500-9bb4-1c7e0adc2172");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6bb5f58a-8ebf-4152-a370-46b09d2a41ed");

            migrationBuilder.AddColumn<string>(
                name: "Place_Id",
                table: "Restaurants",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CustomerCuisinePreferences",
                columns: table => new
                {
                    CCPMPrimaryKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerPK = table.Column<string>(nullable: true),
                    CuisineType = table.Column<string>(nullable: true),
                    PreferredOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerCuisinePreferences", x => x.CCPMPrimaryKey);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "67597e38-af4c-45be-aa66-76576cc61293", "7aacf5fe-5b6c-43d8-aabf-7e2610d0f8f3", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a5ca32f2-b572-47d8-bd06-95f370f1ad16", "7f9c0671-8015-489c-a893-af5facb2447c", "Employee", "EMPLOYEE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerCuisinePreferences");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "67597e38-af4c-45be-aa66-76576cc61293");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a5ca32f2-b572-47d8-bd06-95f370f1ad16");

            migrationBuilder.DropColumn(
                name: "Place_Id",
                table: "Restaurants");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5a2380a5-1f3a-4500-9bb4-1c7e0adc2172", "093f26b2-bed7-4d0d-92e9-0b70e90fd34e", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6bb5f58a-8ebf-4152-a370-46b09d2a41ed", "3514a695-88ea-410a-87b5-e9bc80741d35", "Employee", "EMPLOYEE" });
        }
    }
}
