using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WhoWantsToBeAMillionaireGame.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class ColorValueProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ColorValue",
                table: "PrizeColors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColorValue",
                table: "PrizeColors");
        }
    }
}
