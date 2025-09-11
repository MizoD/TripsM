using Microsoft.EntityFrameworkCore.Migrations;
using Models;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addAircrafts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "InitialPrice",
                table: "AirCrafts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.InsertData(
table: "AirCrafts",
columns: new[] { "Model", "Capacity", "Status", "AirlineName", "Type", "InitialPrice", "AirportId" },
values: new object[,]
{
    { "Boeing 737", 180, (int)AirCraftStatus.Ready, "Delta Airlines", (int)AirCraftType.Economy, 0.12m, 1 },
    { "Airbus A320", 170, (int)AirCraftStatus.Ready, "American Airlines", (int)AirCraftType.Economy, 0.11m, 2 },
    { "Boeing 787 Dreamliner", 250, (int)AirCraftStatus.Busy, "United Airlines", (int)AirCraftType.Business, 0.18m, 3 },
    { "Airbus A350", 300, (int)AirCraftStatus.Ready, "Lufthansa", (int)AirCraftType.Business, 0.20m, 4 },
    { "Cessna Citation X", 12, (int)AirCraftStatus.Maintainance, "Private Charter", (int)AirCraftType.Private, 0.35m, 5 },
    { "Bombardier Global 7500", 15, (int)AirCraftStatus.Ready, "Private Jet Inc.", (int)AirCraftType.Private, 0.40m, 6 },
    { "Boeing 747", 400, (int)AirCraftStatus.Ready, "British Airways", (int)AirCraftType.Business, 0.25m, 7 },
    { "Embraer E190", 100, (int)AirCraftStatus.Ready, "KLM", (int)AirCraftType.Economy, 0.10m, 8 },
    { "Boeing 777", 350, (int)AirCraftStatus.Busy, "Qatar Airways", (int)AirCraftType.Business, 0.22m, 9 },
    { "Airbus A321", 200, (int)AirCraftStatus.Ready, "Turkish Airlines", (int)AirCraftType.Economy, 0.13m, 10 },
    { "Gulfstream G650", 14, (int)AirCraftStatus.Ready, "Elite Air", (int)AirCraftType.Private, 0.38m, 11 },
    { "Dassault Falcon 8X", 16, (int)AirCraftStatus.Busy, "Luxury Jet Services", (int)AirCraftType.Private, 0.36m, 12 },
    { "Boeing 737 MAX", 190, (int)AirCraftStatus.Ready, "Southwest Airlines", (int)AirCraftType.Economy, 0.12m, 13 },
    { "Airbus A319", 150, (int)AirCraftStatus.Ready, "Iberia", (int)AirCraftType.Economy, 0.11m, 14 },
    { "Boeing 767", 270, (int)AirCraftStatus.Ready, "LATAM Airlines", (int)AirCraftType.Business, 0.19m, 15 },
    { "Airbus A330", 300, (int)AirCraftStatus.Maintainance, "Air France", (int)AirCraftType.Business, 0.21m, 16 },
    { "Embraer E175", 88, (int)AirCraftStatus.Ready, "Alaska Airlines", (int)AirCraftType.Economy, 0.10m, 17 },
    { "Boeing 737-800", 189, (int)AirCraftStatus.Ready, "Ryanair", (int)AirCraftType.Economy, 0.12m, 18 },
    { "Airbus A340", 320, (int)AirCraftStatus.Busy, "Swiss International", (int)AirCraftType.Business, 0.23m, 19 },
    { "Bombardier Challenger 650", 12, (int)AirCraftStatus.Ready, "SkyLux Jets", (int)AirCraftType.Private, 0.34m, 20 },
    { "Boeing 737", 178, (int)AirCraftStatus.Ready, "Aeromexico", (int)AirCraftType.Economy, 0.12m, 21 },
    { "Airbus A320neo", 180, (int)AirCraftStatus.Ready, "Volaris", (int)AirCraftType.Economy, 0.11m, 22 },
    { "Boeing 787", 260, (int)AirCraftStatus.Busy, "ANA", (int)AirCraftType.Business, 0.18m, 23 },
    { "Airbus A350-1000", 350, (int)AirCraftStatus.Ready, "Cathay Pacific", (int)AirCraftType.Business, 0.22m, 24 },
    { "Learjet 75", 9, (int)AirCraftStatus.Ready, "Private Charter Asia", (int)AirCraftType.Private, 0.33m, 25 },
    { "Boeing 757", 220, (int)AirCraftStatus.Ready, "Delta Airlines", (int)AirCraftType.Business, 0.17m, 26 },
    { "Airbus A220", 130, (int)AirCraftStatus.Maintainance, "Air Canada", (int)AirCraftType.Economy, 0.11m, 27 },
    { "Embraer E195", 108, (int)AirCraftStatus.Ready, "Azul Brazilian", (int)AirCraftType.Economy, 0.10m, 28 },
    { "Boeing 737 MAX 9", 193, (int)AirCraftStatus.Ready, "United Airlines", (int)AirCraftType.Economy, 0.12m, 29 },
    { "Airbus A330-900neo", 310, (int)AirCraftStatus.Ready, "TAP Portugal", (int)AirCraftType.Business, 0.21m, 30 },
    { "Gulfstream G700", 17, (int)AirCraftStatus.Ready, "JetLux Europe", (int)AirCraftType.Private, 0.39m, 31 },
    { "Dassault Falcon 2000", 10, (int)AirCraftStatus.Busy, "SkyPrivate", (int)AirCraftType.Private, 0.32m, 32 },
    { "Boeing 737", 185, (int)AirCraftStatus.Ready, "EgyptAir", (int)AirCraftType.Economy, 0.12m, 33 },
    { "Airbus A320", 174, (int)AirCraftStatus.Ready, "Saudia", (int)AirCraftType.Economy, 0.11m, 34 },
    { "Boeing 777-300ER", 380, (int)AirCraftStatus.Ready, "Emirates", (int)AirCraftType.Business, 0.24m, 35 },
    { "Airbus A321neo", 210, (int)AirCraftStatus.Maintainance, "Etihad Airways", (int)AirCraftType.Economy, 0.13m, 36 },
    { "Boeing 737 MAX 8", 186, (int)AirCraftStatus.Ready, "FlyDubai", (int)AirCraftType.Economy, 0.12m, 37 },
    { "Airbus A380", 500, (int)AirCraftStatus.Busy, "Qatar Airways", (int)AirCraftType.Business, 0.28m, 38 },
    { "Cessna Citation CJ4", 9, (int)AirCraftStatus.Ready, "PrivateSky Gulf", (int)AirCraftType.Private, 0.31m, 39 },
    { "Boeing 767", 280, (int)AirCraftStatus.Ready, "Turkish Airlines", (int)AirCraftType.Business, 0.20m, 40 },
    { "Airbus A350", 330, (int)AirCraftStatus.Ready, "Singapore Airlines", (int)AirCraftType.Business, 0.22m, 41 },
    { "Bombardier Global 6000", 14, (int)AirCraftStatus.Ready, "Elite Asia Jets", (int)AirCraftType.Private, 0.37m, 42 },
    { "Embraer E170", 78, (int)AirCraftStatus.Maintainance, "Scandinavian Airlines", (int)AirCraftType.Economy, 0.10m, 43 },
    { "Boeing 737", 182, (int)AirCraftStatus.Ready, "Norwegian Air", (int)AirCraftType.Economy, 0.12m, 44 },
    { "Airbus A220-300", 140, (int)AirCraftStatus.Ready, "AirBaltic", (int)AirCraftType.Economy, 0.11m, 45 },
    { "Gulfstream G550", 14, (int)AirCraftStatus.Busy, "Private Nordic", (int)AirCraftType.Private, 0.35m, 46 },
    { "Boeing 787", 280, (int)AirCraftStatus.Ready, "SAS", (int)AirCraftType.Business, 0.19m, 47 },
    { "Airbus A320neo", 180, (int)AirCraftStatus.Ready, "Finnair", (int)AirCraftType.Economy, 0.11m, 48 },
    { "Boeing 737", 175, (int)AirCraftStatus.Ready, "LOT Polish Airlines", (int)AirCraftType.Economy, 0.12m, 49 },
    { "Airbus A321", 195, (int)AirCraftStatus.Ready, "Austrian Airlines", (int)AirCraftType.Economy, 0.13m, 50 },
});

            migrationBuilder.InsertData(
    table: "AirCrafts",
    columns: new[] { "Model", "Capacity", "Status", "AirlineName", "Type", "InitialPrice", "AirportId" },
    values: new object[,]
    {
    { "Boeing 737 MAX", 188, (int)AirCraftStatus.Ready, "Brussels Airlines", (int)AirCraftType.Economy, 0.12m, 51 },
    { "Airbus A320", 174, (int)AirCraftStatus.Ready, "Swiss Air", (int)AirCraftType.Economy, 0.11m, 52 },
    { "Boeing 777", 330, (int)AirCraftStatus.Busy, "Austrian Airlines", (int)AirCraftType.Business, 0.21m, 53 },
    { "Airbus A330", 290, (int)AirCraftStatus.Ready, "KLM", (int)AirCraftType.Business, 0.20m, 54 },
    { "Cessna Citation XLS+", 9, (int)AirCraftStatus.Ready, "EuroPrivate", (int)AirCraftType.Private, 0.34m, 55 },
    { "Bombardier Global Express", 14, (int)AirCraftStatus.Maintainance, "SkyElite Europe", (int)AirCraftType.Private, 0.36m, 56 },
    { "Boeing 737", 180, (int)AirCraftStatus.Ready, "TAP Portugal", (int)AirCraftType.Economy, 0.12m, 57 },
    { "Airbus A319", 156, (int)AirCraftStatus.Ready, "Iberia", (int)AirCraftType.Economy, 0.11m, 58 },
    { "Boeing 767", 260, (int)AirCraftStatus.Ready, "Thai Airways", (int)AirCraftType.Business, 0.19m, 59 },
    { "Airbus A350", 320, (int)AirCraftStatus.Ready, "Singapore Airlines", (int)AirCraftType.Business, 0.22m, 60 },
    { "Embraer E190", 100, (int)AirCraftStatus.Ready, "Garuda Indonesia", (int)AirCraftType.Economy, 0.10m, 61 },
    { "Boeing 737-900ER", 189, (int)AirCraftStatus.Busy, "Lion Air", (int)AirCraftType.Economy, 0.12m, 62 },
    { "Airbus A321", 200, (int)AirCraftStatus.Ready, "Malaysia Airlines", (int)AirCraftType.Economy, 0.13m, 63 },
    { "Gulfstream G600", 15, (int)AirCraftStatus.Ready, "Private Charter Asia", (int)AirCraftType.Private, 0.37m, 64 },
    { "Dassault Falcon 900LX", 12, (int)AirCraftStatus.Ready, "Elite Asia Jets", (int)AirCraftType.Private, 0.35m, 65 },
    { "Boeing 737", 180, (int)AirCraftStatus.Ready, "Singapore Airlines", (int)AirCraftType.Economy, 0.12m, 66 },
    { "Airbus A320neo", 180, (int)AirCraftStatus.Ready, "Korean Air", (int)AirCraftType.Economy, 0.11m, 67 },
    { "Boeing 777-200ER", 320, (int)AirCraftStatus.Ready, "Asiana Airlines", (int)AirCraftType.Business, 0.23m, 68 },
    { "Airbus A330", 310, (int)AirCraftStatus.Maintainance, "Korean Air", (int)AirCraftType.Business, 0.21m, 69 },
    { "Learjet 70", 8, (int)AirCraftStatus.Ready, "Private Morocco", (int)AirCraftType.Private, 0.32m, 70 },
    { "Boeing 737 MAX", 185, (int)AirCraftStatus.Ready, "Royal Air Maroc", (int)AirCraftType.Economy, 0.12m, 1 },
    { "Airbus A321", 200, (int)AirCraftStatus.Ready, "Delta Airlines", (int)AirCraftType.Economy, 0.13m, 2 },
    { "Boeing 787", 280, (int)AirCraftStatus.Ready, "Air France", (int)AirCraftType.Business, 0.20m, 3 },
    { "Airbus A350", 320, (int)AirCraftStatus.Ready, "Turkish Airlines", (int)AirCraftType.Business, 0.22m, 4 },
    { "Cessna Citation Longitude", 9, (int)AirCraftStatus.Ready, "Private USA", (int)AirCraftType.Private, 0.35m, 5 },
    { "Bombardier Global 8000", 17, (int)AirCraftStatus.Ready, "SkyJet Global", (int)AirCraftType.Private, 0.39m, 6 },
    { "Boeing 747-8", 450, (int)AirCraftStatus.Busy, "Lufthansa", (int)AirCraftType.Business, 0.26m, 7 },
    { "Embraer E195", 110, (int)AirCraftStatus.Ready, "Azul", (int)AirCraftType.Economy, 0.10m, 8 },
    { "Boeing 767", 270, (int)AirCraftStatus.Ready, "United Airlines", (int)AirCraftType.Business, 0.19m, 9 },
    { "Airbus A330neo", 310, (int)AirCraftStatus.Ready, "Virgin Atlantic", (int)AirCraftType.Business, 0.21m, 10 },
    { "Gulfstream G280", 12, (int)AirCraftStatus.Maintainance, "Elite Europe Jets", (int)AirCraftType.Private, 0.33m, 11 },
    { "Dassault Falcon 7X", 14, (int)AirCraftStatus.Ready, "Private Jet Europe", (int)AirCraftType.Private, 0.36m, 12 },
    { "Boeing 737-700", 149, (int)AirCraftStatus.Ready, "Southwest Airlines", (int)AirCraftType.Economy, 0.12m, 13 },
    { "Airbus A220-100", 120, (int)AirCraftStatus.Ready, "AirBaltic", (int)AirCraftType.Economy, 0.11m, 14 },
    { "Boeing 777X", 400, (int)AirCraftStatus.Busy, "Emirates", (int)AirCraftType.Business, 0.27m, 15 },
    { "Airbus A380", 520, (int)AirCraftStatus.Ready, "Qatar Airways", (int)AirCraftType.Business, 0.29m, 16 },
    { "Cessna Citation M2", 7, (int)AirCraftStatus.Ready, "Private Nordic", (int)AirCraftType.Private, 0.30m, 17 },
    { "Bombardier Challenger 350", 11, (int)AirCraftStatus.Ready, "SkyPrivate Europe", (int)AirCraftType.Private, 0.33m, 18 },
    { "Boeing 737 MAX 10", 204, (int)AirCraftStatus.Ready, "United Airlines", (int)AirCraftType.Economy, 0.13m, 19 },
    { "Airbus A321neo", 210, (int)AirCraftStatus.Ready, "Finnair", (int)AirCraftType.Economy, 0.13m, 20 },
    { "Boeing 787-10", 330, (int)AirCraftStatus.Ready, "Singapore Airlines", (int)AirCraftType.Business, 0.22m, 21 },
    { "Airbus A350-900", 320, (int)AirCraftStatus.Ready, "Cathay Pacific", (int)AirCraftType.Business, 0.23m, 22 },
    { "Gulfstream G500", 15, (int)AirCraftStatus.Ready, "Elite Asia Private", (int)AirCraftType.Private, 0.37m, 23 },
    { "Dassault Falcon 6X", 16, (int)AirCraftStatus.Maintainance, "SkyElite Private", (int)AirCraftType.Private, 0.36m, 24 },
    { "Boeing 737", 178, (int)AirCraftStatus.Ready, "Ryanair", (int)AirCraftType.Economy, 0.12m, 25 },
    { "Airbus A320neo", 180, (int)AirCraftStatus.Ready, "EasyJet", (int)AirCraftType.Economy, 0.11m, 26 },
    { "Boeing 747", 420, (int)AirCraftStatus.Busy, "British Airways", (int)AirCraftType.Business, 0.25m, 27 },
    { "Airbus A340", 300, (int)AirCraftStatus.Ready, "Swiss Air", (int)AirCraftType.Business, 0.21m, 28 },
    { "Embraer E175", 86, (int)AirCraftStatus.Ready, "LOT Polish Airlines", (int)AirCraftType.Economy, 0.10m, 29 },
    { "Bombardier Global 7500", 16, (int)AirCraftStatus.Ready, "Private Global", (int)AirCraftType.Private, 0.40m, 30 },
    });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InitialPrice",
                table: "AirCrafts");

            // First 25 Aircraft
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Boeing 737-800" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Airbus A320" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Boeing 777-300ER" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Airbus A350-900" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Boeing 787-9" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Embraer E190" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Bombardier Q400" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Airbus A321neo" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Boeing 747-8" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Airbus A330-300" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Cessna Citation X" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Gulfstream G650" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Boeing 767-300" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Airbus A220-300" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "ATR 72-600" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Dassault Falcon 2000" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Boeing 737 MAX 8" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Airbus A319" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Boeing 727" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Concorde" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Lockheed L-1011 TriStar" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Tupolev Tu-154" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Comac C919" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Irkut MC-21" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Mitsubishi SpaceJet" });

            // 26–50
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Antonov An-148" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Boeing 737-900ER" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Airbus A340-600" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Sukhoi Superjet 100" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Boeing 757-200" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Airbus A318" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Bombardier CRJ900" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Embraer E175" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Cessna Citation CJ4" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Gulfstream G500" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Learjet 75" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Boeing 737-700" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Airbus A310" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "McDonnell Douglas DC-10" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Douglas DC-8" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Vickers VC10" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Fokker 100" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "BAe 146" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Tupolev Tu-204" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Yakovlev Yak-42" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Boeing 737-600" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Airbus A350-1000" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Bombardier Global 7500" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Pilatus PC-24" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "HondaJet HA-420" });

            // 51–75
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Boeing 737 MAX 9" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Airbus A321XLR" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Boeing 747-400" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Airbus A330-200" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Lockheed C-130 Hercules" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Boeing 707" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Douglas DC-3" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Tupolev Tu-134" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Antonov An-225" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Ilyushin Il-76" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Bombardier Challenger 350" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Dassault Falcon 7X" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Embraer Phenom 300" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Beechcraft King Air 350" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Saab 340" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Fokker 50" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Comac ARJ21" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Antonov An-24" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Let L-410 Turbolet" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Yakovlev Yak-40" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Boeing 787-10" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Airbus A220-100" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "McDonnell Douglas MD-80" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Douglas DC-9" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "British Aerospace Jetstream 41" });

            // 76–100
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Bombardier CRJ200" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Embraer Legacy 650" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Cessna 208 Caravan" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Pilatus PC-12" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "De Havilland Canada DHC-6 Twin Otter" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Shorts 360" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Fairchild Swearingen Metroliner" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Beechcraft 1900" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Ilyushin Il-96" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Tupolev Tu-204SM" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Antonov An-140" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Sukhoi Superjet 130" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Bombardier CS100" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Airbus BelugaXL" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Boeing Dreamlifter" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Lockheed Martin LM-100J" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Comac C929" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "McDonnell Douglas MD-11" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Boeing 737 MAX 10" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Airbus A380" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Bombardier Learjet 85" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Cessna Citation Longitude" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Gulfstream G700" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Dassault Falcon 8X" });
            migrationBuilder.DeleteData("AirCrafts", new[] { "Model" }, new object[] { "Embraer Praetor 600" });
        }
    }
}
