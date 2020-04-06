using Microsoft.EntityFrameworkCore.Migrations;

namespace Foodies.Migrations
{
    public partial class AddedRestaurants : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "435aaa53-f777-47c2-90f6-b6af97c283ac");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "782716f0-d39f-4938-bbb0-3aac5867a3eb");

            migrationBuilder.CreateTable(
                name: "AddressModel",
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
                    table.PrimaryKey("PK_AddressModel", x => x.AddressKey);
                });

            migrationBuilder.CreateTable(
                name: "Restaurants",
                columns: table => new
                {
                    RestaurantModelPrimaryKey = table.Column<string>(nullable: false),
                    RestaurantName = table.Column<string>(nullable: true),
                    RestaurantPhone = table.Column<string>(nullable: true),
                    AddressKey = table.Column<string>(nullable: true),
                    PriceRangeIndex = table.Column<int>(nullable: false),
                    WebsiteUrl = table.Column<string>(nullable: true),
                    MenuUrl = table.Column<string>(nullable: true),
                    GoogleGeoLocationData = table.Column<string>(nullable: true),
                    AddressModelAddressKey = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurants", x => x.RestaurantModelPrimaryKey);
                    table.ForeignKey(
                        name: "FK_Restaurants_AddressModel_AddressModelAddressKey",
                        column: x => x.AddressModelAddressKey,
                        principalTable: "AddressModel",
                        principalColumn: "AddressKey",
                        onDelete: ReferentialAction.Restrict);
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Restaurants");

            migrationBuilder.DropTable(
                name: "AddressModel");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a5c105b9-bda0-405f-bb6f-e143601abff5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e4fdde37-ae96-4483-b235-3a5b84356916");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "782716f0-d39f-4938-bbb0-3aac5867a3eb", "51e19910-fd6c-405c-a2d2-0422218d6cea", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "435aaa53-f777-47c2-90f6-b6af97c283ac", "17cd7d27-83e0-4427-8b28-e28df91152e6", "Employee", "EMPLOYEE" });
        }
    }
}
