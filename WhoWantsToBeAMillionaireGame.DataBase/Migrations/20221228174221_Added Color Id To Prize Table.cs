using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WhoWantsToBeAMillionaireGame.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class AddedColorIdToPrizeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ColorId",
                table: "Prize",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColorId",
                table: "Prize");
        }
    }
}
