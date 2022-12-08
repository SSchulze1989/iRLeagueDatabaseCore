using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iRLeagueDatabaseCore.Migrations
{
    public partial class ReviewPenaltyCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReviewPenaltys_IncidentReviews_LeagueId_ReviewId",
                table: "ReviewPenaltys");

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewPenaltys_IncidentReviews_LeagueId_ReviewId",
                table: "ReviewPenaltys",
                columns: new[] { "LeagueId", "ReviewId" },
                principalTable: "IncidentReviews",
                principalColumns: new[] { "LeagueId", "ReviewId" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReviewPenaltys_IncidentReviews_LeagueId_ReviewId",
                table: "ReviewPenaltys");

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewPenaltys_IncidentReviews_LeagueId_ReviewId",
                table: "ReviewPenaltys",
                columns: new[] { "LeagueId", "ReviewId" },
                principalTable: "IncidentReviews",
                principalColumns: new[] { "LeagueId", "ReviewId" });
        }
    }
}
