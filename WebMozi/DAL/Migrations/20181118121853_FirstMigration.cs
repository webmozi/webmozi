using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "ReservationId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "ReservationId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "ReservationId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.AddColumn<string>(
                name: "Img",
                table: "Movies",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "MovieId",
                keyValue: 1,
                column: "Img",
                value: "venom.jpg");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "MovieId",
                keyValue: 2,
                column: "Img",
                value: "venom.jpg");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "MovieId",
                keyValue: 3,
                column: "Img",
                value: "venom.jpg");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Img",
                table: "Movies");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "Name", "TelephoneNumber" },
                values: new object[] { 1, "laci@gmail.com", "Laci", "06-70-707-0707" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "Name", "TelephoneNumber" },
                values: new object[] { 2, "peti@icloud.com", "Peti", "06-70- 606-0606" });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "ReservationId", "MovieEventId", "SeatId", "UserId" },
                values: new object[] { 3, 3, 1, 1 });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "ReservationId", "MovieEventId", "SeatId", "UserId" },
                values: new object[] { 1, 1, 4, 2 });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "ReservationId", "MovieEventId", "SeatId", "UserId" },
                values: new object[] { 2, 1, 5, 2 });
        }
    }
}
