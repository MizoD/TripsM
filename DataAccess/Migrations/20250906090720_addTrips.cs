using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addTrips : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seats_AirCrafts_AirCraftId",
                table: "Seats");

            migrationBuilder.DropIndex(
                name: "IX_Seats_AirCraftId",
                table: "Seats");

            migrationBuilder.DropColumn(
                name: "AirCraftId",
                table: "Seats");

            migrationBuilder.InsertData(
        table: "Trips",
        columns: new[]
        {
            "Title", "Description", "TripType", "CountryId",
            "ImageUrl", "StartDate", "EndDate",
            "TotalSeats", "AvailableSeats", "Price", "IsAvailable", "Rate"
        },
        values: new object[,]
        {
            // --- Tourism (0) ---
            { "Pyramids of Egypt", "Discover the Great Pyramids and ancient history of Egypt.", 0, 1, "https://example.com/images/pyramids.jpg", new DateTime(2025, 10, 1), new DateTime(2025, 10, 7), 50, 50, 1300m, true, 4.7m },
            { "Tokyo Exploration", "Experience futuristic Tokyo, culture, and cuisine.", 0, 2, "https://example.com/images/tokyo.jpg", new DateTime(2025, 10, 10), new DateTime(2025, 10, 18), 40, 40, 2200m, true, 4.6m },
            { "London City Tour", "See Big Ben, Buckingham Palace, and more.", 0, 3, "https://example.com/images/london.jpg", new DateTime(2025, 11, 5), new DateTime(2025, 11, 12), 35, 35, 1800m, true, 4.5m },
            { "New York Highlights", "Times Square, Central Park, and the Statue of Liberty.", 0, 4, "https://example.com/images/ny.jpg", new DateTime(2025, 11, 20), new DateTime(2025, 11, 27), 60, 60, 2100m, true, 4.6m },
            { "Dubai Tourism", "Luxury shopping, desert safari, and Burj Khalifa.", 0, 5, "https://example.com/images/dubai.jpg", new DateTime(2025, 12, 3), new DateTime(2025, 12, 10), 50, 50, 1700m, true, 4.8m },
            { "Rome Ancient Wonders", "Colosseum, Roman Forum, and Vatican City.", 0, 6, "https://example.com/images/rome.jpg", new DateTime(2026, 1, 10), new DateTime(2026, 1, 17), 45, 45, 1600m, true, 4.7m },
            { "Istanbul Heritage Tour", "Hagia Sophia, Blue Mosque, and Grand Bazaar.", 0, 7, "https://example.com/images/istanbul.jpg", new DateTime(2026, 2, 1), new DateTime(2026, 2, 8), 40, 40, 1500m, true, 4.6m },
            { "Bangkok & Phuket", "Temples, markets, and Thai beaches.", 0, 8, "https://example.com/images/thailand.jpg", new DateTime(2026, 2, 15), new DateTime(2026, 2, 25), 50, 50, 1400m, true, 4.5m },
            { "Barcelona Discovery", "Sagrada Familia, Gothic Quarter, and tapas.", 0, 9, "https://example.com/images/barcelona.jpg", new DateTime(2026, 3, 5), new DateTime(2026, 3, 12), 30, 30, 1700m, true, 4.6m },
            { "Marrakech Colors", "Markets, palaces, and desert landscapes.", 0, 10, "https://example.com/images/marrakech.jpg", new DateTime(2026, 3, 20), new DateTime(2026, 3, 27), 40, 40, 1200m, true, 4.4m },

            // --- Religion (1) ---
            { "Hajj Journey", "Perform Hajj with full guided services.", 1, 11, "https://example.com/images/hajj.jpg", new DateTime(2025, 7, 1), new DateTime(2025, 7, 15), 100, 100, 3500m, true, 4.9m },
            { "Umrah Special", "Perform Umrah with comfortable arrangements.", 1, 11, "https://example.com/images/umrah.jpg", new DateTime(2025, 12, 1), new DateTime(2025, 12, 10), 90, 90, 2000m, true, 4.8m },
            { "Jerusalem Pilgrimage", "Visit Al-Aqsa Mosque and holy sites.", 1, 12, "https://example.com/images/jerusalem.jpg", new DateTime(2026, 1, 5), new DateTime(2026, 1, 12), 50, 50, 1800m, true, 4.7m },
            { "Lourdes, France", "Spiritual healing journey in Lourdes.", 1, 13, "https://example.com/images/lourdes.jpg", new DateTime(2026, 2, 10), new DateTime(2026, 2, 17), 30, 30, 1500m, true, 4.6m },
            { "Vatican Tour", "Explore St. Peter's Basilica and Vatican Museums.", 1, 6, "https://example.com/images/vatican.jpg", new DateTime(2026, 3, 1), new DateTime(2026, 3, 5), 20, 20, 1400m, true, 4.7m },
            { "Varanasi Pilgrimage", "Experience the spiritual city of Varanasi, India.", 1, 14, "https://example.com/images/varanasi.jpg", new DateTime(2026, 3, 20), new DateTime(2026, 3, 27), 40, 40, 1200m, true, 4.5m },
            { "Medina Spiritual Tour", "Visit Masjid an-Nabawi and landmarks.", 1, 11, "https://example.com/images/medina.jpg", new DateTime(2026, 4, 10), new DateTime(2026, 4, 20), 60, 60, 2200m, true, 4.8m },
            { "Mount Sinai Journey", "Climb Mount Sinai for a spiritual experience.", 1, 1, "https://example.com/images/sinai.jpg", new DateTime(2026, 5, 1), new DateTime(2026, 5, 7), 25, 25, 1000m, true, 4.4m },
            { "Assisi Pilgrimage", "Walk in the footsteps of St. Francis.", 1, 6, "https://example.com/images/assisi.jpg", new DateTime(2026, 5, 20), new DateTime(2026, 5, 25), 20, 20, 1300m, true, 4.5m },
            { "Bodh Gaya", "Visit the sacred site where Buddha attained enlightenment.", 1, 14, "https://example.com/images/bodhgaya.jpg", new DateTime(2026, 6, 1), new DateTime(2026, 6, 10), 30, 30, 1100m, true, 4.6m },

            // --- Adventure (2) ---
            { "Safari in Kenya", "Witness the Big Five in Masai Mara.", 2, 15, "https://example.com/images/kenya.jpg", new DateTime(2025, 12, 1), new DateTime(2025, 12, 10), 25, 25, 1800m, true, 4.8m },
            { "Swiss Alps Adventure", "Ski and hike in the Alps.", 2, 16, "https://example.com/images/alps.jpg", new DateTime(2026, 1, 10), new DateTime(2026, 1, 20), 20, 20, 2100m, true, 4.7m },
            { "Amazon Rainforest", "Explore the Amazon jungle and wildlife.", 2, 17, "https://example.com/images/amazon.jpg", new DateTime(2026, 2, 1), new DateTime(2026, 2, 12), 30, 30, 2000m, true, 4.6m },
            { "Patagonia Trek", "Hike the glaciers and peaks of Patagonia.", 2, 18, "https://example.com/images/patagonia.jpg", new DateTime(2026, 3, 5), new DateTime(2026, 3, 15), 20, 20, 2500m, true, 4.8m },
            { "Great Barrier Reef", "Snorkel and dive in Australia’s coral reef.", 2, 19, "https://example.com/images/reef.jpg", new DateTime(2026, 4, 1), new DateTime(2026, 4, 8), 40, 40, 2300m, true, 4.7m },
            { "Everest Base Camp", "Trek to Everest Base Camp in Nepal.", 2, 20, "https://example.com/images/everest.jpg", new DateTime(2026, 4, 20), new DateTime(2026, 5, 5), 15, 15, 2700m, true, 4.9m },
            { "New Zealand Adventure", "Bungee jumping, rafting, and hiking.", 2, 21, "https://example.com/images/nz.jpg", new DateTime(2026, 5, 15), new DateTime(2026, 5, 25), 30, 30, 2400m, true, 4.8m },
            { "Canadian Rockies", "Hike Banff and Jasper national parks.", 2, 22, "https://example.com/images/rockies.jpg", new DateTime(2026, 6, 5), new DateTime(2026, 6, 15), 25, 25, 2200m, true, 4.6m },
            { "Iceland Volcano Tour", "Hike volcanoes and hot springs.", 2, 23, "https://example.com/images/iceland.jpg", new DateTime(2026, 7, 1), new DateTime(2026, 7, 10), 20, 20, 2000m, true, 4.5m },
            { "Peru Inca Trail", "Hike to Machu Picchu.", 2, 17, "https://example.com/images/inca.jpg", new DateTime(2026, 7, 20), new DateTime(2026, 7, 30), 25, 25, 1900m, true, 4.7m },

            // --- Romantic (3) ---
            { "Paris Getaway", "Eiffel Tower and Seine River romance.", 3, 13, "https://example.com/images/paris.jpg", new DateTime(2025, 10, 15), new DateTime(2025, 10, 20), 20, 20, 1600m, true, 4.9m },
            { "Venice Escape", "Gondola rides and Italian romance.", 3, 6, "https://example.com/images/venice.jpg", new DateTime(2025, 11, 1), new DateTime(2025, 11, 7), 15, 15, 1500m, true, 4.8m },
            { "Santorini Sunsets", "Romantic Greek island with stunning views.", 3, 9, "https://example.com/images/santorini.jpg", new DateTime(2026, 1, 1), new DateTime(2026, 1, 8), 20, 20, 1700m, true, 4.9m },
            { "Maldives Paradise", "Stay in overwater villas.", 3, 24, "https://example.com/images/maldives.jpg", new DateTime(2026, 2, 1), new DateTime(2026, 2, 7), 15, 15, 3000m, true, 4.9m },
            { "Bali Romance", "Private villas and beaches in Bali.", 3, 25, "https://example.com/images/bali.jpg", new DateTime(2026, 2, 20), new DateTime(2026, 2, 28), 25, 25, 1800m, true, 4.7m },
            { "Hawaii Honeymoon", "Volcanoes, beaches, and waterfalls.", 3, 26, "https://example.com/images/hawaii.jpg", new DateTime(2026, 3, 10), new DateTime(2026, 3, 17), 30, 30, 2200m, true, 4.8m },
            { "Prague Romance", "Fairytale castles and cobblestone streets.", 3, 27, "https://example.com/images/prague.jpg", new DateTime(2026, 4, 1), new DateTime(2026, 4, 7), 20, 20, 1500m, true, 4.7m },
            { "Vienna Romance", "Classical music and royal palaces.", 3, 28, "https://example.com/images/vienna.jpg", new DateTime(2026, 4, 20), new DateTime(2026, 4, 26), 15, 15, 1400m, true, 4.6m },
            { "Cappadocia Hot Air Balloons", "Sunrise balloon rides in Turkey.", 3, 7, "https://example.com/images/cappadocia.jpg", new DateTime(2026, 5, 5), new DateTime(2026, 5, 10), 20, 20, 1600m, true, 4.8m },
            { "Kyoto Romance", "Cherry blossoms and temples.", 3, 2, "https://example.com/images/kyoto.jpg", new DateTime(2026, 5, 20), new DateTime(2026, 5, 27), 25, 25, 2000m, true, 4.7m }
        });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AirCraftId",
                table: "Seats",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Seats_AirCraftId",
                table: "Seats",
                column: "AirCraftId");

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_AirCrafts_AirCraftId",
                table: "Seats",
                column: "AirCraftId",
                principalTable: "AirCrafts",
                principalColumn: "Id");

            migrationBuilder.Sql("DELETE FROM Trips;");
        }
    }
}
