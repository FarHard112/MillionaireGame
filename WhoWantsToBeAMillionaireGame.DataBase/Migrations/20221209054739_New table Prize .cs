using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WhoWantsToBeAMillionaireGame.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class NewtablePrize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Prize",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    isEnable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prize", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prize_Text",
                table: "Prize",
                column: "Text",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prize");
        }
    }
}
