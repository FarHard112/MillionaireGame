using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WhoWantsToBeAMillionaireGame.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class Color : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PrizeColors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrizeColors", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrizeColors_Text",
                table: "PrizeColors",
                column: "Text",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrizeColors");
        }
    }
}
