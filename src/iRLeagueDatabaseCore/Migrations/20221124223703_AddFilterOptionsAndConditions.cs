using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace iRLeagueDatabaseCore.Migrations
{
    public partial class AddFilterOptionsAndConditions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FilterOptions",
                columns: table => new
                {
                    LeagueId = table.Column<long>(type: "bigint", nullable: false),
                    FilterOptionId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    PointFilterResultConfigId = table.Column<long>(type: "bigint", nullable: true),
                    ResultFilterResultConfigId = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_FilterOptions", x => new { x.LeagueId, x.FilterOptionId });
                    table.UniqueConstraint("AK_FilterOptions_FilterOptionId", x => x.FilterOptionId);
                    table.ForeignKey(
                        name: "FK_FilterOptions_ResultConfigurations_LeagueId_PointFilterResul~",
                        columns: x => new { x.LeagueId, x.PointFilterResultConfigId },
                        principalTable: "ResultConfigurations",
                        principalColumns: new[] { "LeagueId", "ResultConfigId" });
                    table.ForeignKey(
                        name: "FK_FilterOptions_ResultConfigurations_LeagueId_ResultFilterResu~",
                        columns: x => new { x.LeagueId, x.ResultFilterResultConfigId },
                        principalTable: "ResultConfigurations",
                        principalColumns: new[] { "LeagueId", "ResultConfigId" });
                });

            migrationBuilder.CreateTable(
                name: "FilterConditions",
                columns: table => new
                {
                    LeagueId = table.Column<long>(type: "bigint", nullable: false),
                    ConditionId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    FilterOptionId = table.Column<long>(type: "bigint", nullable: false),
                    FilterType = table.Column<string>(type: "longtext", nullable: false),
                    ColumnPropertyName = table.Column<string>(type: "longtext", nullable: true),
                    Comparator = table.Column<string>(type: "longtext", nullable: false),
                    Action = table.Column<string>(type: "longtext", nullable: false),
                    FilterValues = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilterConditions", x => new { x.LeagueId, x.ConditionId });
                    table.UniqueConstraint("AK_FilterConditions_ConditionId", x => x.ConditionId);
                    table.ForeignKey(
                        name: "FK_FilterConditions_FilterOptions_LeagueId_FilterOptionId",
                        columns: x => new { x.LeagueId, x.FilterOptionId },
                        principalTable: "FilterOptions",
                        principalColumns: new[] { "LeagueId", "FilterOptionId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FilterConditions_LeagueId_FilterOptionId",
                table: "FilterConditions",
                columns: new[] { "LeagueId", "FilterOptionId" });

            migrationBuilder.CreateIndex(
                name: "IX_FilterOptions_LeagueId_PointFilterResultConfigId",
                table: "FilterOptions",
                columns: new[] { "LeagueId", "PointFilterResultConfigId" });

            migrationBuilder.CreateIndex(
                name: "IX_FilterOptions_LeagueId_ResultFilterResultConfigId",
                table: "FilterOptions",
                columns: new[] { "LeagueId", "ResultFilterResultConfigId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FilterConditions");

            migrationBuilder.DropTable(
                name: "FilterOptions");
        }
    }
}
