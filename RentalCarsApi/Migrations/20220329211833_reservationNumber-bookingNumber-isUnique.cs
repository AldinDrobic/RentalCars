using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentalCarsApi.Migrations
{
    public partial class reservationNumberbookingNumberisUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "BookingNumber",
                startValue: 1000L);

            migrationBuilder.CreateSequence<int>(
                name: "ReservationNumber",
                startValue: 1000L);

            migrationBuilder.AlterColumn<int>(
                name: "ReservationNumber",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValueSql: "NEXT VALUE FOR ReservationNumber",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "TotalRentalDays",
                table: "Rentals",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "RentalPrice",
                table: "Rentals",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "BookingNumber",
                table: "Rentals",
                type: "int",
                nullable: false,
                defaultValueSql: "NEXT VALUE FOR BookingNumber",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "EndCarMilage",
                table: "Rentals",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfKilometers",
                table: "Rentals",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StartCarMilage",
                table: "Rentals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsRented",
                value: true);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsRented",
                value: true);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Volvo V90");

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "IsRented", "Name" },
                values: new object[] { true, "Volvo XC90" });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "IsRented", "Name" },
                values: new object[] { true, "Volvo XC60" });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "IsRented", "Name" },
                values: new object[] { true, "Volvo XC40" });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 8,
                column: "IsRented",
                value: true);

            migrationBuilder.UpdateData(
                table: "Rentals",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CarId", "RentalPrice", "TotalRentalDays" },
                values: new object[] { 2, null, 1 });

            migrationBuilder.UpdateData(
                table: "Rentals",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BookingNumber", "CarId", "RentalPrice", "TotalRentalDays" },
                values: new object[] { 1001, 5, null, 1 });

            migrationBuilder.UpdateData(
                table: "Rentals",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "BookingNumber", "CarId", "RentalPrice", "TotalRentalDays" },
                values: new object[] { 1002, 8, null, 1 });

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 2,
                column: "ReservationNumber",
                value: 1001);

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 3,
                column: "ReservationNumber",
                value: 1002);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropSequence(
                name: "BookingNumber");

            migrationBuilder.DropSequence(
                name: "ReservationNumber");

            migrationBuilder.DropColumn(
                name: "EndCarMilage",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "NumberOfKilometers",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "StartCarMilage",
                table: "Rentals");

            migrationBuilder.AlterColumn<int>(
                name: "ReservationNumber",
                table: "Reservations",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValueSql: "NEXT VALUE FOR ReservationNumber");

            migrationBuilder.AlterColumn<int>(
                name: "TotalRentalDays",
                table: "Rentals",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "RentalPrice",
                table: "Rentals",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BookingNumber",
                table: "Rentals",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValueSql: "NEXT VALUE FOR BookingNumber");

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsRented",
                value: false);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsRented",
                value: false);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Opel V90");

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "IsRented", "Name" },
                values: new object[] { false, "Opel XC90" });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "IsRented", "Name" },
                values: new object[] { false, "Opel XC60" });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "IsRented", "Name" },
                values: new object[] { false, "Opel XC40" });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 8,
                column: "IsRented",
                value: false);

            migrationBuilder.UpdateData(
                table: "Rentals",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CarId", "RentalPrice", "TotalRentalDays" },
                values: new object[] { 1, 1000.0, 0 });

            migrationBuilder.UpdateData(
                table: "Rentals",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BookingNumber", "CarId", "RentalPrice", "TotalRentalDays" },
                values: new object[] { 2000, 4, 2000.0, 0 });

            migrationBuilder.UpdateData(
                table: "Rentals",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "BookingNumber", "CarId", "RentalPrice", "TotalRentalDays" },
                values: new object[] { 3000, 6, 3000.0, 0 });

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 2,
                column: "ReservationNumber",
                value: 2000);

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 3,
                column: "ReservationNumber",
                value: 3000);
        }
    }
}
