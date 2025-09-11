using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addhotelsAndairports : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
            table: "Airports",
            columns: new[] { "Name", "City", "Latitude", "Longitude", "CountryId" },
            values: new object[,]
            {
                // United States
                { "John F. Kennedy International Airport", "New York", 40.6413, -73.7781, 1 },
                { "Los Angeles International Airport", "Los Angeles", 33.9416, -118.4085, 1 },

                // United Kingdom
                { "Heathrow Airport", "London", 51.4700, -0.4543, 2 },
                { "Manchester Airport", "Manchester", 53.3656, -2.2726, 2 },

                // Canada
                { "Toronto Pearson International Airport", "Toronto", 43.6777, -79.6248, 3 },
                { "Vancouver International Airport", "Vancouver", 49.1951, -123.1779, 3 },

                // Germany
                { "Frankfurt Airport", "Frankfurt", 50.0379, 8.5622, 4 },
                { "Munich Airport", "Munich", 48.3538, 11.7861, 4 },

                // France
                { "Charles de Gaulle Airport", "Paris", 49.0097, 2.5479, 5 },
                { "Nice Côte d'Azur Airport", "Nice", 43.6653, 7.2150, 5 },

                // Spain
                { "Adolfo Suárez Madrid–Barajas Airport", "Madrid", 40.4893, -3.5676, 6 },
                { "Barcelona–El Prat Airport", "Barcelona", 41.2974, 2.0833, 6 },

                // Italy
                { "Leonardo da Vinci–Fiumicino Airport", "Rome", 41.8003, 12.2389, 7 },
                { "Milan Malpensa Airport", "Milan", 45.6306, 8.7281, 7 },

                // Australia
                { "Sydney Kingsford Smith Airport", "Sydney", -33.9399, 151.1753, 8 },
                { "Melbourne Airport", "Melbourne", -37.6733, 144.8430, 8 },

                // Japan
                { "Tokyo Haneda Airport", "Tokyo", 35.5494, 139.7798, 9 },
                { "Kansai International Airport", "Osaka", 34.4342, 135.2440, 9 },

                // China
                { "Beijing Capital International Airport", "Beijing", 40.0799, 116.6031, 10 },
                { "Shanghai Pudong International Airport", "Shanghai", 31.1443, 121.8083, 10 },

                // India
                { "Indira Gandhi International Airport", "Delhi", 28.5562, 77.1000, 11 },
                { "Chhatrapati Shivaji Maharaj International Airport", "Mumbai", 19.0896, 72.8656, 11 },

                // Brazil
                { "São Paulo/Guarulhos International Airport", "São Paulo", -23.4356, -46.4731, 12 },
                { "Rio de Janeiro–Galeão International Airport", "Rio de Janeiro", -22.8089, -43.2436, 12 },

                // Mexico
                { "Mexico City International Airport", "Mexico City", 19.4361, -99.0719, 13 },
                { "Cancún International Airport", "Cancún", 21.0365, -86.8771, 13 },

                // Russia
                { "Sheremetyevo International Airport", "Moscow", 55.9726, 37.4146, 14 },
                { "Pulkovo Airport", "Saint Petersburg", 59.8003, 30.2625, 14 },

                // South Africa
                { "O. R. Tambo International Airport", "Johannesburg", -26.1367, 28.2410, 15 },
                { "Cape Town International Airport", "Cape Town", -33.9648, 18.6017, 15 },

                // Egypt
                { "Cairo International Airport", "Cairo", 30.1219, 31.4056, 16 },
                { "Hurghada International Airport", "Hurghada", 27.1800, 33.7984, 16 },

                // Saudi Arabia
                { "King Abdulaziz International Airport", "Jeddah", 21.6702, 39.1528, 17 },
                { "King Khalid International Airport", "Riyadh", 24.9594, 46.6981, 17 },

                // Turkey
                { "Istanbul Airport", "Istanbul", 41.2753, 28.7519, 18 },
                { "Antalya Airport", "Antalya", 36.8987, 30.8006, 18 },

                // UAE
                { "Dubai International Airport", "Dubai", 25.2532, 55.3657, 19 },
                { "Abu Dhabi International Airport", "Abu Dhabi", 24.4329, 54.6511, 19 },

                // Qatar
                { "Hamad International Airport", "Doha", 25.2731, 51.6081, 20 },
                { "Doha International Airport", "Doha", 25.2600, 51.5650, 20 },

                // Greece
                { "Athens International Airport", "Athens", 37.9364, 23.9445, 21 },
                { "Thessaloniki Airport", "Thessaloniki", 40.5197, 22.9709, 21 },

                // Sweden
                { "Stockholm Arlanda Airport", "Stockholm", 59.6519, 17.9186, 22 },
                { "Göteborg Landvetter Airport", "Gothenburg", 57.6688, 12.2910, 22 },

                // Norway
                { "Oslo Gardermoen Airport", "Oslo", 60.1976, 11.1004, 23 },
                { "Bergen Airport", "Bergen", 60.2934, 5.2181, 23 },

                // Denmark
                { "Copenhagen Airport", "Copenhagen", 55.6180, 12.6560, 24 },
                { "Billund Airport", "Billund", 55.7403, 9.1518, 24 },

                // Netherlands
                { "Amsterdam Schiphol Airport", "Amsterdam", 52.3105, 4.7683, 25 },
                { "Rotterdam The Hague Airport", "Rotterdam", 51.9569, 4.4372, 25 },

                // Belgium
                { "Brussels Airport", "Brussels", 50.9010, 4.4844, 26 },
                { "Antwerp International Airport", "Antwerp", 51.1894, 4.4602, 26 },

                // Switzerland
                { "Zurich Airport", "Zurich", 47.4582, 8.5555, 27 },
                { "Geneva Airport", "Geneva", 46.2381, 6.1090, 27 },

                // Austria
                { "Vienna International Airport", "Vienna", 48.1103, 16.5697, 28 },
                { "Salzburg Airport", "Salzburg", 47.7944, 13.0033, 28 },

                // Portugal
                { "Lisbon Airport", "Lisbon", 38.7742, -9.1342, 29 },
                { "Porto Airport", "Porto", 41.2481, -8.6814, 29 },

                // Thailand
                { "Suvarnabhumi Airport", "Bangkok", 13.6900, 100.7501, 30 },
                { "Chiang Mai Airport", "Chiang Mai", 18.7668, 98.9626, 30 },

                // Indonesia
                { "Soekarno–Hatta International Airport", "Jakarta", -6.1256, 106.6559, 31 },
                { "Ngurah Rai International Airport", "Bali", -8.7481, 115.1670, 31 },

                // Malaysia
                { "Kuala Lumpur International Airport", "Kuala Lumpur", 2.7456, 101.7072, 32 },
                { "Penang International Airport", "Penang", 5.2971, 100.2770, 32 },

                // Singapore
                { "Singapore Changi Airport", "Singapore", 1.3644, 103.9915, 33 },
                { "Seletar Airport", "Singapore", 1.4169, 103.8672, 33 },

                // South Korea
                { "Incheon International Airport", "Seoul", 37.4602, 126.4407, 34 },
                { "Gimpo International Airport", "Seoul", 37.5626, 126.8020, 34 },

                // Morocco
                { "Mohammed V International Airport", "Casablanca", 33.3675, -7.5899, 35 },
                { "Marrakech Menara Airport", "Marrakech", 31.6069, -8.0363, 35 }
            });

            migrationBuilder.InsertData(
        table: "Hotels",
        columns: new[] { "Name", "Description", "Phone", "AvailableRooms", "PricePerNight", "City", "MainImg", "Traffic", "CountryId" },
        values: new object[,]
        {
            // 1. Egypt
            { "Nile View Hotel", "A luxury hotel overlooking the Nile in Cairo.", "+20 1000000001", 120, 150m, "Cairo", "https://picsum.photos/600/400?random=1", 0, 1 },
            { "Alexandria Beach Resort", "A seaside resort with private beaches.", "+20 1000000002", 90, 120m, "Alexandria", "https://picsum.photos/600/400?random=2", 0, 1 },

            // 2. Saudi Arabia
            { "Makkah Tower Hotel", "Luxury hotel near Al-Haram Mosque.", "+966 500000001", 200, 250m, "Makkah", "https://picsum.photos/600/400?random=3", 0, 2 },
            { "Riyadh Grand Inn", "Business-friendly hotel in the heart of Riyadh.", "+966 500000002", 150, 180m, "Riyadh", "https://picsum.photos/600/400?random=4", 0, 2 },

            // 3. UAE
            { "Dubai Marina Suites", "Modern hotel with views of Dubai Marina.", "+971 500000001", 180, 220m, "Dubai", "https://picsum.photos/600/400?random=5", 0, 3 },
            { "Abu Dhabi Palace Hotel", "Elegant hotel with Arabian Gulf views.", "+971 500000002", 130, 200m, "Abu Dhabi", "https://picsum.photos/600/400?random=6", 0, 3 },

            // 4. USA
            { "New York Central Hotel", "Luxury hotel in Manhattan.", "+1 2120000001", 250, 300m, "New York", "https://picsum.photos/600/400?random=7", 0, 4 },
            { "Los Angeles Sunset Inn", "Cozy hotel near Hollywood Boulevard.", "+1 3230000002", 160, 180m, "Los Angeles", "https://picsum.photos/600/400?random=8", 0, 4 },

            // 5. UK
            { "London Bridge Hotel", "Charming hotel near Tower Bridge.", "+44 2000000001", 170, 220m, "London", "https://picsum.photos/600/400?random=9", 0, 5 },
            { "Manchester City Lodge", "Comfortable stay in Manchester’s center.", "+44 2000000002", 100, 150m, "Manchester", "https://picsum.photos/600/400?random=10", 0, 5 },

            // 6. France
            { "Paris Eiffel View", "Romantic hotel overlooking the Eiffel Tower.", "+33 100000001", 200, 250m, "Paris", "https://picsum.photos/600/400?random=11", 0, 6 },
            { "Nice Beach Hotel", "Beachfront hotel on the French Riviera.", "+33 100000002", 140, 190m, "Nice", "https://picsum.photos/600/400?random=12", 0, 6 },

            // 7. Germany
            { "Berlin Central Inn", "Modern hotel close to Brandenburg Gate.", "+49 300000001", 180, 210m, "Berlin", "https://picsum.photos/600/400?random=13", 0, 7 },
            { "Munich Alps Resort", "Mountain resort near the Bavarian Alps.", "+49 300000002", 120, 200m, "Munich", "https://picsum.photos/600/400?random=14", 0, 7 },

            // 8. Italy
            { "Rome Colosseum Hotel", "Stay in the heart of ancient Rome.", "+39 600000001", 160, 220m, "Rome", "https://picsum.photos/600/400?random=15", 0, 8 },
            { "Venice Canal Suites", "Luxury suites by the Venice canals.", "+39 600000002", 100, 250m, "Venice", "https://picsum.photos/600/400?random=16", 0, 8 },

            // 9. Spain
            { "Madrid Plaza Hotel", "Boutique hotel near Plaza Mayor.", "+34 700000001", 150, 190m, "Madrid", "https://picsum.photos/600/400?random=17", 0, 9 },
            { "Barcelona Beach Resort", "Resort with sea views and pools.", "+34 700000002", 130, 200m, "Barcelona", "https://picsum.photos/600/400?random=18", 0, 9 },

            // 10. Japan
            { "Tokyo Skytree Hotel", "Modern hotel with city skyline views.", "+81 800000001", 220, 260m, "Tokyo", "https://picsum.photos/600/400?random=19", 0, 10 },
            { "Kyoto Zen Inn", "Traditional Ryokan with gardens.", "+81 800000002", 90, 180m, "Kyoto", "https://picsum.photos/600/400?random=20", 0, 10 },

            // 11. China
            { "Beijing Imperial Hotel", "Luxury stay near the Forbidden City.", "+86 900000001", 240, 270m, "Beijing", "https://picsum.photos/600/400?random=21", 0, 11 },
            { "Shanghai Skyline Inn", "Modern hotel with Bund views.", "+86 900000002", 200, 250m, "Shanghai", "https://picsum.photos/600/400?random=22", 0, 11 },

            // 12. India
            { "Taj Mahal Palace", "Iconic hotel near Gateway of India.", "+91 910000001", 300, 280m, "Mumbai", "https://picsum.photos/600/400?random=23", 0, 12 },
            { "Delhi Heritage Inn", "Stay close to India Gate.", "+91 910000002", 180, 200m, "Delhi", "https://picsum.photos/600/400?random=24", 0, 12 },

            // 13. Brazil
            { "Rio Beach Hotel", "Beachfront stay in Copacabana.", "+55 920000001", 200, 220m, "Rio de Janeiro", "https://picsum.photos/600/400?random=25", 0, 13 },
            { "São Paulo Grand Inn", "Modern hotel in São Paulo center.", "+55 920000002", 160, 190m, "São Paulo", "https://picsum.photos/600/400?random=26", 0, 13 },

            // 14. Mexico
            { "Cancun Paradise Resort", "All-inclusive beachfront resort.", "+52 930000001", 250, 230m, "Cancun", "https://picsum.photos/600/400?random=27", 0, 14 },
            { "Mexico City Plaza Hotel", "Historic hotel in city center.", "+52 930000002", 150, 180m, "Mexico City", "https://picsum.photos/600/400?random=28", 0, 14 },

            // 15. Canada
            { "Toronto Skyline Hotel", "Modern hotel with CN Tower views.", "+1 4160000001", 180, 210m, "Toronto", "https://picsum.photos/600/400?random=29", 0, 15 },
            { "Vancouver Harbor Inn", "Hotel overlooking Vancouver waterfront.", "+1 6040000002", 140, 190m, "Vancouver", "https://picsum.photos/600/400?random=30", 0, 15 },

            // 16. Australia
            { "Sydney Opera Hotel", "Luxury hotel near Sydney Opera House.", "+61 940000001", 200, 250m, "Sydney", "https://picsum.photos/600/400?random=31", 0, 16 },
            { "Melbourne City Inn", "Modern stay in Melbourne center.", "+61 940000002", 160, 200m, "Melbourne", "https://picsum.photos/600/400?random=32", 0, 16 },

            // 17. South Africa
            { "Cape Town Beach Resort", "Hotel by Table Mountain and beaches.", "+27 950000001", 170, 220m, "Cape Town", "https://picsum.photos/600/400?random=33", 0, 17 },
            { "Johannesburg Central Inn", "Business-friendly hotel in Joburg.", "+27 950000002", 130, 180m, "Johannesburg", "https://picsum.photos/600/400?random=34", 0, 17 },

            // 18. Turkey
            { "Istanbul Bosphorus Hotel", "Views of the Bosphorus bridge.", "+90 960000001", 200, 240m, "Istanbul", "https://picsum.photos/600/400?random=35", 0, 18 },
            { "Ankara City Suites", "Comfortable suites in Ankara.", "+90 960000002", 150, 190m, "Ankara", "https://picsum.photos/600/400?random=36", 0, 18 },

            // 19. Russia
            { "Moscow Red Square Inn", "Stay near Red Square.", "+7 970000001", 220, 260m, "Moscow", "https://picsum.photos/600/400?random=37", 0, 19 },
            { "Saint Petersburg Palace", "Luxury hotel with canal views.", "+7 970000002", 180, 230m, "Saint Petersburg", "https://picsum.photos/600/400?random=38", 0, 19 },

            // 20. Argentina
            { "Buenos Aires Tango Hotel", "Cultural hotel in BA center.", "+54 980000001", 170, 200m, "Buenos Aires", "https://picsum.photos/600/400?random=39", 0, 20 },
            { "Mendoza Wine Resort", "Resort near vineyards in Mendoza.", "+54 980000002", 120, 180m, "Mendoza", "https://picsum.photos/600/400?random=40", 0, 20 },

            // 21. Greece
            { "Athens Acropolis Hotel", "Hotel near the Acropolis.", "+30 990000001", 150, 210m, "Athens", "https://picsum.photos/600/400?random=41", 0, 21 },
            { "Santorini Sunset Resort", "Romantic stay with sea views.", "+30 990000002", 100, 250m, "Santorini", "https://picsum.photos/600/400?random=42", 0, 21 },

            // 22. Thailand
            { "Bangkok Riverside Hotel", "Luxury hotel by Chao Phraya River.", "+66 991000001", 200, 220m, "Bangkok", "https://picsum.photos/600/400?random=43", 0, 22 },
            { "Phuket Beach Resort", "Seaside hotel with tropical vibes.", "+66 991000002", 150, 200m, "Phuket", "https://picsum.photos/600/400?random=44", 0, 22 },

            // 23. Indonesia
            { "Bali Paradise Inn", "Traditional Balinese luxury resort.", "+62 992000001", 180, 230m, "Bali", "https://picsum.photos/600/400?random=45", 0, 23 },
            { "Jakarta City Suites", "Modern hotel in Jakarta center.", "+62 992000002", 140, 180m, "Jakarta", "https://picsum.photos/600/400?random=46", 0, 23 },

            // 24. Malaysia
            { "Kuala Lumpur Skyline Hotel", "Views of Petronas Towers.", "+60 993000001", 200, 220m, "Kuala Lumpur", "https://picsum.photos/600/400?random=47", 0, 24 },
            { "Langkawi Beach Resort", "Island getaway with sea views.", "+60 993000002", 130, 200m, "Langkawi", "https://picsum.photos/600/400?random=48", 0, 24 },

            // 25. Singapore
            { "Marina Bay Suites", "Famous luxury hotel in Marina Bay.", "+65 994000001", 250, 300m, "Singapore", "https://picsum.photos/600/400?random=49", 0, 25 },
            { "Orchard Road Hotel", "Boutique hotel on Orchard Road.", "+65 994000002", 150, 220m, "Singapore", "https://picsum.photos/600/400?random=50", 0, 25 },

            // 26. Switzerland
            { "Zurich Lake Hotel", "Luxury hotel by Lake Zurich.", "+41 995000001", 180, 260m, "Zurich", "https://picsum.photos/600/400?random=51", 0, 26 },
            { "Geneva Mountain Inn", "Hotel near Swiss Alps.", "+41 995000002", 120, 230m, "Geneva", "https://picsum.photos/600/400?random=52", 0, 26 },

            // 27. Netherlands
            { "Amsterdam Canal Suites", "Stay in traditional canal houses.", "+31 996000001", 170, 210m, "Amsterdam", "https://picsum.photos/600/400?random=53", 0, 27 },
            { "Rotterdam City Inn", "Modern hotel in Rotterdam center.", "+31 996000002", 130, 180m, "Rotterdam", "https://picsum.photos/600/400?random=54", 0, 27 },

            // 28. Belgium
            { "Brussels Grand Hotel", "Luxury hotel near Grand Place.", "+32 997000001", 160, 220m, "Brussels", "https://picsum.photos/600/400?random=55", 0, 28 },
            { "Antwerp Diamond Inn", "Boutique hotel in Antwerp center.", "+32 997000002", 120, 180m, "Antwerp", "https://picsum.photos/600/400?random=56", 0, 28 },

            // 29. Austria
            { "Vienna Imperial Hotel", "Historic hotel in Vienna center.", "+43 998000001", 200, 240m, "Vienna", "https://picsum.photos/600/400?random=57", 0, 29 },
            { "Salzburg Hillside Inn", "Charming hotel near Salzburg Alps.", "+43 998000002", 140, 200m, "Salzburg", "https://picsum.photos/600/400?random=58", 0, 29 },

            // 30. Sweden
            { "Stockholm Archipelago Hotel", "Hotel with sea and city views.", "+46 999000001", 170, 220m, "Stockholm", "https://picsum.photos/600/400?random=59", 0, 30 },
            { "Gothenburg Harbor Inn", "Stay near Gothenburg waterfront.", "+46 999000002", 120, 180m, "Gothenburg", "https://picsum.photos/600/400?random=60", 0, 30 },

            // 31. Norway
            { "Oslo Fjord Hotel", "Luxury hotel with fjord views.", "+47 999100001", 150, 210m, "Oslo", "https://picsum.photos/600/400?random=61", 0, 31 },
            { "Bergen Mountain Inn", "Stay close to Norwegian mountains.", "+47 999100002", 100, 180m, "Bergen", "https://picsum.photos/600/400?random=62", 0, 31 },

            // 32. Denmark
            { "Copenhagen City Suites", "Stylish hotel in Copenhagen center.", "+45 999200001", 180, 220m, "Copenhagen", "https://picsum.photos/600/400?random=63", 0, 32 },
            { "Aarhus Seaside Inn", "Cozy hotel near Aarhus beaches.", "+45 999200002", 130, 190m, "Aarhus", "https://picsum.photos/600/400?random=64", 0, 32 },

            // 33. Portugal
            { "Lisbon Riverside Hotel", "Luxury stay by the Tagus River.", "+351 999300001", 160, 200m, "Lisbon", "https://picsum.photos/600/400?random=65", 0, 33 },
            { "Porto Wine Inn", "Boutique hotel near Douro River.", "+351 999300002", 120, 180m, "Porto", "https://picsum.photos/600/400?random=66", 0, 33 },

            { "Sharm El Sheikh Resort", "Beach resort on the Red Sea.", "+20 1000000003", 180, 220m, "Sharm El Sheikh", "https://picsum.photos/600/400?random=67", 0, 34 },
            { "Luxor Nile Palace", "Luxury hotel with views of ancient temples.", "+20 1000000004", 140, 190m, "Luxor", "https://picsum.photos/600/400?random=68", 0, 34 },

            // 35. Morocco
            { "Marrakech Medina Hotel", "Traditional Riad-style stay in the Medina.", "+212 999400001", 160, 200m, "Marrakech", "https://picsum.photos/600/400?random=69", 0, 35 },
            { "Casablanca Ocean View", "Modern hotel overlooking the Atlantic.", "+212 999400002", 130, 180m, "Casablanca", "https://picsum.photos/600/400?random=70", 0, 35 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "John F. Kennedy International Airport" });
            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "Los Angeles International Airport" });

            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "Heathrow Airport" });
            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "Manchester Airport" });

            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "Toronto Pearson International Airport" });
            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "Vancouver International Airport" });

            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "Frankfurt Airport" });
            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "Munich Airport" });

            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "Charles de Gaulle Airport" });
            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "Nice Côte d'Azur Airport" });

            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "Adolfo Suárez Madrid–Barajas Airport" });
            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "Barcelona–El Prat Airport" });

            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "Leonardo da Vinci–Fiumicino Airport" });
            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "Milan Malpensa Airport" });

            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "Sydney Kingsford Smith Airport" });
            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "Melbourne Airport" });

            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "Tokyo Haneda Airport" });
            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "Kansai International Airport" });

            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "Beijing Capital International Airport" });
            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "Shanghai Pudong International Airport" });

            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "Indira Gandhi International Airport" });
            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "Chhatrapati Shivaji Maharaj International Airport" });

            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "São Paulo/Guarulhos International Airport" });
            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "Rio de Janeiro–Galeão International Airport" });

            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "Mexico City International Airport" });
            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "Cancún International Airport" });

            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "Sheremetyevo International Airport" });
            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "Pulkovo Airport" });

            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "O. R. Tambo International Airport" });
            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "Cape Town International Airport" });

            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "Cairo International Airport" });
            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "Hurghada International Airport" });

            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "King Abdulaziz International Airport" });
            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "King Khalid International Airport" });

            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "Istanbul Airport" });
            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "Antalya Airport" });

            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "Dubai International Airport" });
            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "Abu Dhabi International Airport" });

            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "Hamad International Airport" });
            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "Doha International Airport" });

            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "Athens International Airport" });
            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "Thessaloniki Airport" });

            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "Stockholm Arlanda Airport" });
            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "Göteborg Landvetter Airport" });

            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "Oslo Gardermoen Airport" });
            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "Bergen Airport" });

            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "Copenhagen Airport" });
            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "Billund Airport" });

            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "Amsterdam Schiphol Airport" });
            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "Rotterdam The Hague Airport" });

            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "Brussels Airport" });
            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "Antwerp International Airport" });

            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "Zurich Airport" });
            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "Geneva Airport" });

            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "Vienna International Airport" });
            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "Salzburg Airport" });

            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "Lisbon Airport" });
            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "Porto Airport" });

            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "Suvarnabhumi Airport" });
            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "Chiang Mai Airport" });

            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "Soekarno–Hatta International Airport" });
            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "Ngurah Rai International Airport" });

            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "Kuala Lumpur International Airport" });
            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "Penang International Airport" });

            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "Singapore Changi Airport" });
            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "Seletar Airport" });

            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "Incheon International Airport" });
            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "Gimpo International Airport" });

            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "Mohammed V International Airport" });
            migrationBuilder.DeleteData("Airports", new[] { "Name" }, new object[] { "Marrakech Menara Airport" });

            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "Nile View Hotel", 1 });
            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "Alexandria Beach Resort", 1 });

            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "Makkah Tower Hotel", 2 });
            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "Riyadh Grand Inn", 2 });

            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "Dubai Marina Suites", 3 });
            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "Abu Dhabi Palace Hotel", 3 });

            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "New York Central Hotel", 4 });
            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "Los Angeles Sunset Inn", 4 });

            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "Toronto Maple Hotel", 5 });
            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "Vancouver Harbour Resort", 5 });

            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "London Bridge Hotel", 6 });
            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "Manchester Grand Inn", 6 });

            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "Paris Eiffel Suites", 7 });
            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "Nice Riviera Hotel", 7 });

            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "Berlin Central Hotel", 8 });
            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "Munich Bavarian Inn", 8 });

            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "Rome Colosseum Hotel", 9 });
            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "Milan Fashion Suites", 9 });

            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "Madrid Plaza Inn", 10 });
            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "Barcelona Coast Resort", 10 });

            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "Athens Acropolis Hotel", 11 });
            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "Santorini Blue Resort", 11 });

            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "Istanbul Bosphorus Suites", 12 });
            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "Ankara Central Hotel", 12 });

            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "Beijing Forbidden Hotel", 13 });
            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "Shanghai Skyline Inn", 13 });

            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "Tokyo Shibuya Hotel", 14 });
            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "Kyoto Zen Resort", 14 });

            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "Delhi Grand Inn", 15 });
            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "Mumbai Sea View", 15 });

            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "Moscow Red Square Hotel", 16 });
            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "St. Petersburg Palace", 16 });

            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "Rio Copacabana Inn", 17 });
            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "São Paulo Central Suites", 17 });

            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "Buenos Aires Tango Hotel", 18 });
            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "Cordoba Mountain Inn", 18 });

            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "Cape Town Ocean View", 19 });
            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "Johannesburg Safari Hotel", 19 });

            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "Sydney Opera Suites", 20 });
            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "Melbourne Harbour Hotel", 20 });

            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "Wellington Bay Hotel", 21 });
            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "Auckland Harbour Suites", 21 });

            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "Cairo Pyramids Hotel", 22 });
            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "Luxor Nile Palace", 22 });

            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "Havana Colonial Hotel", 23 });
            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "Varadero Beach Resort", 23 });

            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "Bangkok Riverside Hotel", 24 });
            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "Phuket Island Resort", 24 });

            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "Seoul Gangnam Suites", 25 });
            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "Busan Beach Hotel", 25 });

            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "Nairobi Safari Lodge", 26 });
            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "Mombasa Ocean Hotel", 26 });

            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "Hanoi Old Quarter Hotel", 27 });
            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "Ho Chi Minh City Grand Inn", 27 });

            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "Kuala Lumpur Tower Hotel", 28 });
            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "Penang Island Resort", 28 });

            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "Singapore Bayfront Hotel", 29 });
            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "Sentosa Island Resort", 29 });

            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "Doha Corniche Hotel", 30 });
            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "Al Wakrah Beach Resort", 30 });

            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "Kuwait City Central Inn", 31 });
            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "Salmiya Sea View Hotel", 31 });

            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "Manama Bay Hotel", 32 });
            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "Riffa Heights Inn", 32 });

            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "Muscat Coastal Resort", 33 });
            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "Salalah Palm Hotel", 33 });

            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "Amman Citadel Hotel", 34 });
            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "Petra Desert Resort", 34 });

            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "Marrakech Medina Hotel", 35 });
            migrationBuilder.DeleteData("Hotels", new[] { "Name", "CountryId" }, new object[] { "Casablanca Ocean View", 35 });

        }
    }
}
