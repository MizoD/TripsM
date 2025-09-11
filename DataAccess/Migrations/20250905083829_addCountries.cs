using Microsoft.EntityFrameworkCore.Migrations;
using System.Configuration;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addCountries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TripImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TripId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TripImage_Trips_TripId",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TripImage_TripId",
                table: "TripImage",
                column: "TripId");

            migrationBuilder.InsertData(
            table: "Countries",
            columns: new[] { "Name", "Code", "Currency" },
            values: new object[,]
            {
                { "United States", "US", "USD" },
                { "United Kingdom", "GB", "GBP" },
                { "Canada", "CA", "CAD" },
                { "Germany", "DE", "EUR" },
                { "France", "FR", "EUR" },
                { "Spain", "ES", "EUR" },
                { "Italy", "IT", "EUR" },
                { "Australia", "AU", "AUD" },
                { "Japan", "JP", "JPY" },
                { "China", "CN", "CNY" },
                { "India", "IN", "INR" },
                { "Brazil", "BR", "BRL" },
                { "Mexico", "MX", "MXN" },
                { "Russia", "RU", "RUB" },
                { "South Africa", "ZA", "ZAR" },
                { "Egypt", "EG", "EGP" },
                { "Saudi Arabia", "SA", "SAR" },
                { "Turkey", "TR", "TRY" },
                { "United Arab Emirates", "AE", "AED" },
                { "Qatar", "QA", "QAR" },
                { "Greece", "GR", "EUR" },
                { "Sweden", "SE", "SEK" },
                { "Norway", "NO", "NOK" },
                { "Denmark", "DK", "DKK" },
                { "Netherlands", "NL", "EUR" },
                { "Belgium", "BE", "EUR" },
                { "Switzerland", "CH", "CHF" },
                { "Austria", "AT", "EUR" },
                { "Portugal", "PT", "EUR" },
                { "Thailand", "TH", "THB" },
                { "Indonesia", "ID", "IDR" },
                { "Malaysia", "MY", "MYR" },
                { "Singapore", "SG", "SGD" },
                { "South Korea", "KR", "KRW" },
                { "Morocco", "MA", "MAD" }
            });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TripImage");

            migrationBuilder.Sql("DELETE FROM Countries");
        }
    }
}
