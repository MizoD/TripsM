using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addFlights : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
        table: "Flights",
        columns: new[] { "Title", "Status", "DepartureTime", "ArrivalTime", "Traffic", "DepartureAirportId", "ArrivalAirportId", "AirCraftId" },
        values: new object[,]
        {
            { "FL001", 0, new DateTime(2025, 10, 01, 08, 00, 00), new DateTime(2025, 10, 01, 12, 00, 00), 120, 1, 2, 1 },
            { "FL002", 0, new DateTime(2025, 10, 02, 09, 15, 00), new DateTime(2025, 10, 02, 13, 45, 00), 150, 2, 3, 2 },
            { "FL003", 0, new DateTime(2025, 10, 03, 10, 30, 00), new DateTime(2025, 10, 03, 14, 50, 00), 180, 3, 4, 3 },
            { "FL004", 0, new DateTime(2025, 10, 04, 11, 45, 00), new DateTime(2025, 10, 04, 16, 00, 00), 90, 4, 5, 4 },
            { "FL005", 0, new DateTime(2025, 10, 05, 07, 00, 00), new DateTime(2025, 10, 05, 10, 30, 00), 200, 5, 6, 5 },
            { "FL006", 0, new DateTime(2025, 10, 06, 14, 20, 00), new DateTime(2025, 10, 06, 18, 10, 00), 110, 6, 7, 6 },
            { "FL007", 0, new DateTime(2025, 10, 07, 15, 10, 00), new DateTime(2025, 10, 07, 20, 00, 00), 130, 7, 8, 7 },
            { "FL008", 0, new DateTime(2025, 10, 08, 06, 45, 00), new DateTime(2025, 10, 08, 09, 15, 00), 140, 8, 9, 8 },
            { "FL009", 0, new DateTime(2025, 10, 09, 13, 30, 00), new DateTime(2025, 10, 09, 17, 40, 00), 95, 9, 10, 9 },
            { "FL010", 0, new DateTime(2025, 10, 10, 16, 15, 00), new DateTime(2025, 10, 10, 21, 00, 00), 100, 10, 11, 10 },
            { "FL011", 0, new DateTime(2025, 10, 11, 12, 00, 00), new DateTime(2025, 10, 11, 15, 45, 00), 80, 11, 12, 11 },
            { "FL012", 0, new DateTime(2025, 10, 12, 18, 10, 00), new DateTime(2025, 10, 12, 22, 15, 00), 160, 12, 13, 12 },
            { "FL013", 0, new DateTime(2025, 10, 13, 05, 30, 00), new DateTime(2025, 10, 13, 09, 40, 00), 175, 13, 14, 13 },
            { "FL014", 0, new DateTime(2025, 10, 14, 07, 45, 00), new DateTime(2025, 10, 14, 11, 15, 00), 200, 14, 15, 14 },
            { "FL015", 0, new DateTime(2025, 10, 15, 09, 10, 00), new DateTime(2025, 10, 15, 13, 50, 00), 140, 15, 16, 15 },
            { "FL016", 0, new DateTime(2025, 10, 16, 10, 40, 00), new DateTime(2025, 10, 16, 15, 20, 00), 115, 16, 17, 16 },
            { "FL017", 0, new DateTime(2025, 10, 17, 14, 00, 00), new DateTime(2025, 10, 17, 18, 30, 00), 170, 17, 18, 17 },
            { "FL018", 0, new DateTime(2025, 10, 18, 08, 20, 00), new DateTime(2025, 10, 18, 12, 10, 00), 155, 18, 19, 18 },
            { "FL019", 0, new DateTime(2025, 10, 19, 06, 50, 00), new DateTime(2025, 10, 19, 10, 25, 00), 135, 19, 20, 19 },
            { "FL020", 0, new DateTime(2025, 10, 20, 11, 15, 00), new DateTime(2025, 10, 20, 14, 45, 00), 125, 20, 21, 20 },
            { "FL021", 0, new DateTime(2025, 10, 21, 07, 30, 00), new DateTime(2025, 10, 21, 11, 15, 00), 185, 21, 22, 21 },
            { "FL022", 0, new DateTime(2025, 10, 22, 13, 00, 00), new DateTime(2025, 10, 22, 16, 50, 00), 105, 22, 23, 22 },
            { "FL023", 0, new DateTime(2025, 10, 23, 15, 20, 00), new DateTime(2025, 10, 23, 20, 00, 00), 125, 23, 24, 23 },
            { "FL024", 0, new DateTime(2025, 10, 24, 09, 40, 00), new DateTime(2025, 10, 24, 13, 15, 00), 160, 24, 25, 24 },
            { "FL025", 0, new DateTime(2025, 10, 25, 11, 00, 00), new DateTime(2025, 10, 25, 15, 20, 00), 200, 25, 26, 25 },
            { "FL026", 0, new DateTime(2025, 10, 26, 08, 10, 00), new DateTime(2025, 10, 26, 12, 30, 00), 150, 26, 27, 26 },
            { "FL027", 0, new DateTime(2025, 10, 27, 14, 30, 00), new DateTime(2025, 10, 27, 18, 15, 00), 175, 27, 28, 27 },
            { "FL028", 0, new DateTime(2025, 10, 28, 06, 15, 00), new DateTime(2025, 10, 28, 09, 40, 00), 140, 28, 29, 28 },
            { "FL029", 0, new DateTime(2025, 10, 29, 12, 40, 00), new DateTime(2025, 10, 29, 16, 10, 00), 90, 29, 30, 29 },
            { "FL030", 0, new DateTime(2025, 10, 30, 17, 10, 00), new DateTime(2025, 10, 30, 21, 45, 00), 180, 30, 31, 30 },
            { "FL031", 0, new DateTime(2025, 11, 01, 09, 00, 00), new DateTime(2025, 11, 01, 13, 20, 00), 145, 31, 32, 31 },
            { "FL032", 0, new DateTime(2025, 11, 02, 07, 20, 00), new DateTime(2025, 11, 02, 11, 15, 00), 100, 32, 33, 32 },
            { "FL033", 0, new DateTime(2025, 11, 03, 13, 50, 00), new DateTime(2025, 11, 03, 18, 30, 00), 135, 33, 34, 33 },
            { "FL034", 0, new DateTime(2025, 11, 04, 16, 40, 00), new DateTime(2025, 11, 04, 21, 10, 00), 175, 34, 35, 34 },
            { "FL035", 0, new DateTime(2025, 11, 05, 08, 15, 00), new DateTime(2025, 11, 05, 12, 50, 00), 195, 35, 36, 35 },
            { "FL036", 0, new DateTime(2025, 11, 06, 10, 00, 00), new DateTime(2025, 11, 06, 14, 20, 00), 155, 36, 37, 36 },
            { "FL037", 0, new DateTime(2025, 11, 07, 06, 30, 00), new DateTime(2025, 11, 07, 10, 15, 00), 110, 37, 38, 37 },
            { "FL038", 0, new DateTime(2025, 11, 08, 15, 10, 00), new DateTime(2025, 11, 08, 19, 30, 00), 165, 38, 39, 38 },
            { "FL039", 0, new DateTime(2025, 11, 09, 11, 45, 00), new DateTime(2025, 11, 09, 15, 45, 00), 125, 39, 40, 39 },
            { "FL040", 0, new DateTime(2025, 11, 10, 07, 25, 00), new DateTime(2025, 11, 10, 11, 10, 00), 135, 40, 41, 40 },
            { "FL041", 0, new DateTime(2025, 11, 11, 13, 20, 00), new DateTime(2025, 11, 11, 17, 15, 00), 150, 41, 42, 41 },
            { "FL042", 0, new DateTime(2025, 11, 12, 14, 10, 00), new DateTime(2025, 11, 12, 18, 30, 00), 190, 42, 43, 42 },
            { "FL043", 0, new DateTime(2025, 11, 13, 09, 40, 00), new DateTime(2025, 11, 13, 13, 30, 00), 175, 43, 44, 43 },
            { "FL044", 0, new DateTime(2025, 11, 14, 07, 30, 00), new DateTime(2025, 11, 14, 11, 50, 00), 115, 44, 45, 44 },
            { "FL045", 0, new DateTime(2025, 11, 15, 10, 50, 00), new DateTime(2025, 11, 15, 15, 10, 00), 145, 45, 46, 45 },
            { "FL046", 0, new DateTime(2025, 11, 16, 06, 45, 00), new DateTime(2025, 11, 16, 10, 30, 00), 100, 46, 47, 46 },
            { "FL047", 0, new DateTime(2025, 11, 17, 12, 15, 00), new DateTime(2025, 11, 17, 16, 40, 00), 180, 47, 48, 47 },
            { "FL048", 0, new DateTime(2025, 11, 18, 14, 20, 00), new DateTime(2025, 11, 18, 18, 45, 00), 165, 48, 49, 48 },
            { "FL049", 0, new DateTime(2025, 11, 19, 15, 40, 00), new DateTime(2025, 11, 19, 20, 10, 00), 135, 49, 50, 49 },
            { "FL050", 0, new DateTime(2025, 11, 20, 08, 30, 00), new DateTime(2025, 11, 20, 12, 20, 00), 155, 50, 51, 50 },
        });

            migrationBuilder.InsertData(
    table: "Flights",
    columns: new[] { "Title", "Status", "DepartureTime", "ArrivalTime", "Traffic", "DepartureAirportId", "ArrivalAirportId", "AirCraftId" },
    values: new object[,]
    {
        { "FL051", 0, new DateTime(2025, 10, 1, 10, 0, 0), new DateTime(2025, 10, 1, 14, 0, 0), 130, 21, 55, 51 },
        { "FL052", 0, new DateTime(2025, 10, 2, 11, 0, 0), new DateTime(2025, 10, 2, 15, 0, 0), 140, 22, 56, 52 },
        { "FL053", 0, new DateTime(2025, 10, 3, 12, 0, 0), new DateTime(2025, 10, 3, 16, 0, 0), 150, 23, 57, 53 },
        { "FL054", 0, new DateTime(2025, 10, 4, 13, 0, 0), new DateTime(2025, 10, 4, 17, 0, 0), 160, 24, 58, 54 },
        { "FL055", 0, new DateTime(2025, 10, 5, 14, 0, 0), new DateTime(2025, 10, 5, 18, 0, 0), 170, 25, 59, 55 },
        { "FL056", 0, new DateTime(2025, 10, 6, 15, 0, 0), new DateTime(2025, 10, 6, 19, 0, 0), 180, 26, 60, 56 },
        { "FL057", 0, new DateTime(2025, 10, 7, 16, 0, 0), new DateTime(2025, 10, 7, 20, 0, 0), 190, 27, 61, 57 },
        { "FL058", 0, new DateTime(2025, 10, 8, 17, 0, 0), new DateTime(2025, 10, 8, 21, 0, 0), 200, 28, 62, 58 },
        { "FL059", 0, new DateTime(2025, 10, 9, 18, 0, 0), new DateTime(2025, 10, 9, 22, 0, 0), 210, 29, 63, 59 },
        { "FL060", 0, new DateTime(2025, 10, 10, 19, 0, 0), new DateTime(2025, 10, 10, 23, 0, 0), 220, 30, 64, 60 },

        { "FL061", 0, new DateTime(2025, 10, 11, 10, 0, 0), new DateTime(2025, 10, 11, 14, 0, 0), 230, 31, 65, 61 },
        { "FL062", 0, new DateTime(2025, 10, 12, 11, 0, 0), new DateTime(2025, 10, 12, 15, 0, 0), 240, 32, 66, 62 },
        { "FL063", 0, new DateTime(2025, 10, 13, 12, 0, 0), new DateTime(2025, 10, 13, 16, 0, 0), 250, 33, 67, 63 },
        { "FL064", 0, new DateTime(2025, 10, 14, 13, 0, 0), new DateTime(2025, 10, 14, 17, 0, 0), 260, 34, 68, 64 },
        { "FL065", 0, new DateTime(2025, 10, 15, 14, 0, 0), new DateTime(2025, 10, 15, 18, 0, 0), 270, 35, 69, 65 },
        { "FL066", 0, new DateTime(2025, 10, 16, 15, 0, 0), new DateTime(2025, 10, 16, 19, 0, 0), 280, 36, 70, 66 },
        { "FL067", 0, new DateTime(2025, 10, 17, 16, 0, 0), new DateTime(2025, 10, 17, 20, 0, 0), 290, 37, 1, 67 },
        { "FL068", 0, new DateTime(2025, 10, 18, 17, 0, 0), new DateTime(2025, 10, 18, 21, 0, 0), 300, 38, 2, 68 },
        { "FL069", 0, new DateTime(2025, 10, 19, 18, 0, 0), new DateTime(2025, 10, 19, 22, 0, 0), 310, 39, 3, 69 },
        { "FL070", 0, new DateTime(2025, 10, 20, 19, 0, 0), new DateTime(2025, 10, 20, 23, 0, 0), 320, 40, 4, 70 },

        { "FL071", 0, new DateTime(2025, 10, 21, 10, 0, 0), new DateTime(2025, 10, 21, 14, 0, 0), 330, 41, 5, 71 },
        { "FL072", 0, new DateTime(2025, 10, 22, 11, 0, 0), new DateTime(2025, 10, 22, 15, 0, 0), 340, 42, 6, 72 },
        { "FL073", 0, new DateTime(2025, 10, 23, 12, 0, 0), new DateTime(2025, 10, 23, 16, 0, 0), 350, 43, 7, 73 },
        { "FL074", 0, new DateTime(2025, 10, 24, 13, 0, 0), new DateTime(2025, 10, 24, 17, 0, 0), 360, 44, 8, 74 },
        { "FL075", 0, new DateTime(2025, 10, 25, 14, 0, 0), new DateTime(2025, 10, 25, 18, 0, 0), 370, 45, 9, 75 },
        { "FL076", 0, new DateTime(2025, 10, 26, 15, 0, 0), new DateTime(2025, 10, 26, 19, 0, 0), 380, 46, 10, 76 },
        { "FL077", 0, new DateTime(2025, 10, 27, 16, 0, 0), new DateTime(2025, 10, 27, 20, 0, 0), 390, 47, 11, 77 },
        { "FL078", 0, new DateTime(2025, 10, 28, 17, 0, 0), new DateTime(2025, 10, 28, 21, 0, 0), 400, 48, 12, 78 },
        { "FL079", 0, new DateTime(2025, 10, 29, 18, 0, 0), new DateTime(2025, 10, 29, 22, 0, 0), 410, 49, 13, 79 },
        { "FL080", 0, new DateTime(2025, 10, 30, 19, 0, 0), new DateTime(2025, 10, 30, 23, 0, 0), 420, 50, 14, 80 },

        { "FL081", 0, new DateTime(2025, 11, 1, 10, 0, 0), new DateTime(2025, 11, 1, 14, 0, 0), 430, 51, 15, 81 },
        { "FL082", 0, new DateTime(2025, 11, 2, 11, 0, 0), new DateTime(2025, 11, 2, 15, 0, 0), 440, 52, 16, 82 },
        { "FL083", 0, new DateTime(2025, 11, 3, 12, 0, 0), new DateTime(2025, 11, 3, 16, 0, 0), 450, 53, 17, 83 },
        { "FL084", 0, new DateTime(2025, 11, 4, 13, 0, 0), new DateTime(2025, 11, 4, 17, 0, 0), 460, 54, 18, 84 },
        { "FL085", 0, new DateTime(2025, 11, 5, 14, 0, 0), new DateTime(2025, 11, 5, 18, 0, 0), 470, 55, 19, 85 },
        { "FL086", 0, new DateTime(2025, 11, 6, 15, 0, 0), new DateTime(2025, 11, 6, 19, 0, 0), 480, 56, 20, 86 },
        { "FL087", 0, new DateTime(2025, 11, 7, 16, 0, 0), new DateTime(2025, 11, 7, 20, 0, 0), 490, 57, 21, 87 },
        { "FL088", 0, new DateTime(2025, 11, 8, 17, 0, 0), new DateTime(2025, 11, 8, 21, 0, 0), 500, 58, 22, 88 },
        { "FL089", 0, new DateTime(2025, 11, 9, 18, 0, 0), new DateTime(2025, 11, 9, 22, 0, 0), 510, 59, 23, 89 },
        { "FL090", 0, new DateTime(2025, 11, 10, 19, 0, 0), new DateTime(2025, 11, 10, 23, 0, 0), 520, 60, 24, 90 },

        { "FL091", 0, new DateTime(2025, 11, 11, 10, 0, 0), new DateTime(2025, 11, 11, 14, 0, 0), 530, 61, 25, 91 },
        { "FL092", 0, new DateTime(2025, 11, 12, 11, 0, 0), new DateTime(2025, 11, 12, 15, 0, 0), 540, 62, 26, 92 },
        { "FL093", 0, new DateTime(2025, 11, 13, 12, 0, 0), new DateTime(2025, 11, 13, 16, 0, 0), 550, 63, 27, 93 },
        { "FL094", 0, new DateTime(2025, 11, 14, 13, 0, 0), new DateTime(2025, 11, 14, 17, 0, 0), 560, 64, 28, 94 },
        { "FL095", 0, new DateTime(2025, 11, 15, 14, 0, 0), new DateTime(2025, 11, 15, 18, 0, 0), 570, 65, 29, 95 },
        { "FL096", 0, new DateTime(2025, 11, 16, 15, 0, 0), new DateTime(2025, 11, 16, 19, 0, 0), 580, 66, 30, 96 },
        { "FL097", 0, new DateTime(2025, 11, 17, 16, 0, 0), new DateTime(2025, 11, 17, 20, 0, 0), 590, 67, 31, 97 },
        { "FL098", 0, new DateTime(2025, 11, 18, 17, 0, 0), new DateTime(2025, 11, 18, 21, 0, 0), 600, 68, 32, 98 },
        { "FL099", 0, new DateTime(2025, 11, 19, 18, 0, 0), new DateTime(2025, 11, 19, 22, 0, 0), 610, 69, 33, 99 },
        { "FL100", 0, new DateTime(2025, 11, 20, 19, 0, 0), new DateTime(2025, 11, 20, 23, 0, 0), 620, 70, 34, 100 }
    });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            for (int i = 100; i <= 150; i++)
            {
                migrationBuilder.DeleteData(
                    table: "Flights",
                    keyColumn: "Title",
                    keyValue: $"FL{i:D3}");
            }

            // ✅ Remove flights Part 2 (FL051–FL100)
            for (int i = 151; i <= 200; i++)
            {
                migrationBuilder.DeleteData(
                    table: "Flights",
                    keyColumn: "Title",
                    keyValue: $"FL{i:D3}");
            }

        }
    }
}
