using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iRLeagueDatabaseCore.Migrations
{
    public partial class AddLegacyTrackId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MapImageSrc",
                table: "TrackConfigs",
                newName: "LegacyTrackId");

            migrationBuilder.RenameColumn(
                name: "HasNigtLigthing",
                table: "TrackConfigs",
                newName: "HasNightLighting");

            migrationBuilder.AlterColumn<string>(
                name: "ConfigType",
                table: "TrackConfigs",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LegacyTrackId",
                table: "TrackConfigs",
                newName: "MapImageSrc");

            migrationBuilder.RenameColumn(
                name: "HasNightLighting",
                table: "TrackConfigs",
                newName: "HasNigtLigthing");

            migrationBuilder.AlterColumn<int>(
                name: "ConfigType",
                table: "TrackConfigs",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");
        }
    }
}
