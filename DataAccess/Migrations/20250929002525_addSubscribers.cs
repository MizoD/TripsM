using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addSubscribers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Passengers_Bookings_BookingId",
                table: "Passengers");

            migrationBuilder.DropForeignKey(
                name: "FK_Passengers_Seats_SeatId",
                table: "Passengers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Passengers",
                table: "Passengers");

            migrationBuilder.RenameTable(
                name: "Passengers",
                newName: "Passenger");

            migrationBuilder.RenameIndex(
                name: "IX_Passengers_SeatId",
                table: "Passenger",
                newName: "IX_Passenger_SeatId");

            migrationBuilder.RenameIndex(
                name: "IX_Passengers_BookingId",
                table: "Passenger",
                newName: "IX_Passenger_BookingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Passenger",
                table: "Passenger",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "NewsletterSubscribers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubscribedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsletterSubscribers", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Passenger_Bookings_BookingId",
                table: "Passenger",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Passenger_Seats_SeatId",
                table: "Passenger",
                column: "SeatId",
                principalTable: "Seats",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Passenger_Bookings_BookingId",
                table: "Passenger");

            migrationBuilder.DropForeignKey(
                name: "FK_Passenger_Seats_SeatId",
                table: "Passenger");

            migrationBuilder.DropTable(
                name: "NewsletterSubscribers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Passenger",
                table: "Passenger");

            migrationBuilder.RenameTable(
                name: "Passenger",
                newName: "Passengers");

            migrationBuilder.RenameIndex(
                name: "IX_Passenger_SeatId",
                table: "Passengers",
                newName: "IX_Passengers_SeatId");

            migrationBuilder.RenameIndex(
                name: "IX_Passenger_BookingId",
                table: "Passengers",
                newName: "IX_Passengers_BookingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Passengers",
                table: "Passengers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Passengers_Bookings_BookingId",
                table: "Passengers",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Passengers_Seats_SeatId",
                table: "Passengers",
                column: "SeatId",
                principalTable: "Seats",
                principalColumn: "Id");
        }
    }
}
