using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentalCarsApi.Migrations
{
    public partial class seeddata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Categories_CategoryId",
                table: "Cars");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Cars",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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
                    { 1, 1, false, 156000, "Opel Astra" },
                    { 2, 1, false, 230526, "Volvo V70" },
                    { 3, 1, false, 122000, "Opel V90" },
                    { 4, 2, false, 172340, "Opel XC90" },
                    { 5, 1, false, 154794, "Opel XC60" },
                    { 6, 3, false, 254786, "Opel XC40" },
                    { 7, 3, false, 204413, "Chrysler Pacifica" },
                    { 8, 2, false, 11445, "Ferrari Enzo" },
                    { 9, 2, false, 10224, "Lamborghini Aventador" },
                    { 10, 3, false, 12000, "Opel Meriva" }
                });

            migrationBuilder.InsertData(
                table: "Rentals",
                columns: new[] { "Id", "BookingNumber", "CarId", "CustomerBirth", "RentalDays", "RentalPrice", "TimeDateRental", "TimeDateReturn" },
                values: new object[,]
                {
                    { 1, 1000, 1, "1984-07-04-1111", 1, 1000.0, "220401", "220401" },
                    { 2, 2000, 4, "1974-07-04-1112", 1, 2000.0, "220402", "220402" },
                    { 3, 3000, 6, "1964-07-04-1113", 1, 3000.0, "220403", "220403" }
                });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "CarId", "CustomerBirth", "ReservationNumber", "TimeDateRental", "TimeDateReturn" },
                values: new object[,]
                {
                    { 1, 1, "1984-07-04-1111", 1000, "220401", "220401" },
                    { 2, 4, "1974-08-04-1112", 2000, "220402", "220402" },
                    { 3, 6, "1964-09-04-1113", 3000, "220403", "220403" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ReservationNumber",
                table: "Reservations",
                column: "ReservationNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_BookingNumber",
                table: "Rentals",
                column: "BookingNumber",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Categories_CategoryId",
                table: "Cars",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Categories_CategoryId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_ReservationNumber",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Rentals_BookingNumber",
                table: "Rentals");

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Rentals",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Rentals",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Rentals",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Categories_CategoryId",
                table: "Cars",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
