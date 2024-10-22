﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PassportChecker.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Changes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Series = table.Column<int>(type: "integer", nullable: false),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Changes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Passports",
                columns: table => new
                {
                    Series = table.Column<int>(type: "integer", nullable: false),
                    Number = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passports", x => new { x.Series, x.Number });
                });

            migrationBuilder.CreateIndex(
                name: "IX_Changes_Date",
                table: "Changes",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_Changes_Series_Number",
                table: "Changes",
                columns: new[] { "Series", "Number" });

            migrationBuilder.CreateIndex(
                name: "IX_Passports_Series_Number",
                table: "Passports",
                columns: new[] { "Series", "Number" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Changes");

            migrationBuilder.DropTable(
                name: "Passports");
        }
    
    }
}
