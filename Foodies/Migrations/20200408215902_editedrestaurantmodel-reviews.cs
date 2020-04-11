using Microsoft.EntityFrameworkCore.Migrations;

namespace Foodies.Migrations
{
    public partial class editedrestaurantmodelreviews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "89e08e78-1601-40ca-8c57-9b620fa4c8fa");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a9f322f3-717d-4d6e-8e3f-dcaf6afdefe1");

            migrationBuilder.DropColumn(
                name: "GoogleGeoLocationData",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "MenuUrl",
                table: "Restaurants");

            migrationBuilder.AddColumn<float>(
                name: "lat",
                table: "Restaurants",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "lng",
                table: "Restaurants",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<bool>(
                name: "open_now",
                table: "Restaurants",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "price_level",
                table: "Restaurants",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "rating",
                table: "Restaurants",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.CreateTable(
                name: "PhotosFromGoogle",
                columns: table => new
                {
                    PhotosPrimaryKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RestaurantGuid = table.Column<string>(nullable: true),
                    height = table.Column<int>(nullable: false),
                    photo_reference = table.Column<string>(nullable: true),
                    width = table.Column<int>(nullable: false),
                    RestaurantModelPrimaryKey = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotosFromGoogle", x => x.PhotosPrimaryKey);
                    table.ForeignKey(
                        name: "FK_PhotosFromGoogle_Restaurants_RestaurantModelPrimaryKey",
                        column: x => x.RestaurantModelPrimaryKey,
                        principalTable: "Restaurants",
                        principalColumn: "RestaurantModelPrimaryKey",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "83e4e7c5-9fbd-486a-88ec-7cd11035ee46", "93322545-2fa2-48aa-9b9a-935dac567ca0", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a42bd1d0-5330-4989-861d-2138185e21fe", "6b0b8800-f683-4726-a1e1-561b13ba3bf5", "Employee", "EMPLOYEE" });

            migrationBuilder.CreateIndex(
                name: "IX_PhotosFromGoogle_RestaurantModelPrimaryKey",
                table: "PhotosFromGoogle",
                column: "RestaurantModelPrimaryKey");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhotosFromGoogle");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "83e4e7c5-9fbd-486a-88ec-7cd11035ee46");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a42bd1d0-5330-4989-861d-2138185e21fe");

            migrationBuilder.DropColumn(
                name: "lat",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "lng",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "open_now",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "price_level",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "rating",
                table: "Restaurants");

            migrationBuilder.AddColumn<string>(
                name: "GoogleGeoLocationData",
                table: "Restaurants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MenuUrl",
                table: "Restaurants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "89e08e78-1601-40ca-8c57-9b620fa4c8fa", "8fbf4907-c243-46a1-b05d-1b495adf41d7", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a9f322f3-717d-4d6e-8e3f-dcaf6afdefe1", "26f45bd0-865f-465e-af42-86c7a640e291", "Employee", "EMPLOYEE" });
        }
    }
}
