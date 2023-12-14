using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace iRLeagueDatabaseCore.Migrations
{
    public partial class AddAdditionalBonusPoints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AddBonuses",
                columns: table => new
                {
                    LeagueId = table.Column<long>(type: "bigint", nullable: false),
                    AddBonusId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ScoredResultRowId = table.Column<long>(type: "bigint", nullable: false),
                    Reason = table.Column<string>(type: "varchar(2048)", maxLength: 2048, nullable: true),
                    BonusPoints = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddBonuses", x => new { x.LeagueId, x.AddBonusId });
                    table.UniqueConstraint("AK_AddBonuses_AddBonusId", x => x.AddBonusId);
                    table.ForeignKey(
                        name: "FK_AddBonuses_ScoredResultRows_LeagueId_ScoredResultRowId",
                        columns: x => new { x.LeagueId, x.ScoredResultRowId },
                        principalTable: "ScoredResultRows",
                        principalColumns: new[] { "LeagueId", "ScoredResultRowId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AddBonuses_LeagueId_ScoredResultRowId",
                table: "AddBonuses",
                columns: new[] { "LeagueId", "ScoredResultRowId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AddBonuses");
        }
    }
}
