using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace iRLeagueDatabaseCore.Migrations
{
    public partial class DeleteResultsFilterOptionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResultsFilterOptions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ResultsFilterOptions",
                columns: table => new
                {
                    LeagueId = table.Column<long>(type: "bigint", nullable: false),
                    ResultsFilterId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    PointRuleId = table.Column<long>(type: "bigint", nullable: true),
                    ScoringId = table.Column<long>(type: "bigint", nullable: true),
                    ColumnPropertyName = table.Column<string>(type: "longtext", nullable: true),
                    Comparator = table.Column<int>(type: "int", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "longtext", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "longtext", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    FilterValues = table.Column<string>(type: "longtext", nullable: true),
                    Include = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LastModifiedByUserId = table.Column<string>(type: "longtext", nullable: true),
                    LastModifiedByUserName = table.Column<string>(type: "longtext", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    ResultsFilterType = table.Column<string>(type: "longtext", nullable: true),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResultsFilterOptions", x => new { x.LeagueId, x.ResultsFilterId });
                    table.UniqueConstraint("AK_ResultsFilterOptions_ResultsFilterId", x => x.ResultsFilterId);
                    table.ForeignKey(
                        name: "FK_ResultsFilterOptions_PointRules_LeagueId_PointRuleId",
                        columns: x => new { x.LeagueId, x.PointRuleId },
                        principalTable: "PointRules",
                        principalColumns: new[] { "LeagueId", "PointRuleId" });
                    table.ForeignKey(
                        name: "FK_ResultsFilterOptions_Scorings_LeagueId_ScoringId",
                        columns: x => new { x.LeagueId, x.ScoringId },
                        principalTable: "Scorings",
                        principalColumns: new[] { "LeagueId", "ScoringId" });
                });

            migrationBuilder.CreateIndex(
                name: "IX_ResultsFilterOptions_LeagueId_PointRuleId",
                table: "ResultsFilterOptions",
                columns: new[] { "LeagueId", "PointRuleId" });

            migrationBuilder.CreateIndex(
                name: "IX_ResultsFilterOptions_LeagueId_ScoringId",
                table: "ResultsFilterOptions",
                columns: new[] { "LeagueId", "ScoringId" });

            migrationBuilder.CreateIndex(
                name: "IX_ResultsFilterOptions_PointRuleId",
                table: "ResultsFilterOptions",
                column: "PointRuleId");

            migrationBuilder.CreateIndex(
                name: "IX_ResultsFilterOptions_ScoringId",
                table: "ResultsFilterOptions",
                column: "ScoringId");
        }
    }
}
