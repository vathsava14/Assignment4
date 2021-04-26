using Microsoft.EntityFrameworkCore.Migrations;

namespace Assignment4.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Population",
                columns: table => new
                {
                    PopulationID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PopTypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Population", x => x.PopulationID);
                });

            migrationBuilder.CreateTable(
                name: "County",
                columns: table => new
                {
                    CountyID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountyName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_County", x => x.CountyID);
                });

            migrationBuilder.CreateTable(
                name: "Demographic",
                columns: table => new
                {
                    DemographicID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountyID = table.Column<int>(nullable: true),
                    PopulationID = table.Column<int>(nullable: true),
                    Value = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Demographic", x => x.DemographicID);
                    table.ForeignKey(
                        name: "FK_Demographic_Population_PopulationID",
                        column: x => x.PopulationID,
                        principalTable: "Population",
                        principalColumn: "PopulationID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Demographic_County_CountyID",
                        column: x => x.CountyID,
                        principalTable: "County",
                        principalColumn: "CountyID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Demographic_PopulationID",
                table: "Demographic",
                column: "PopulationID");

            migrationBuilder.CreateIndex(
                name: "IX_Demographic_CountyID",
                table: "Demographic",
                column: "CountyID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Demographic");

            migrationBuilder.DropTable(
                name: "Population");

            migrationBuilder.DropTable(
                name: "County");
        }
    }
}
