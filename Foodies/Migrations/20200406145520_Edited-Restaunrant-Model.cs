using Microsoft.EntityFrameworkCore.Migrations;

namespace Foodies.Migrations
{
    public partial class EditedRestaunrantModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_AddressModel_AddressModelAddressKey",
                table: "Restaurants");

            migrationBuilder.DropTable(
                name: "AddressModel");

            migrationBuilder.DropIndex(
                name: "IX_Restaurants_AddressModelAddressKey",
                table: "Restaurants");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a5c105b9-bda0-405f-bb6f-e143601abff5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e4fdde37-ae96-4483-b235-3a5b84356916");

            migrationBuilder.DropColumn(
                name: "AddressModelAddressKey",
                table: "Restaurants");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "abbe18de-e7c8-439d-9157-e131ed1bed73", "84864fbc-a112-4309-977a-b3dbc09098c6", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1f6b2daf-83e2-4e44-ad4d-bbcd2cdcb233", "2b9f6eec-a161-4625-bfc8-52dd0b3bfce4", "Employee", "EMPLOYEE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1f6b2daf-83e2-4e44-ad4d-bbcd2cdcb233");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "abbe18de-e7c8-439d-9157-e131ed1bed73");

            migrationBuilder.AddColumn<string>(
                name: "AddressModelAddressKey",
                table: "Restaurants",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AddressModel",
                columns: table => new
                {
                    AddressKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BuildingNumber = table.Column<int>(type: "int", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StreetName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZipCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressModel", x => x.AddressKey);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e4fdde37-ae96-4483-b235-3a5b84356916", "eac7d0fd-c491-4c10-82ff-a1de6ac9a964", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a5c105b9-bda0-405f-bb6f-e143601abff5", "7b8041c1-5b3f-4a15-a995-e4718feab353", "Employee", "EMPLOYEE" });

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_AddressModelAddressKey",
                table: "Restaurants",
                column: "AddressModelAddressKey");

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_AddressModel_AddressModelAddressKey",
                table: "Restaurants",
                column: "AddressModelAddressKey",
                principalTable: "AddressModel",
                principalColumn: "AddressKey",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
