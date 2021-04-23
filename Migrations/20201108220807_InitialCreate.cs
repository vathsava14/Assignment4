using Microsoft.EntityFrameworkCore.Migrations;

namespace Assignment4.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EnergySource",
                columns: table => new
                {
                    EnergySourceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SourceName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnergySource", x => x.EnergySourceId);
                });

            migrationBuilder.CreateTable(
                name: "Sector",
                columns: table => new
                {
                    SectorId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SectorName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sector", x => x.SectorId);
                });

            migrationBuilder.CreateTable(
                name: "AnnualEnergyConsumption",
                columns: table => new
                {
                    Year = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SectorId = table.Column<int>(nullable: true),
                    EnergySourceId = table.Column<int>(nullable: true),
                    Value = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnualEnergyConsumption", x => x.Year);
                    table.ForeignKey(
                        name: "FK_AnnualEnergyConsumption_EnergySource_EnergySourceId",
                        column: x => x.EnergySourceId,
                        principalTable: "EnergySource",
                        principalColumn: "EnergySourceId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnnualEnergyConsumption_Sector_SectorId",
                        column: x => x.SectorId,
                        principalTable: "Sector",
                        principalColumn: "SectorId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnnualEnergyConsumption_EnergySourceId",
                table: "AnnualEnergyConsumption",
                column: "EnergySourceId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnualEnergyConsumption_SectorId",
                table: "AnnualEnergyConsumption",
                column: "SectorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnnualEnergyConsumption");

            migrationBuilder.DropTable(
                name: "EnergySource");

            migrationBuilder.DropTable(
                name: "Sector");
        }
    }
}
