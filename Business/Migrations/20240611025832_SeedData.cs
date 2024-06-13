using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StargateAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CareerStartDate",
                table: "AstronautDetail",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Person",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Jill Carter" },
                    { 2, "John O'Neil" },
                    { 3, "Daniel Hurst"},
                    { 4, "Teal Clark" }
                });

            //jill
            migrationBuilder.InsertData(
                table: "AstronautDetail",
                columns: new[] { "Id", "CareerEndDate", "CareerStartDate", "CurrentDutyTitle", "CurrentRank", "PersonId" },
                values: new object[] { 1, null, new DateTime(2024, 6, 10, 21, 58, 32, 526, DateTimeKind.Local).AddTicks(796000), "Captain", "Capt", 1 });

            migrationBuilder.InsertData(
                table: "AstronautDuty",
                columns: new[] { "Id", "DutyEndDate", "DutyStartDate", "DutyTitle", "PersonId", "Rank" },
                values: new object[] { 1, null, new DateTime(2024, 6, 10, 21, 58, 32, 526, DateTimeKind.Local).AddTicks(801900), "Captain", 1, "Capt" });

            //john
            migrationBuilder.InsertData(
                 table: "AstronautDetail",
                columns: new[] { "Id", "CareerEndDate", "CareerStartDate", "CurrentDutyTitle", "CurrentRank", "PersonId" },
                values: new object[] { 2, null, new DateTime(2024, 6, 10, 21, 58, 32, 526, DateTimeKind.Local).AddTicks(79600), "Lieutenant Colonel", "Lt Col", 2 });

            migrationBuilder.InsertData(
                table: "AstronautDuty",
                columns: new[] { "Id", "DutyEndDate", "DutyStartDate", "DutyTitle", "PersonId", "Rank" },
                values: new object[] { 2, null, new DateTime(2024, 6, 10, 21, 58, 32, 526, DateTimeKind.Local).AddTicks(801900), "Lieutenant Colonel", 2, "Lt Col" });
           
            //daniel
            migrationBuilder.InsertData(
                table: "AstronautDetail",
                columns: new[] { "Id", "CareerEndDate", "CareerStartDate", "CurrentDutyTitle", "CurrentRank", "PersonId" },
                values: new object[] { 3, null, new DateTime(2024, 6, 10, 21, 58, 32, 526, DateTimeKind.Local).AddTicks(796000), "Master Sergeant", "MSgt", 3 });

            migrationBuilder.InsertData(
                table: "AstronautDuty",
                columns: new[] { "Id", "DutyEndDate", "DutyStartDate", "DutyTitle", "PersonId", "Rank" },
                values: new object[] { 3, null, new DateTime(2024, 6, 10, 21, 58, 32, 526, DateTimeKind.Local).AddTicks(801900), "Master Sergeant", 3, "MSgt" });
            
            //teal
            migrationBuilder.InsertData(
                table: "AstronautDetail",
                columns: new[] { "Id", "CareerEndDate", "CareerStartDate", "CurrentDutyTitle", "CurrentRank", "PersonId" },
                values: new object[] { 4, null, new DateTime(2024, 6, 10, 21, 58, 32, 526, DateTimeKind.Local).AddTicks(79600), "Master Sergeant", "MSgt", 4 });

            migrationBuilder.InsertData(
                table: "AstronautDuty",
                columns: new[] { "Id", "DutyEndDate", "DutyStartDate", "DutyTitle", "PersonId", "Rank" },
                values: new object[] { 4, null, new DateTime(2024, 6, 10, 21, 58, 32, 526, DateTimeKind.Local).AddTicks(80190), "Master Sergeant", 4, "MSgt" });



        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AstronautDetail",
                keyColumn: "Id",
                keyValue: 1);
            migrationBuilder.DeleteData(
                table: "AstronautDetail",
                keyColumn: "Id",
                keyValue: 2);
            migrationBuilder.DeleteData(
                table: "AstronautDetail",
                keyColumn: "Id",
                keyValue: 3);
            migrationBuilder.DeleteData(
                table: "AstronautDetail",
                keyColumn: "Id",
                keyValue: 4);


            migrationBuilder.DeleteData(
                table: "AstronautDuty",
                keyColumn: "Id",
                keyValue: 1);
            migrationBuilder.DeleteData(
                table: "AstronautDuty",
                keyColumn: "Id",
                keyValue: 2);
            migrationBuilder.DeleteData(
                table: "AstronautDuty",
                keyColumn: "Id",
                keyValue: 3);
            migrationBuilder.DeleteData(
                table: "AstronautDuty",
                keyColumn: "Id",
                keyValue: 4);


            migrationBuilder.DeleteData(
                table: "Person",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Person",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Person",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Person",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CareerStartDate",
                table: "AstronautDetail",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT");
        }
    }
}
