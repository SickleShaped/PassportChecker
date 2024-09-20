using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PassportChecker.Migrations
{
    /// <inheritdoc />
    public partial class IndexDates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Changes_Date",
                table: "Changes",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_Changes_Series_Number",
                table: "Changes",
                columns: new[] { "Series", "Number" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
