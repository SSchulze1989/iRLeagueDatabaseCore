using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace iRLeagueDatabaseCore.Migrations
{
    public partial class StandingConfigurations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "StandingConfigurationEntityLeagueId",
                table: "Standings",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "StandingConfigurationEntityStandingConfigId",
                table: "Standings",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsScored",
                table: "StandingRows_ScoredResultRows",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "StandingConfigurationEntity",
                columns: table => new
                {
                    LeagueId = table.Column<long>(type: "bigint", nullable: false),
                    StandingConfigId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true),
                    ResultKind = table.Column<int>(type: "int", nullable: false),
                    UseCombinedResult = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    WeeksCounted = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_StandingConfigurationEntity", x => new { x.LeagueId, x.StandingConfigId });
                    table.UniqueConstraint("AK_StandingConfigurationEntity_StandingConfigId", x => x.StandingConfigId);
                });

            migrationBuilder.CreateTable(
                name: "StandingConfigs_ResultConfigs",
                columns: table => new
                {
                    LeagueId = table.Column<long>(type: "bigint", nullable: false),
                    StandingConfigId = table.Column<long>(type: "bigint", nullable: false),
                    ResultConfigId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StandingConfigs_ResultConfigs", x => new { x.ResultConfigId, x.LeagueId, x.StandingConfigId });
                    table.ForeignKey(
                        name: "FK_StandingConfigs_ResultConfigs_ResultConfigurations_LeagueId_~",
                        columns: x => new { x.LeagueId, x.ResultConfigId },
                        principalTable: "ResultConfigurations",
                        principalColumns: new[] { "LeagueId", "ResultConfigId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StandingConfigs_ResultConfigs_StandingConfigurationEntity_Le~",
                        columns: x => new { x.LeagueId, x.StandingConfigId },
                        principalTable: "StandingConfigurationEntity",
                        principalColumns: new[] { "LeagueId", "StandingConfigId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Standings_StandingConfigurationEntityLeagueId_StandingConfig~",
                table: "Standings",
                columns: new[] { "StandingConfigurationEntityLeagueId", "StandingConfigurationEntityStandingConfigId" });

            migrationBuilder.CreateIndex(
                name: "IX_StandingConfigs_ResultConfigs_LeagueId_ResultConfigId",
                table: "StandingConfigs_ResultConfigs",
                columns: new[] { "LeagueId", "ResultConfigId" });

            migrationBuilder.CreateIndex(
                name: "IX_StandingConfigs_ResultConfigs_LeagueId_StandingConfigId",
                table: "StandingConfigs_ResultConfigs",
                columns: new[] { "LeagueId", "StandingConfigId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Standings_StandingConfigurationEntity_StandingConfigurationE~",
                table: "Standings",
                columns: new[] { "StandingConfigurationEntityLeagueId", "StandingConfigurationEntityStandingConfigId" },
                principalTable: "StandingConfigurationEntity",
                principalColumns: new[] { "LeagueId", "StandingConfigId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Standings_StandingConfigurationEntity_StandingConfigurationE~",
                table: "Standings");

            migrationBuilder.DropTable(
                name: "StandingConfigs_ResultConfigs");

            migrationBuilder.DropTable(
                name: "StandingConfigurationEntity");

            migrationBuilder.DropIndex(
                name: "IX_Standings_StandingConfigurationEntityLeagueId_StandingConfig~",
                table: "Standings");

            migrationBuilder.DropColumn(
                name: "StandingConfigurationEntityLeagueId",
                table: "Standings");

            migrationBuilder.DropColumn(
                name: "StandingConfigurationEntityStandingConfigId",
                table: "Standings");

            migrationBuilder.DropColumn(
                name: "IsScored",
                table: "StandingRows_ScoredResultRows");
        }
    }
}
