using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iRLeagueDatabaseCore.Migrations
{
    public partial class StandingConfigForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Standings_StandingConfigurationEntity_StandingConfigurationE~",
                table: "Standings");

            migrationBuilder.DropIndex(
                name: "`IX_Standings_StandingConfigurationEntityLeagueId_StandingConfig~`",
                table: "Standings");

            migrationBuilder.DropColumn(
                name: "StandingConfigurationEntityLeagueId",
                table: "Standings");

            migrationBuilder.DropColumn(
                name: "StandingConfigurationEntityStandingConfigId",
                table: "Standings");

            migrationBuilder.CreateIndex(
                name: "IX_Standings_LeagueId_StandingConfigId",
                table: "Standings",
                columns: new[] { "LeagueId", "StandingConfigId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Standings_StandingConfigurationEntity_LeagueId_StandingConfi~",
                table: "Standings",
                columns: new[] { "LeagueId", "StandingConfigId" },
                principalTable: "StandingConfigurationEntity",
                principalColumns: new[] { "LeagueId", "StandingConfigId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Standings_StandingConfigurationEntity_LeagueId_StandingConfi~",
                table: "Standings");

            migrationBuilder.DropIndex(
                name: "IX_Standings_LeagueId_StandingConfigId",
                table: "Standings");

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

            migrationBuilder.CreateIndex(
                name: "IX_Standings_StandingConfigurationEntityLeagueId_StandingConfig~",
                table: "Standings",
                columns: new[] { "StandingConfigurationEntityLeagueId", "StandingConfigurationEntityStandingConfigId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Standings_StandingConfigurationEntity_StandingConfigurationE~",
                table: "Standings",
                columns: new[] { "StandingConfigurationEntityLeagueId", "StandingConfigurationEntityStandingConfigId" },
                principalTable: "StandingConfigurationEntity",
                principalColumns: new[] { "LeagueId", "StandingConfigId" });
        }
    }
}
