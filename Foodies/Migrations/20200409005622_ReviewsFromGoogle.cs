using Microsoft.EntityFrameworkCore.Migrations;

namespace Foodies.Migrations
{
    public partial class ReviewsFromGoogle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "83e4e7c5-9fbd-486a-88ec-7cd11035ee46");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a42bd1d0-5330-4989-861d-2138185e21fe");

            migrationBuilder.RenameColumn(
                name: "open_now",
                table: "Restaurants",
                newName: "Open_now");

            migrationBuilder.RenameColumn(
                name: "lng",
                table: "Restaurants",
                newName: "Lng");

            migrationBuilder.RenameColumn(
                name: "lat",
                table: "Restaurants",
                newName: "Lat");

            migrationBuilder.CreateTable(
                name: "ReviewsFromGoogle",
                columns: table => new
                {
                    ReviewsPrimaryKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RestaurantGuid = table.Column<string>(nullable: true),
                    Author_name = table.Column<string>(nullable: true),
                    Author_url = table.Column<string>(nullable: true),
                    Language = table.Column<string>(nullable: true),
                    Profile_photo_url = table.Column<string>(nullable: true),
                    Rating = table.Column<int>(nullable: false),
                    Relative_time_description = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    Time = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewsFromGoogle", x => x.ReviewsPrimaryKey);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a1b530d4-6b07-4eab-aae5-dbfae815acc4", "9065bc73-054c-4d33-97cc-35ddb5777d54", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1a4824e8-2fdb-4741-8ced-1c1b0d6a285d", "2c941acc-ad99-495e-aed2-1a27ee2af633", "Employee", "EMPLOYEE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReviewsFromGoogle");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1a4824e8-2fdb-4741-8ced-1c1b0d6a285d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a1b530d4-6b07-4eab-aae5-dbfae815acc4");

            migrationBuilder.RenameColumn(
                name: "Open_now",
                table: "Restaurants",
                newName: "open_now");

            migrationBuilder.RenameColumn(
                name: "Lng",
                table: "Restaurants",
                newName: "lng");

            migrationBuilder.RenameColumn(
                name: "Lat",
                table: "Restaurants",
                newName: "lat");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "83e4e7c5-9fbd-486a-88ec-7cd11035ee46", "93322545-2fa2-48aa-9b9a-935dac567ca0", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a42bd1d0-5330-4989-861d-2138185e21fe", "6b0b8800-f683-4726-a1e1-561b13ba3bf5", "Employee", "EMPLOYEE" });
        }
    }
}
