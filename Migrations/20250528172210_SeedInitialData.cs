using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace palantir00CW9s30320.Migrations
{
    public partial class SeedInitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "IdDoctor", "Email", "FirstName", "LastName" },
                values: new object[] { 1, "anna.nowak@przyklad.pl", "Anna", "Nowak" });

            migrationBuilder.InsertData(
                table: "Medicaments",
                columns: new[] { "IdMedicament", "Description", "Name", "Price" },
                values: new object[] { 1, "Przeciwbólowy", "Ibuprofen", 5m });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "IdPatient", "BirthDate", "FirstName", "LastName" },
                values: new object[] { 1, new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jan", "Kowalski" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "IdDoctor",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Medicaments",
                keyColumn: "IdMedicament",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "IdPatient",
                keyValue: 1);
        }
    }
}
