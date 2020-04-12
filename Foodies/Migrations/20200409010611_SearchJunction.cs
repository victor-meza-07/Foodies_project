using Microsoft.EntityFrameworkCore.Migrations;

namespace Foodies.Migrations
{
    public partial class SearchJunction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1a4824e8-2fdb-4741-8ced-1c1b0d6a285d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a1b530d4-6b07-4eab-aae5-dbfae815acc4");

            migrationBuilder.RenameColumn(
                name: "rating",
                table: "Restaurants",
                newName: "Rating");

            migrationBuilder.RenameColumn(
                name: "price_level",
                table: "Restaurants",
                newName: "Price_level");

            migrationBuilder.RenameColumn(
                name: "width",
                table: "PhotosFromGoogle",
                newName: "Width");

            migrationBuilder.RenameColumn(
                name: "photo_reference",
                table: "PhotosFromGoogle",
                newName: "Photo_reference");

            migrationBuilder.RenameColumn(
                name: "height",
                table: "PhotosFromGoogle",
                newName: "Height");

            migrationBuilder.CreateTable(
                name: "SearchJunctions",
                columns: table => new
                {
                    JunctionPrimaryKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RestaurantModelPrimaryKey = table.Column<string>(nullable: true),
                    ApiPrimaryKey = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchJunctions", x => x.JunctionPrimaryKey);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5a2380a5-1f3a-4500-9bb4-1c7e0adc2172", "093f26b2-bed7-4d0d-92e9-0b70e90fd34e", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6bb5f58a-8ebf-4152-a370-46b09d2a41ed", "3514a695-88ea-410a-87b5-e9bc80741d35", "Employee", "EMPLOYEE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SearchJunctions");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5a2380a5-1f3a-4500-9bb4-1c7e0adc2172");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6bb5f58a-8ebf-4152-a370-46b09d2a41ed");

            migrationBuilder.RenameColumn(
                name: "Rating",
                table: "Restaurants",
                newName: "rating");

            migrationBuilder.RenameColumn(
                name: "Price_level",
                table: "Restaurants",
                newName: "price_level");

            migrationBuilder.RenameColumn(
                name: "Width",
                table: "PhotosFromGoogle",
                newName: "width");

            migrationBuilder.RenameColumn(
                name: "Photo_reference",
                table: "PhotosFromGoogle",
                newName: "photo_reference");

            migrationBuilder.RenameColumn(
                name: "Height",
                table: "PhotosFromGoogle",
                newName: "height");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a1b530d4-6b07-4eab-aae5-dbfae815acc4", "9065bc73-054c-4d33-97cc-35ddb5777d54", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1a4824e8-2fdb-4741-8ced-1c1b0d6a285d", "2c941acc-ad99-495e-aed2-1a27ee2af633", "Employee", "EMPLOYEE" });
        }
    }
}
