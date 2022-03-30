using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentalCarsApi.Migrations
{
    public partial class initDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "BookingNumber",
                startValue: 1000L);

            migrationBuilder.CreateSequence<int>(
                name: "ReservationNumber",
                startValue: 1000L);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Prices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BaseDayRental = table.Column<double>(type: "float", nullable: false),
                    KilometerPrice = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Milage = table.Column<int>(type: "int", nullable: false),
                    IsRented = table.Column<bool>(type: "bit", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cars_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Rentals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingNumber = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR BookingNumber"),
                    RentalPrice = table.Column<double>(type: "float", nullable: true),
                    TotalRentalDays = table.Column<int>(type: "int", nullable: true),
                    CustomerBirth = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeDateRental = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeDateReturn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartCarMilage = table.Column<int>(type: "int", nullable: false),
                    EndCarMilage = table.Column<int>(type: "int", nullable: true),
                    NumberOfKilometers = table.Column<int>(type: "int", nullable: true),
                    CarId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rentals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rentals_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservationNumber = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR ReservationNumber"),
                    CustomerBirth = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeDateRental = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeDateReturn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Compact" },
                    { 2, "Premium" },
                    { 3, "Minivan" }
                });

            migrationBuilder.InsertData(
                table: "Prices",
                columns: new[] { "Id", "BaseDayRental", "KilometerPrice", "Name" },
                values: new object[] { 1, 10.0, 1.0, "Standard" });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "CategoryId", "IsRented", "Milage", "Name" },
                values: new object[,]
                {
                    { 1, 1, true, 156000, "Opel Astra" },
                    { 2, 1, true, 230526, "Volvo V70" },
                    { 3, 1, false, 122000, "Volvo V90" },
                    { 4, 2, true, 172340, "Volvo XC90" },
                    { 5, 1, true, 154794, "Volvo XC60" },
                    { 6, 3, true, 254786, "Volvo XC40" },
                    { 7, 3, false, 204413, "Chrysler Pacifica" },
                    { 8, 2, true, 11445, "Ferrari Enzo" },
                    { 9, 2, false, 10224, "Lamborghini Aventador" },
                    { 10, 3, false, 12000, "Opel Meriva" }
                });

            migrationBuilder.InsertData(
                table: "Rentals",
                columns: new[] { "Id", "BookingNumber", "CarId", "CustomerBirth", "EndCarMilage", "NumberOfKilometers", "RentalPrice", "StartCarMilage", "TimeDateRental", "TimeDateReturn", "TotalRentalDays" },
                values: new object[,]
                {
                    { 1, 1000, 2, "1984-07-04-1111", null, null, null, 230526, "220401", "220401", 1 },
                    { 2, 1001, 5, "1974-07-04-1112", null, null, null, 154794, "220402", "220402", 1 },
                    { 3, 1002, 8, "1964-07-04-1113", null, null, null, 11445, "220403", "220403", 1 }
                });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "CarId", "CustomerBirth", "ReservationNumber", "TimeDateRental", "TimeDateReturn" },
                values: new object[,]
                {
                    { 1, 1, "1984-07-04-1111", 1000, "220401", "220401" },
                    { 2, 4, "1974-08-04-1112", 1001, "220402", "220402" },
                    { 3, 6, "1964-09-04-1113", 1002, "220403", "220403" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CategoryId",
                table: "Cars",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_BookingNumber",
                table: "Rentals",
                column: "BookingNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_CarId",
                table: "Rentals",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_CarId",
                table: "Reservations",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ReservationNumber",
                table: "Reservations",
                column: "ReservationNumber",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prices");

            migrationBuilder.DropTable(
                name: "Rentals");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropSequence(
                name: "BookingNumber");

            migrationBuilder.DropSequence(
                name: "ReservationNumber");
        }
    }
}
