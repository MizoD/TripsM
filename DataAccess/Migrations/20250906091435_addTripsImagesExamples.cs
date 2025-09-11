using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addTripsImagesExamples : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
    table: "TripImage",
    columns: new[] { "Id", "ImageUrl", "TripId" },
    values: new object[,]
    {
        { 1, "https://images.unsplash.com/photo-1507525428034-b723cf961d3e", 1 },
        { 2, "https://images.unsplash.com/photo-1506748686214-e9df14d4d9d0", 1 },
        { 3, "https://images.unsplash.com/photo-1522708323590-d24dbb6b0267", 1 },

        { 4, "https://images.unsplash.com/photo-1491553895911-0055eca6402d", 2 },
        { 5, "https://images.unsplash.com/photo-1519817650390-64a93db511aa", 2 },
        { 6, "https://images.unsplash.com/photo-1500530855697-b586d89ba3ee", 2 },

        { 7, "https://images.unsplash.com/photo-1521295121783-8a321d551ad2", 3 },
        { 8, "https://images.unsplash.com/photo-1499696010181-9c8e59f0bc04", 3 },
        { 9, "https://images.unsplash.com/photo-1534081333815-ae5019106622", 3 },
        { 10, "https://images.unsplash.com/photo-1529429617124-95b109e86bb8", 3 },

        { 11, "https://images.unsplash.com/photo-1470770841072-f978cf4d019e", 4 },
        { 12, "https://images.unsplash.com/photo-1524492449090-1a065f2d7d10", 4 },
        { 13, "https://images.unsplash.com/photo-1503264116251-35a269479413", 4 },

        { 14, "https://images.unsplash.com/photo-1441974231531-c6227db76b6e", 5 },
        { 15, "https://images.unsplash.com/photo-1493810329807-34bb39a3161f", 5 },
        { 16, "https://images.unsplash.com/photo-1455659817273-f96807779a8d", 5 },
        { 17, "https://images.unsplash.com/photo-1469474968028-56623f02e42e", 5 },

        { 18, "https://images.unsplash.com/photo-1472214103451-9374bd1c798e", 6 },
        { 19, "https://images.unsplash.com/photo-1501785888041-af3ef285b470", 6 },
        { 20, "https://images.unsplash.com/photo-1507525428034-b723cf961d3e", 6 },

        { 21, "https://images.unsplash.com/photo-1491553895911-0055eca6402d", 7 },
        { 22, "https://images.unsplash.com/photo-1500530855697-b586d89ba3ee", 7 },
        { 23, "https://images.unsplash.com/photo-1506748686214-e9df14d4d9d0", 7 },

        { 24, "https://images.unsplash.com/photo-1522708323590-d24dbb6b0267", 8 },
        { 25, "https://images.unsplash.com/photo-1519817650390-64a93db511aa", 8 },
        { 26, "https://images.unsplash.com/photo-1534081333815-ae5019106622", 8 },

        { 27, "https://images.unsplash.com/photo-1499696010181-9c8e59f0bc04", 9 },
        { 28, "https://images.unsplash.com/photo-1529429617124-95b109e86bb8", 9 },
        { 29, "https://images.unsplash.com/photo-1470770841072-f978cf4d019e", 9 },

        { 30, "https://images.unsplash.com/photo-1503264116251-35a269479413", 10 },
        { 31, "https://images.unsplash.com/photo-1441974231531-c6227db76b6e", 10 },
        { 32, "https://images.unsplash.com/photo-1455659817273-f96807779a8d", 10 },
        { 33, "https://images.unsplash.com/photo-1469474968028-56623f02e42e", 10 },

         { 34, "https://images.unsplash.com/photo-1501785888041-af3ef285b470", 11 },
        { 35, "https://images.unsplash.com/photo-1524492449090-1a065f2d7d10", 11 },
        { 36, "https://images.unsplash.com/photo-1522708323590-d24dbb6b0267", 11 },

        { 37, "https://images.unsplash.com/photo-1519817650390-64a93db511aa", 12 },
        { 38, "https://images.unsplash.com/photo-1506748686214-e9df14d4d9d0", 12 },
        { 39, "https://images.unsplash.com/photo-1503264116251-35a269479413", 12 },

        { 40, "https://images.unsplash.com/photo-1507525428034-b723cf961d3e", 13 },
        { 41, "https://images.unsplash.com/photo-1529429617124-95b109e86bb8", 13 },
        { 42, "https://images.unsplash.com/photo-1472214103451-9374bd1c798e", 13 },

        { 43, "https://images.unsplash.com/photo-1455659817273-f96807779a8d", 14 },
        { 44, "https://images.unsplash.com/photo-1469474968028-56623f02e42e", 14 },
        { 45, "https://images.unsplash.com/photo-1493810329807-34bb39a3161f", 14 },
        { 46, "https://images.unsplash.com/photo-1491553895911-0055eca6402d", 14 },

        { 47, "https://images.unsplash.com/photo-1521295121783-8a321d551ad2", 15 },
        { 48, "https://images.unsplash.com/photo-1500530855697-b586d89ba3ee", 15 },
        { 49, "https://images.unsplash.com/photo-1534081333815-ae5019106622", 15 },

        { 50, "https://images.unsplash.com/photo-1499696010181-9c8e59f0bc04", 16 },
        { 51, "https://images.unsplash.com/photo-1470770841072-f978cf4d019e", 16 },
        { 52, "https://images.unsplash.com/photo-1524492449090-1a065f2d7d10", 16 },

        { 53, "https://images.unsplash.com/photo-1506748686214-e9df14d4d9d0", 17 },
        { 54, "https://images.unsplash.com/photo-1522708323590-d24dbb6b0267", 17 },
        { 55, "https://images.unsplash.com/photo-1507525428034-b723cf961d3e", 17 },

        { 56, "https://images.unsplash.com/photo-1503264116251-35a269479413", 18 },
        { 57, "https://images.unsplash.com/photo-1455659817273-f96807779a8d", 18 },
        { 58, "https://images.unsplash.com/photo-1469474968028-56623f02e42e", 18 },

        { 59, "https://images.unsplash.com/photo-1491553895911-0055eca6402d", 19 },
        { 60, "https://images.unsplash.com/photo-1529429617124-95b109e86bb8", 19 },
        { 61, "https://images.unsplash.com/photo-1472214103451-9374bd1c798e", 19 },

        { 62, "https://images.unsplash.com/photo-1524492449090-1a065f2d7d10", 20 },
        { 63, "https://images.unsplash.com/photo-1501785888041-af3ef285b470", 20 },
        { 64, "https://images.unsplash.com/photo-1499696010181-9c8e59f0bc04", 20 },
        { 65, "https://images.unsplash.com/photo-1534081333815-ae5019106622", 20 },

        { 66, "https://images.unsplash.com/photo-1507525428034-b723cf961d3e", 21 },
        { 67, "https://images.unsplash.com/photo-1506748686214-e9df14d4d9d0", 21 },
        { 68, "https://images.unsplash.com/photo-1529429617124-95b109e86bb8", 21 },

        { 69, "https://images.unsplash.com/photo-1469474968028-56623f02e42e", 22 },
        { 70, "https://images.unsplash.com/photo-1470770841072-f978cf4d019e", 22 },
        { 71, "https://images.unsplash.com/photo-1455659817273-f96807779a8d", 22 },

        { 72, "https://images.unsplash.com/photo-1521295121783-8a321d551ad2", 23 },
        { 73, "https://images.unsplash.com/photo-1534081333815-ae5019106622", 23 },
        { 74, "https://images.unsplash.com/photo-1500530855697-b586d89ba3ee", 23 },

        { 75, "https://images.unsplash.com/photo-1503264116251-35a269479413", 24 },
        { 76, "https://images.unsplash.com/photo-1501785888041-af3ef285b470", 24 },
        { 77, "https://images.unsplash.com/photo-1522708323590-d24dbb6b0267", 24 },

        { 78, "https://images.unsplash.com/photo-1472214103451-9374bd1c798e", 25 },
        { 79, "https://images.unsplash.com/photo-1493810329807-34bb39a3161f", 25 },
        { 80, "https://images.unsplash.com/photo-1499696010181-9c8e59f0bc04", 25 },
        { 81, "https://images.unsplash.com/photo-1524492449090-1a065f2d7d10", 25 },

        { 82, "https://images.unsplash.com/photo-1506748686214-e9df14d4d9d0", 26 },
        { 83, "https://images.unsplash.com/photo-1507525428034-b723cf961d3e", 26 },
        { 84, "https://images.unsplash.com/photo-1529429617124-95b109e86bb8", 26 },

        { 85, "https://images.unsplash.com/photo-1501785888041-af3ef285b470", 27 },
        { 86, "https://images.unsplash.com/photo-1503264116251-35a269479413", 27 },
        { 87, "https://images.unsplash.com/photo-1521295121783-8a321d551ad2", 27 },

        { 88, "https://images.unsplash.com/photo-1455659817273-f96807779a8d", 28 },
        { 89, "https://images.unsplash.com/photo-1469474968028-56623f02e42e", 28 },
        { 90, "https://images.unsplash.com/photo-1470770841072-f978cf4d019e", 28 },

        { 91, "https://images.unsplash.com/photo-1534081333815-ae5019106622", 29 },
        { 92, "https://images.unsplash.com/photo-1500530855697-b586d89ba3ee", 29 },
        { 93, "https://images.unsplash.com/photo-1522708323590-d24dbb6b0267", 29 },

        { 94, "https://images.unsplash.com/photo-1499696010181-9c8e59f0bc04", 30 },
        { 95, "https://images.unsplash.com/photo-1472214103451-9374bd1c798e", 30 },
        { 96, "https://images.unsplash.com/photo-1524492449090-1a065f2d7d10", 30 },
        { 97, "https://images.unsplash.com/photo-1493810329807-34bb39a3161f", 30 },

        { 98, "https://images.unsplash.com/photo-1507525428034-b723cf961d3e", 31 },
        { 99, "https://images.unsplash.com/photo-1506748686214-e9df14d4d9d0", 31 },
        { 100, "https://images.unsplash.com/photo-1522708323590-d24dbb6b0267", 31 },

        { 101, "https://images.unsplash.com/photo-1469474968028-56623f02e42e", 32 },
        { 102, "https://images.unsplash.com/photo-1470770841072-f978cf4d019e", 32 },
        { 103, "https://images.unsplash.com/photo-1455659817273-f96807779a8d", 32 },

        { 104, "https://images.unsplash.com/photo-1521295121783-8a321d551ad2", 33 },
        { 105, "https://images.unsplash.com/photo-1534081333815-ae5019106622", 33 },
        { 106, "https://images.unsplash.com/photo-1500530855697-b586d89ba3ee", 33 },

        { 107, "https://images.unsplash.com/photo-1503264116251-35a269479413", 34 },
        { 108, "https://images.unsplash.com/photo-1501785888041-af3ef285b470", 34 },
        { 109, "https://images.unsplash.com/photo-1524492449090-1a065f2d7d10", 34 },

        { 110, "https://images.unsplash.com/photo-1472214103451-9374bd1c798e", 35 },
        { 111, "https://images.unsplash.com/photo-1493810329807-34bb39a3161f", 35 },
        { 112, "https://images.unsplash.com/photo-1499696010181-9c8e59f0bc04", 35 },

        { 113, "https://images.unsplash.com/photo-1522708323590-d24dbb6b0267", 36 },
        { 114, "https://images.unsplash.com/photo-1507525428034-b723cf961d3e", 36 },
        { 115, "https://images.unsplash.com/photo-1506748686214-e9df14d4d9d0", 36 },

        { 116, "https://images.unsplash.com/photo-1529429617124-95b109e86bb8", 37 },
        { 117, "https://images.unsplash.com/photo-1469474968028-56623f02e42e", 37 },
        { 118, "https://images.unsplash.com/photo-1470770841072-f978cf4d019e", 37 },

        { 119, "https://images.unsplash.com/photo-1455659817273-f96807779a8d", 38 },
        { 120, "https://images.unsplash.com/photo-1521295121783-8a321d551ad2", 38 },
        { 121, "https://images.unsplash.com/photo-1534081333815-ae5019106622", 38 },

        { 122, "https://images.unsplash.com/photo-1500530855697-b586d89ba3ee", 39 },
        { 123, "https://images.unsplash.com/photo-1503264116251-35a269479413", 39 },
        { 124, "https://images.unsplash.com/photo-1501785888041-af3ef285b470", 39 },

        { 125, "https://images.unsplash.com/photo-1524492449090-1a065f2d7d10", 40 },
        { 126, "https://images.unsplash.com/photo-1472214103451-9374bd1c798e", 40 },
        { 127, "https://images.unsplash.com/photo-1493810329807-34bb39a3161f", 40 },
        { 128, "https://images.unsplash.com/photo-1499696010181-9c8e59f0bc04", 40 }

            });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM TripImage WHERE TripId BETWEEN 1 AND 50");
        }
    }
}
