using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPITitiksha.API.Migrations
{
    /// <inheritdoc />
    public partial class InitinalMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Length",
                table: "WalkDifficulties");

            migrationBuilder.DropColumn(
                name: "RegionId",
                table: "WalkDifficulties");

            migrationBuilder.DropColumn(
                name: "WalkDifficultyId",
                table: "WalkDifficulties");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "WalkDifficulties",
                newName: "Code");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Code",
                table: "WalkDifficulties",
                newName: "Name");

            migrationBuilder.AddColumn<double>(
                name: "Length",
                table: "WalkDifficulties",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<Guid>(
                name: "RegionId",
                table: "WalkDifficulties",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "WalkDifficultyId",
                table: "WalkDifficulties",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
