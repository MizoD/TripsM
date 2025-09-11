using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addTripsImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Trips_TripId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Trips_TripId",
                table: "Flights");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelImage_Hotels_HotelId",
                table: "HotelImage");

            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_Trips_TripId",
                table: "Hotels");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Trips_TripId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_TripCarts_Trips_TripId",
                table: "TripCarts");

            migrationBuilder.DropForeignKey(
                name: "FK_TripImage_Trips_TripId",
                table: "TripImage");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Countries_CountryId",
                table: "Trips");

            migrationBuilder.DropForeignKey(
                name: "FK_TripWishlists_Trips_TripId",
                table: "TripWishlists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Trips",
                table: "Trips");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HotelImage",
                table: "HotelImage");

            migrationBuilder.RenameTable(
                name: "Trips",
                newName: "Trip");

            migrationBuilder.RenameTable(
                name: "HotelImage",
                newName: "HotelImages");

            migrationBuilder.RenameIndex(
                name: "IX_Trips_CountryId",
                table: "Trip",
                newName: "IX_Trip_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_HotelImage_HotelId",
                table: "HotelImages",
                newName: "IX_HotelImages_HotelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Trip",
                table: "Trip",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HotelImages",
                table: "HotelImages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Trip_TripId",
                table: "Bookings",
                column: "TripId",
                principalTable: "Trip",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Trip_TripId",
                table: "Flights",
                column: "TripId",
                principalTable: "Trip",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelImages_Hotels_HotelId",
                table: "HotelImages",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Hotels_Trip_TripId",
                table: "Hotels",
                column: "TripId",
                principalTable: "Trip",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Trip_TripId",
                table: "Reviews",
                column: "TripId",
                principalTable: "Trip",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trip_Countries_CountryId",
                table: "Trip",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TripCarts_Trip_TripId",
                table: "TripCarts",
                column: "TripId",
                principalTable: "Trip",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TripImage_Trip_TripId",
                table: "TripImage",
                column: "TripId",
                principalTable: "Trip",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TripWishlists_Trip_TripId",
                table: "TripWishlists",
                column: "TripId",
                principalTable: "Trip",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Trip_TripId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Trip_TripId",
                table: "Flights");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelImages_Hotels_HotelId",
                table: "HotelImages");

            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_Trip_TripId",
                table: "Hotels");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Trip_TripId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Trip_Countries_CountryId",
                table: "Trip");

            migrationBuilder.DropForeignKey(
                name: "FK_TripCarts_Trip_TripId",
                table: "TripCarts");

            migrationBuilder.DropForeignKey(
                name: "FK_TripImage_Trip_TripId",
                table: "TripImage");

            migrationBuilder.DropForeignKey(
                name: "FK_TripWishlists_Trip_TripId",
                table: "TripWishlists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Trip",
                table: "Trip");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HotelImages",
                table: "HotelImages");

            migrationBuilder.RenameTable(
                name: "Trip",
                newName: "Trips");

            migrationBuilder.RenameTable(
                name: "HotelImages",
                newName: "HotelImage");

            migrationBuilder.RenameIndex(
                name: "IX_Trip_CountryId",
                table: "Trips",
                newName: "IX_Trips_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_HotelImages_HotelId",
                table: "HotelImage",
                newName: "IX_HotelImage_HotelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Trips",
                table: "Trips",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HotelImage",
                table: "HotelImage",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Trips_TripId",
                table: "Bookings",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Trips_TripId",
                table: "Flights",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelImage_Hotels_HotelId",
                table: "HotelImage",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Hotels_Trips_TripId",
                table: "Hotels",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Trips_TripId",
                table: "Reviews",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TripCarts_Trips_TripId",
                table: "TripCarts",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TripImage_Trips_TripId",
                table: "TripImage",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Countries_CountryId",
                table: "Trips",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TripWishlists_Trips_TripId",
                table: "TripWishlists",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
