using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prueba.Migrations
{
    /// <inheritdoc />
    public partial class mnigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    year = table.Column<int>(type: "int", nullable: false),
                    model = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    brand = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    code_vin = table.Column<string>(type: "nvarchar(17)", maxLength: 17, nullable: false),
                    patent = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
