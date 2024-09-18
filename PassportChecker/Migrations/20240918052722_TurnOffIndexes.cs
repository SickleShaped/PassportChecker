using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PassportChecker.Migrations
{
    /// <inheritdoc />
    public partial class TurnOffIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Passports_Series_Number",
                table: "Passports");

            migrationBuilder.DropIndex(
                name: "IX_Changes_Date",
                table: "Changes");

            migrationBuilder.DropIndex(
                name: "IX_Changes_Series_Number",
                table: "Changes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Passports_Series_Number",
                table: "Passports",
                columns: new[] { "Series", "Number" });

            migrationBuilder.CreateIndex(
                name: "IX_Changes_Date",
                table: "Changes",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_Changes_Series_Number",
                table: "Changes",
                columns: new[] { "Series", "Number" });
        }
    }
}
