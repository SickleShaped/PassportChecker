using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PassportChecker.Migrations
{
    /// <inheritdoc />
    public partial class IndexPassports : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Passports_Series_Number",
                table: "Passports",
                columns: new[] { "Series", "Number" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
