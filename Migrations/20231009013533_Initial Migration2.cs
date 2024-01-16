using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Emp.Migrations
{
    public partial class InitialMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmpId",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Attendance",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PeriodId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmpId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeOut = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendance", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimePeriod",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Totalhours = table.Column<int>(type: "int", nullable: false),
                    BaseSalary = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salary = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deductions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NetSalary = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimePeriod", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attendance");

            migrationBuilder.DropTable(
                name: "TimePeriod");

            migrationBuilder.DropColumn(
                name: "EmpId",
                table: "Employees");
        }
    }
}
