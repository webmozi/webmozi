using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Length",
                table: "Movies",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "MovieId",
                keyValue: 1,
                column: "Length",
                value: 120);

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "MovieId",
                keyValue: 2,
                column: "Length",
                value: 95);

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "MovieId",
                keyValue: 3,
                column: "Length",
                value: 110);

            migrationBuilder.UpdateData(
                table: "Seats",
                keyColumn: "SeatId",
                keyValue: 2,
                columns: new[] { "RowNumber", "SeatNumber" },
                values: new object[] { 1, 2 });

            migrationBuilder.UpdateData(
                table: "Seats",
                keyColumn: "SeatId",
                keyValue: 3,
                columns: new[] { "RowNumber", "SeatNumber" },
                values: new object[] { 1, 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Length",
                table: "Movies");

            migrationBuilder.UpdateData(
                table: "Seats",
                keyColumn: "SeatId",
                keyValue: 2,
                columns: new[] { "RowNumber", "SeatNumber" },
                values: new object[] { 2, 1 });

            migrationBuilder.UpdateData(
                table: "Seats",
                keyColumn: "SeatId",
                keyValue: 3,
                columns: new[] { "RowNumber", "SeatNumber" },
                values: new object[] { 3, 1 });
        }
    }
}
