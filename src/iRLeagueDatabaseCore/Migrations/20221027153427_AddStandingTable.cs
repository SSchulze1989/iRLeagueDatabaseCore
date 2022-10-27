using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace iRLeagueDatabaseCore.Migrations
{
    public partial class AddStandingTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Standings",
                columns: table => new
                {
                    LeagueId = table.Column<long>(type: "bigint", nullable: false),
                    StandingId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    SeasonId = table.Column<long>(type: "bigint", nullable: false),
                    StandingConfigId = table.Column<long>(type: "bigint", nullable: true),
                    EventId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Version = table.Column<int>(type: "int", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "longtext", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "longtext", nullable: true),
                    LastModifiedByUserId = table.Column<string>(type: "longtext", nullable: true),
                    LastModifiedByUserName = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Standings", x => new { x.LeagueId, x.StandingId });
                    table.UniqueConstraint("AK_Standings_StandingId", x => x.StandingId);
                    table.ForeignKey(
                        name: "FK_Standings_Events_LeagueId_EventId",
                        columns: x => new { x.LeagueId, x.EventId },
                        principalTable: "Events",
                        principalColumns: new[] { "LeagueId", "EventId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Standings_Seasons_LeagueId_SeasonId",
                        columns: x => new { x.LeagueId, x.SeasonId },
                        principalTable: "Seasons",
                        principalColumns: new[] { "LeagueId", "SeasonId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StandingRowEntity",
                columns: table => new
                {
                    LeagueId = table.Column<long>(type: "bigint", nullable: false),
                    StandingRowId = table.Column<long>(type: "bigint", nullable: false),
                    StandingId = table.Column<long>(type: "bigint", nullable: false),
                    MemberId = table.Column<long>(type: "bigint", nullable: true),
                    TeamId = table.Column<long>(type: "bigint", nullable: true),
                    Position = table.Column<int>(type: "int", nullable: false),
                    LastPosition = table.Column<int>(type: "int", nullable: false),
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    CarClass = table.Column<string>(type: "longtext", nullable: true),
                    RacePoints = table.Column<int>(type: "int", nullable: false),
                    RacePointsChange = table.Column<int>(type: "int", nullable: false),
                    PenaltyPoints = table.Column<int>(type: "int", nullable: false),
                    PenaltyPointsChange = table.Column<int>(type: "int", nullable: false),
                    TotalPoints = table.Column<int>(type: "int", nullable: false),
                    TotalPointsChange = table.Column<int>(type: "int", nullable: false),
                    Races = table.Column<int>(type: "int", nullable: false),
                    RacesCounted = table.Column<int>(type: "int", nullable: false),
                    DroppedResultCount = table.Column<int>(type: "int", nullable: false),
                    CompletedLaps = table.Column<int>(type: "int", nullable: false),
                    CompletedLapsChange = table.Column<int>(type: "int", nullable: false),
                    LeadLaps = table.Column<int>(type: "int", nullable: false),
                    LeadLapsChange = table.Column<int>(type: "int", nullable: false),
                    FastestLaps = table.Column<int>(type: "int", nullable: false),
                    FastestLapsChange = table.Column<int>(type: "int", nullable: false),
                    PolePositions = table.Column<int>(type: "int", nullable: false),
                    PolePositionsChange = table.Column<int>(type: "int", nullable: false),
                    Wins = table.Column<int>(type: "int", nullable: false),
                    WinsChange = table.Column<int>(type: "int", nullable: false),
                    Top3 = table.Column<int>(type: "int", nullable: false),
                    Top5 = table.Column<int>(type: "int", nullable: false),
                    Top10 = table.Column<int>(type: "int", nullable: false),
                    Incidents = table.Column<int>(type: "int", nullable: false),
                    IncidentsChange = table.Column<int>(type: "int", nullable: false),
                    PositionChange = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StandingRowEntity", x => new { x.LeagueId, x.StandingRowId });
                    table.UniqueConstraint("AK_StandingRowEntity_StandingRowId", x => x.StandingRowId);
                    table.ForeignKey(
                        name: "FK_StandingRowEntity_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StandingRowEntity_Standings_LeagueId_StandingRowId",
                        columns: x => new { x.LeagueId, x.StandingRowId },
                        principalTable: "Standings",
                        principalColumns: new[] { "LeagueId", "StandingId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StandingRowEntity_Teams_LeagueId_TeamId",
                        columns: x => new { x.LeagueId, x.TeamId },
                        principalTable: "Teams",
                        principalColumns: new[] { "LeagueId", "TeamId" });
                });

            migrationBuilder.CreateTable(
                name: "StandingRows_ScoredResultRows",
                columns: table => new
                {
                    LeagueId = table.Column<long>(type: "bigint", nullable: false),
                    StandingRowRefId = table.Column<long>(type: "bigint", nullable: false),
                    ScoredResultRowRefId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StandingRows_ScoredResultRows", x => new { x.ScoredResultRowRefId, x.LeagueId, x.StandingRowRefId });
                    table.ForeignKey(
                        name: "FK_StandingRows_ScoredResultRows_ScoredResultRows_LeagueId_Scor~",
                        columns: x => new { x.LeagueId, x.ScoredResultRowRefId },
                        principalTable: "ScoredResultRows",
                        principalColumns: new[] { "LeagueId", "ScoredResultRowId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StandingRows_ScoredResultRows_StandingRowEntity_LeagueId_Sta~",
                        columns: x => new { x.LeagueId, x.StandingRowRefId },
                        principalTable: "StandingRowEntity",
                        principalColumns: new[] { "LeagueId", "StandingRowId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StandingRowEntity_LeagueId_TeamId",
                table: "StandingRowEntity",
                columns: new[] { "LeagueId", "TeamId" });

            migrationBuilder.CreateIndex(
                name: "IX_StandingRowEntity_MemberId",
                table: "StandingRowEntity",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_StandingRows_ScoredResultRows_LeagueId_ScoredResultRowRefId",
                table: "StandingRows_ScoredResultRows",
                columns: new[] { "LeagueId", "ScoredResultRowRefId" });

            migrationBuilder.CreateIndex(
                name: "IX_StandingRows_ScoredResultRows_LeagueId_StandingRowRefId",
                table: "StandingRows_ScoredResultRows",
                columns: new[] { "LeagueId", "StandingRowRefId" });

            migrationBuilder.CreateIndex(
                name: "IX_Standings_LeagueId_EventId",
                table: "Standings",
                columns: new[] { "LeagueId", "EventId" });

            migrationBuilder.CreateIndex(
                name: "IX_Standings_LeagueId_SeasonId",
                table: "Standings",
                columns: new[] { "LeagueId", "SeasonId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StandingRows_ScoredResultRows");

            migrationBuilder.DropTable(
                name: "StandingRowEntity");

            migrationBuilder.DropTable(
                name: "Standings");
        }
    }
}
