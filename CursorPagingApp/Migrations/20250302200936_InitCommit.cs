using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CursorPagingApp.Migrations
{
    /// <inheritdoc />
    public partial class InitCommit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "Name" },
                values: new object[,]
                {
                    { 1, "Clothing", "Product 1" },
                    { 2, "Toys", "Product 2" },
                    { 3, "Electronics", "Product 3" },
                    { 4, "Clothing", "Product 4" },
                    { 5, "Toys", "Product 5" },
                    { 6, "Electronics", "Product 6" },
                    { 7, "Clothing", "Product 7" },
                    { 8, "Toys", "Product 8" },
                    { 9, "Electronics", "Product 9" },
                    { 10, "Clothing", "Product 10" },
                    { 11, "Toys", "Product 11" },
                    { 12, "Electronics", "Product 12" },
                    { 13, "Clothing", "Product 13" },
                    { 14, "Toys", "Product 14" },
                    { 15, "Electronics", "Product 15" },
                    { 16, "Clothing", "Product 16" },
                    { 17, "Toys", "Product 17" },
                    { 18, "Electronics", "Product 18" },
                    { 19, "Clothing", "Product 19" },
                    { 20, "Toys", "Product 20" },
                    { 21, "Electronics", "Product 21" },
                    { 22, "Clothing", "Product 22" },
                    { 23, "Toys", "Product 23" },
                    { 24, "Electronics", "Product 24" },
                    { 25, "Clothing", "Product 25" },
                    { 26, "Toys", "Product 26" },
                    { 27, "Electronics", "Product 27" },
                    { 28, "Clothing", "Product 28" },
                    { 29, "Toys", "Product 29" },
                    { 30, "Electronics", "Product 30" },
                    { 31, "Clothing", "Product 31" },
                    { 32, "Toys", "Product 32" },
                    { 33, "Electronics", "Product 33" },
                    { 34, "Clothing", "Product 34" },
                    { 35, "Toys", "Product 35" },
                    { 36, "Electronics", "Product 36" },
                    { 37, "Clothing", "Product 37" },
                    { 38, "Toys", "Product 38" },
                    { 39, "Electronics", "Product 39" },
                    { 40, "Clothing", "Product 40" },
                    { 41, "Toys", "Product 41" },
                    { 42, "Electronics", "Product 42" },
                    { 43, "Clothing", "Product 43" },
                    { 44, "Toys", "Product 44" },
                    { 45, "Electronics", "Product 45" },
                    { 46, "Clothing", "Product 46" },
                    { 47, "Toys", "Product 47" },
                    { 48, "Electronics", "Product 48" },
                    { 49, "Clothing", "Product 49" },
                    { 50, "Toys", "Product 50" },
                    { 51, "Electronics", "Product 51" },
                    { 52, "Clothing", "Product 52" },
                    { 53, "Toys", "Product 53" },
                    { 54, "Electronics", "Product 54" },
                    { 55, "Clothing", "Product 55" },
                    { 56, "Toys", "Product 56" },
                    { 57, "Electronics", "Product 57" },
                    { 58, "Clothing", "Product 58" },
                    { 59, "Toys", "Product 59" },
                    { 60, "Electronics", "Product 60" },
                    { 61, "Clothing", "Product 61" },
                    { 62, "Toys", "Product 62" },
                    { 63, "Electronics", "Product 63" },
                    { 64, "Clothing", "Product 64" },
                    { 65, "Toys", "Product 65" },
                    { 66, "Electronics", "Product 66" },
                    { 67, "Clothing", "Product 67" },
                    { 68, "Toys", "Product 68" },
                    { 69, "Electronics", "Product 69" },
                    { 70, "Clothing", "Product 70" },
                    { 71, "Toys", "Product 71" },
                    { 72, "Electronics", "Product 72" },
                    { 73, "Clothing", "Product 73" },
                    { 74, "Toys", "Product 74" },
                    { 75, "Electronics", "Product 75" },
                    { 76, "Clothing", "Product 76" },
                    { 77, "Toys", "Product 77" },
                    { 78, "Electronics", "Product 78" },
                    { 79, "Clothing", "Product 79" },
                    { 80, "Toys", "Product 80" },
                    { 81, "Electronics", "Product 81" },
                    { 82, "Clothing", "Product 82" },
                    { 83, "Toys", "Product 83" },
                    { 84, "Electronics", "Product 84" },
                    { 85, "Clothing", "Product 85" },
                    { 86, "Toys", "Product 86" },
                    { 87, "Electronics", "Product 87" },
                    { 88, "Clothing", "Product 88" },
                    { 89, "Toys", "Product 89" },
                    { 90, "Electronics", "Product 90" },
                    { 91, "Clothing", "Product 91" },
                    { 92, "Toys", "Product 92" },
                    { 93, "Electronics", "Product 93" },
                    { 94, "Clothing", "Product 94" },
                    { 95, "Toys", "Product 95" },
                    { 96, "Electronics", "Product 96" },
                    { 97, "Clothing", "Product 97" },
                    { 98, "Toys", "Product 98" },
                    { 99, "Electronics", "Product 99" },
                    { 100, "Clothing", "Product 100" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
