using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iRLeagueDatabaseCore.Migrations
{
    public partial class RemoveOldStandingTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StatisticSets_Standings_LeagueId_StandingId",
                table: "StatisticSets");

            migrationBuilder.DropTable(
                name: "StandingsScorings");

            migrationBuilder.DropTable(
                name: "Standings");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Standings",
                columns: table => new
                {
                    LeagueId = table.Column<long>(type: "bigint", nullable: false),
                    StandingId = table.Column<long>(type: "bigint", nullable: false),
                    SeasonId = table.Column<long>(type: "bigint", nullable: false),
                    AverageRaceNr = table.Column<int>(type: "int", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "longtext", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "longtext", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    DropRacesOption = table.Column<int>(type: "int", nullable: false),
                    DropWeeks = table.Column<int>(type: "int", nullable: false),
                    LastModifiedByUserId = table.Column<string>(type: "longtext", nullable: true),
                    LastModifiedByUserName = table.Column<string>(type: "longtext", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    Name = table.Column<string>(type: "longtext", nullable: true),
                    ResultsPerRaceCount = table.Column<int>(type: "int", nullable: false),
                    ScoringFactors = table.Column<string>(type: "longtext", nullable: true),
                    ScoringKind = table.Column<int>(type: "int", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Standings", x => new { x.LeagueId, x.StandingId });
                    table.ForeignKey(
                        name: "FK_Standings_Seasons_LeagueId_SeasonId",
                        columns: x => new { x.LeagueId, x.SeasonId },
                        principalTable: "Seasons",
                        principalColumns: new[] { "LeagueId", "SeasonId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StandingsScorings",
                columns: table => new
                {
                    ScoringRefId = table.Column<long>(type: "bigint", nullable: false),
                    LeagueId = table.Column<long>(type: "bigint", nullable: false),
                    StandingRefId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StandingsScorings", x => new { x.ScoringRefId, x.LeagueId, x.StandingRefId });
                    table.ForeignKey(
                        name: "FK_StandingsScorings_Scorings_LeagueId_ScoringRefId",
                        columns: x => new { x.LeagueId, x.ScoringRefId },
                        principalTable: "Scorings",
                        principalColumns: new[] { "LeagueId", "ScoringId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StandingsScorings_Standings_LeagueId_StandingRefId",
                        columns: x => new { x.LeagueId, x.StandingRefId },
                        principalTable: "Standings",
                        principalColumns: new[] { "LeagueId", "StandingId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Standings_LeagueId_SeasonId",
                table: "Standings",
                columns: new[] { "LeagueId", "SeasonId" });

            migrationBuilder.CreateIndex(
                name: "IX_StandingsScorings_LeagueId_ScoringRefId",
                table: "StandingsScorings",
                columns: new[] { "LeagueId", "ScoringRefId" });

            migrationBuilder.CreateIndex(
                name: "IX_StandingsScorings_LeagueId_StandingRefId",
                table: "StandingsScorings",
                columns: new[] { "LeagueId", "StandingRefId" });

            migrationBuilder.AddForeignKey(
                name: "FK_StatisticSets_Standings_LeagueId_StandingId",
                table: "StatisticSets",
                columns: new[] { "LeagueId", "StandingId" },
                principalTable: "Standings",
                principalColumns: new[] { "LeagueId", "StandingId" });
        }
    }
}
