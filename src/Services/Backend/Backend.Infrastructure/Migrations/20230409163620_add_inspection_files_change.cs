using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Infrastructure.Migrations
{
    public partial class add_inspection_files_change : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessType",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "BucketPath",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "Bucketname",
                table: "Photos");

            migrationBuilder.RenameColumn(
                name: "UrlS3",
                table: "Photos",
                newName: "FilePath");

            migrationBuilder.RenameColumn(
                name: "KeyS3",
                table: "Photos",
                newName: "Url");

            migrationBuilder.AlterColumn<string>(
                name: "ContentType",
                table: "Photos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Url",
                table: "Photos",
                newName: "KeyS3");

            migrationBuilder.RenameColumn(
                name: "FilePath",
                table: "Photos",
                newName: "UrlS3");

            migrationBuilder.AlterColumn<string>(
                name: "ContentType",
                table: "Photos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccessType",
                table: "Photos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BucketPath",
                table: "Photos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Bucketname",
                table: "Photos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
