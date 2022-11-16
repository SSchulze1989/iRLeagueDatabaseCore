using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace iRLeagueDatabaseCore.Migrations
{
    public partial class AddTablesWithLeagueCombinedKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AddPenaltys",
                columns: table => new
                {
                    LeagueId = table.Column<long>(type: "bigint", nullable: false),
                    ScoredResultRowId = table.Column<long>(type: "bigint", nullable: false),
                    PenaltyPoints = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddPenaltys", x => new { x.LeagueId, x.ScoredResultRowId });
                    table.ForeignKey(
                        name: "FK_AddPenaltys_ScoredResultRows_LeagueId_ScoredResultRowId",
                        columns: x => new { x.LeagueId, x.ScoredResultRowId },
                        principalTable: "ScoredResultRows",
                        principalColumns: new[] { "LeagueId", "ScoredResultRowId" });
                });

            migrationBuilder.CreateTable(
                name: "CustomIncidents",
                columns: table => new
                {
                    IncidentId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    LeagueId = table.Column<long>(type: "bigint", nullable: false),
                    Text = table.Column<string>(type: "longtext", nullable: true),
                    Index = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomIncidents", x => new { x.LeagueId, x.IncidentId });
                    table.UniqueConstraint("AK_CustomIncidents_IncidentId", x => x.IncidentId);
                    table.ForeignKey(
                        name: "FK_CustomIncidents_Leagues_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "Leagues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResultsFilterOptions",
                columns: table => new
                {
                    LeagueId = table.Column<long>(type: "bigint", nullable: false),
                    ResultsFilterId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ScoringId = table.Column<long>(type: "bigint", nullable: true),
                    PointRuleId = table.Column<long>(type: "bigint", nullable: true),
                    ResultsFilterType = table.Column<string>(type: "longtext", nullable: true),
                    ColumnPropertyName = table.Column<string>(type: "longtext", nullable: true),
                    Comparator = table.Column<int>(type: "int", nullable: false),
                    Include = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    FilterValues = table.Column<string>(type: "longtext", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    Version = table.Column<int>(type: "int", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "longtext", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "longtext", nullable: true),
                    LastModifiedByUserId = table.Column<string>(type: "longtext", nullable: true),
                    LastModifiedByUserName = table.Column<string>(type: "longtext", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "StatisticSets",
                columns: table => new
                {
                    LeagueId = table.Column<long>(type: "bigint", nullable: false),
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true),
                    UpdateInterval = table.Column<long>(type: "bigint", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    RequiresRecalculation = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    Version = table.Column<int>(type: "int", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "longtext", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "longtext", nullable: true),
                    LastModifiedByUserId = table.Column<string>(type: "longtext", nullable: true),
                    LastModifiedByUserName = table.Column<string>(type: "longtext", nullable: true),
                    CurrentChampId = table.Column<long>(type: "bigint", nullable: true),
                    SeasonId = table.Column<long>(type: "bigint", nullable: true),
                    StandingId = table.Column<long>(type: "bigint", nullable: true),
                    FinishedRaces = table.Column<int>(type: "int", nullable: true),
                    IsSeasonFinished = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    ImportSource = table.Column<string>(type: "longtext", nullable: true),
                    Description = table.Column<string>(type: "longtext", nullable: true),
                    FirstDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatisticSets", x => new { x.LeagueId, x.Id });
                    table.UniqueConstraint("AK_StatisticSets_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StatisticSets_Members_CurrentChampId",
                        column: x => x.CurrentChampId,
                        principalTable: "Members",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StatisticSets_Seasons_LeagueId_SeasonId",
                        columns: x => new { x.LeagueId, x.SeasonId },
                        principalTable: "Seasons",
                        principalColumns: new[] { "LeagueId", "SeasonId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DriverStatisticRows",
                columns: table => new
                {
                    StatisticSetId = table.Column<long>(type: "bigint", nullable: false),
                    LeagueId = table.Column<long>(type: "bigint", nullable: false),
                    MemberId = table.Column<long>(type: "bigint", nullable: false),
                    FirstResultRowId = table.Column<long>(type: "bigint", nullable: true),
                    LastResultRowId = table.Column<long>(type: "bigint", nullable: true),
                    StartIRating = table.Column<int>(type: "int", nullable: false),
                    EndIRating = table.Column<int>(type: "int", nullable: false),
                    StartSRating = table.Column<double>(type: "double", nullable: false),
                    EndSRating = table.Column<double>(type: "double", nullable: false),
                    FirstSessionId = table.Column<long>(type: "bigint", nullable: true),
                    FirstSessionDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    FirstRaceId = table.Column<long>(type: "bigint", nullable: true),
                    FirstRaceDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastSessionId = table.Column<long>(type: "bigint", nullable: true),
                    LastSessionDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastRaceId = table.Column<long>(type: "bigint", nullable: true),
                    LastRaceDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    RacePoints = table.Column<double>(type: "double", nullable: false),
                    TotalPoints = table.Column<double>(type: "double", nullable: false),
                    BonusPoints = table.Column<double>(type: "double", nullable: false),
                    Races = table.Column<int>(type: "int", nullable: false),
                    Wins = table.Column<int>(type: "int", nullable: false),
                    Poles = table.Column<int>(type: "int", nullable: false),
                    Top3 = table.Column<int>(type: "int", nullable: false),
                    Top5 = table.Column<int>(type: "int", nullable: false),
                    Top10 = table.Column<int>(type: "int", nullable: false),
                    Top15 = table.Column<int>(type: "int", nullable: false),
                    Top20 = table.Column<int>(type: "int", nullable: false),
                    Top25 = table.Column<int>(type: "int", nullable: false),
                    RacesInPoints = table.Column<int>(type: "int", nullable: false),
                    RacesCompleted = table.Column<int>(type: "int", nullable: false),
                    Incidents = table.Column<double>(type: "double", nullable: false),
                    PenaltyPoints = table.Column<double>(type: "double", nullable: false),
                    FastestLaps = table.Column<int>(type: "int", nullable: false),
                    IncidentsUnderInvestigation = table.Column<int>(type: "int", nullable: false),
                    IncidentsWithPenalty = table.Column<int>(type: "int", nullable: false),
                    LeadingLaps = table.Column<double>(type: "double", nullable: false),
                    CompletedLaps = table.Column<double>(type: "double", nullable: false),
                    CurrentSeasonPosition = table.Column<int>(type: "int", nullable: false),
                    DrivenKm = table.Column<double>(type: "double", nullable: false),
                    LeadingKm = table.Column<double>(type: "double", nullable: false),
                    AvgFinishPosition = table.Column<double>(type: "double", nullable: false),
                    AvgFinalPosition = table.Column<double>(type: "double", nullable: false),
                    AvgStartPosition = table.Column<double>(type: "double", nullable: false),
                    AvgPointsPerRace = table.Column<double>(type: "double", nullable: false),
                    AvgIncidentsPerRace = table.Column<double>(type: "double", nullable: false),
                    AvgIncidentsPerLap = table.Column<double>(type: "double", nullable: false),
                    AvgIncidentsPerKm = table.Column<double>(type: "double", nullable: false),
                    AvgPenaltyPointsPerRace = table.Column<double>(type: "double", nullable: false),
                    AvgPenaltyPointsPerLap = table.Column<double>(type: "double", nullable: false),
                    AvgPenaltyPointsPerKm = table.Column<double>(type: "double", nullable: false),
                    AvgIRating = table.Column<double>(type: "double", nullable: false),
                    AvgSRating = table.Column<double>(type: "double", nullable: false),
                    BestFinishPosition = table.Column<double>(type: "double", nullable: false),
                    WorstFinishPosition = table.Column<double>(type: "double", nullable: false),
                    FirstRaceFinishPosition = table.Column<double>(type: "double", nullable: false),
                    LastRaceFinishPosition = table.Column<double>(type: "double", nullable: false),
                    BestFinalPosition = table.Column<int>(type: "int", nullable: false),
                    WorstFinalPosition = table.Column<int>(type: "int", nullable: false),
                    FirstRaceFinalPosition = table.Column<int>(type: "int", nullable: false),
                    LastRaceFinalPosition = table.Column<int>(type: "int", nullable: false),
                    BestStartPosition = table.Column<double>(type: "double", nullable: false),
                    WorstStartPosition = table.Column<double>(type: "double", nullable: false),
                    FirstRaceStartPosition = table.Column<double>(type: "double", nullable: false),
                    LastRaceStartPosition = table.Column<double>(type: "double", nullable: false),
                    Titles = table.Column<int>(type: "int", nullable: false),
                    HardChargerAwards = table.Column<int>(type: "int", nullable: false),
                    CleanestDriverAwards = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverStatisticRows", x => new { x.LeagueId, x.StatisticSetId, x.MemberId });
                    table.ForeignKey(
                        name: "FK_DriverStatisticRows_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DriverStatisticRows_ScoredResultRows_LeagueId_FirstResultRow~",
                        columns: x => new { x.LeagueId, x.FirstResultRowId },
                        principalTable: "ScoredResultRows",
                        principalColumns: new[] { "LeagueId", "ScoredResultRowId" });
                    table.ForeignKey(
                        name: "FK_DriverStatisticRows_ScoredResultRows_LeagueId_LastResultRowId",
                        columns: x => new { x.LeagueId, x.LastResultRowId },
                        principalTable: "ScoredResultRows",
                        principalColumns: new[] { "LeagueId", "ScoredResultRowId" });
                    table.ForeignKey(
                        name: "FK_DriverStatisticRows_Sessions_LeagueId_FirstRaceId",
                        columns: x => new { x.LeagueId, x.FirstRaceId },
                        principalTable: "Sessions",
                        principalColumns: new[] { "LeagueId", "SessionId" });
                    table.ForeignKey(
                        name: "FK_DriverStatisticRows_Sessions_LeagueId_FirstSessionId",
                        columns: x => new { x.LeagueId, x.FirstSessionId },
                        principalTable: "Sessions",
                        principalColumns: new[] { "LeagueId", "SessionId" });
                    table.ForeignKey(
                        name: "FK_DriverStatisticRows_Sessions_LeagueId_LastRaceId",
                        columns: x => new { x.LeagueId, x.LastRaceId },
                        principalTable: "Sessions",
                        principalColumns: new[] { "LeagueId", "SessionId" });
                    table.ForeignKey(
                        name: "FK_DriverStatisticRows_Sessions_LeagueId_LastSessionId",
                        columns: x => new { x.LeagueId, x.LastSessionId },
                        principalTable: "Sessions",
                        principalColumns: new[] { "LeagueId", "SessionId" });
                    table.ForeignKey(
                        name: "FK_DriverStatisticRows_StatisticSets_LeagueId_StatisticSetId",
                        columns: x => new { x.LeagueId, x.StatisticSetId },
                        principalTable: "StatisticSets",
                        principalColumns: new[] { "LeagueId", "Id" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LeagueStatisticSetsStatisticSets",
                columns: table => new
                {
                    DependendStatisticSetsLeagueId = table.Column<long>(type: "bigint", nullable: false),
                    DependendStatisticSetsId = table.Column<long>(type: "bigint", nullable: false),
                    LeagueStatisticSetsLeagueId = table.Column<long>(type: "bigint", nullable: false),
                    LeagueStatisticSetsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeagueStatisticSetsStatisticSets", x => new { x.DependendStatisticSetsLeagueId, x.DependendStatisticSetsId, x.LeagueStatisticSetsLeagueId, x.LeagueStatisticSetsId });
                    table.ForeignKey(
                        name: "FK_LeagueStatisticSetsStatisticSets_StatisticSets_DependendStat~",
                        columns: x => new { x.DependendStatisticSetsLeagueId, x.DependendStatisticSetsId },
                        principalTable: "StatisticSets",
                        principalColumns: new[] { "LeagueId", "Id" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeagueStatisticSetsStatisticSets_StatisticSets_LeagueStatist~",
                        columns: x => new { x.LeagueStatisticSetsLeagueId, x.LeagueStatisticSetsId },
                        principalTable: "StatisticSets",
                        principalColumns: new[] { "LeagueId", "Id" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AddPenaltys_LeagueId_ScoredResultRowId",
                table: "AddPenaltys",
                columns: new[] { "LeagueId", "ScoredResultRowId" });

            migrationBuilder.CreateIndex(
                name: "IX_DriverStatisticRows_LeagueId_FirstRaceId",
                table: "DriverStatisticRows",
                columns: new[] { "LeagueId", "FirstRaceId" });

            migrationBuilder.CreateIndex(
                name: "IX_DriverStatisticRows_LeagueId_FirstResultRowId",
                table: "DriverStatisticRows",
                columns: new[] { "LeagueId", "FirstResultRowId" });

            migrationBuilder.CreateIndex(
                name: "IX_DriverStatisticRows_LeagueId_FirstSessionId",
                table: "DriverStatisticRows",
                columns: new[] { "LeagueId", "FirstSessionId" });

            migrationBuilder.CreateIndex(
                name: "IX_DriverStatisticRows_LeagueId_LastRaceId",
                table: "DriverStatisticRows",
                columns: new[] { "LeagueId", "LastRaceId" });

            migrationBuilder.CreateIndex(
                name: "IX_DriverStatisticRows_LeagueId_LastResultRowId",
                table: "DriverStatisticRows",
                columns: new[] { "LeagueId", "LastResultRowId" });

            migrationBuilder.CreateIndex(
                name: "IX_DriverStatisticRows_LeagueId_LastSessionId",
                table: "DriverStatisticRows",
                columns: new[] { "LeagueId", "LastSessionId" });

            migrationBuilder.CreateIndex(
                name: "IX_DriverStatisticRows_MemberId",
                table: "DriverStatisticRows",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_DriverStatisticRows_StatisticSetId",
                table: "DriverStatisticRows",
                column: "StatisticSetId");

            migrationBuilder.CreateIndex(
                name: "IX_LeagueStatisticSetsStatisticSets_LeagueStatisticSetsLeagueId~",
                table: "LeagueStatisticSetsStatisticSets",
                columns: new[] { "LeagueStatisticSetsLeagueId", "LeagueStatisticSetsId" });

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

            migrationBuilder.CreateIndex(
                name: "IX_StatisticSets_CurrentChampId",
                table: "StatisticSets",
                column: "CurrentChampId");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticSets_LeagueId_SeasonId",
                table: "StatisticSets",
                columns: new[] { "LeagueId", "SeasonId" });

            migrationBuilder.CreateIndex(
                name: "IX_StatisticSets_LeagueId_StandingId",
                table: "StatisticSets",
                columns: new[] { "LeagueId", "StandingId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AddPenaltys");

            migrationBuilder.DropTable(
                name: "CustomIncidents");

            migrationBuilder.DropTable(
                name: "DriverStatisticRows");

            migrationBuilder.DropTable(
                name: "LeagueStatisticSetsStatisticSets");

            migrationBuilder.DropTable(
                name: "ResultsFilterOptions");

            migrationBuilder.DropTable(
                name: "StatisticSets");
        }
    }
}
