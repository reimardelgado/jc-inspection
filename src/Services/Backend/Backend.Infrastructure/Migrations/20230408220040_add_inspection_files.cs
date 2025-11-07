using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Infrastructure.Migrations
{
    public partial class add_inspection_files : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Items_ItemId",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_ItemId",
                table: "Photos");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "Photos",
                newName: "SectionId");

            migrationBuilder.AlterColumn<string>(
                name: "KeyS3",
                table: "Photos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "Photos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "InspectionId",
                table: "Photos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Photos_InspectionId",
                table: "Photos",
                column: "InspectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Inspections_InspectionId",
                table: "Photos",
                column: "InspectionId",
                principalTable: "Inspections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Inspections_InspectionId",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_InspectionId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "AccessType",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "BucketPath",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "Bucketname",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "InspectionId",
                table: "Photos");

            migrationBuilder.RenameColumn(
                name: "SectionId",
                table: "Photos",
                newName: "ItemId");

            migrationBuilder.AlterColumn<string>(
                name: "KeyS3",
                table: "Photos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Photos_ItemId",
                table: "Photos",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Items_ItemId",
                table: "Photos",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
