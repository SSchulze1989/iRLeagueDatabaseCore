using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iRLeagueDatabaseCore.Migrations
{
    public partial class ResultKindConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScoringKind",
                table: "Scorings");

            migrationBuilder.AddColumn<string>(
                name: "ResultKind",
                table: "ResultConfigurations",
                type: "longtext",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResultKind",
                table: "ResultConfigurations");

            migrationBuilder.AddColumn<string>(
                name: "ScoringKind",
                table: "Scorings",
                type: "longtext",
                nullable: false);
        }
    }
}
