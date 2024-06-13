using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StargateAPI.Migrations
{
    /// <inheritdoc />
    public partial class CreateLoggingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                  name: "Logging",
                  columns: table => new
                  {
                      Id = table.Column<int>(type: "INTEGER", nullable: false)
                          .Annotation("Sqlite:Autoincrement", true),
                      Message = table.Column<string>(type: "TEXT", nullable: false),
                      TypeOfMessage = table.Column<string>(type: "TEXT", nullable: false),
                      Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                  },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logging", x => x.Id);

                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
              name: "Logging");
        }
    }
}
