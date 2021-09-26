using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

namespace SnlMaps.Web.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    InseeCode = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Population = table.Column<int>(type: "integer", nullable: false),
                    Geometry = table.Column<Polygon>(type: "geometry", nullable: true),
                    PostCode = table.Column<string>(type: "text", nullable: true),
                    Location = table.Column<Point>(type: "geometry", nullable: true),
                    SruDeficit = table.Column<bool>(type: "boolean", nullable: false),
                    SocialHousingRate = table.Column<decimal>(type: "numeric", nullable: false),
                    SocialHousingCount = table.Column<int>(type: "integer", nullable: false),
                    SnlHousingCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.InseeCode);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
