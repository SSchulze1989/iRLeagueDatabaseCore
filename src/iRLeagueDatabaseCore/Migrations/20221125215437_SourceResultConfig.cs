using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iRLeagueDatabaseCore.Migrations
{
    public partial class SourceResultConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "SourceResultConfigId",
                table: "ResultConfigurations",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResultConfigurations_LeagueId_SourceResultConfigId",
                table: "ResultConfigurations",
                columns: new[] { "LeagueId", "SourceResultConfigId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ResultConfigurations_ResultConfigurations_LeagueId_SourceRes~",
                table: "ResultConfigurations",
                columns: new[] { "LeagueId", "SourceResultConfigId" },
                principalTable: "ResultConfigurations",
                principalColumns: new[] { "LeagueId", "ResultConfigId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResultConfigurations_ResultConfigurations_LeagueId_SourceRes~",
                table: "ResultConfigurations");

            migrationBuilder.DropIndex(
                name: "IX_ResultConfigurations_LeagueId_SourceResultConfigId",
                table: "ResultConfigurations");

            migrationBuilder.DropColumn(
                name: "SourceResultConfigId",
                table: "ResultConfigurations");
        }
    }
}
