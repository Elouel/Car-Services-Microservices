using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarService.JobScheduler.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedByEmployeeId = table.Column<int>(nullable: false),
                    CreateByEmployeeName = table.Column<string>(nullable: true),
                    AssignedEmployeeId = table.Column<int>(nullable: true),
                    AssignedEmployeeName = table.Column<string>(nullable: true),
                    JobStatus = table.Column<int>(nullable: false),
                    DeadLine = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    StratedDate = table.Column<DateTime>(nullable: true),
                    FinishedDate = table.Column<DateTime>(nullable: true),
                    DepartmentId = table.Column<int>(nullable: false),
                    VehicleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PurhcasedServices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobId = table.Column<int>(nullable: false),
                    ServiceId = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurhcasedServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurhcasedServices_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PurhcasedServices_JobId",
                table: "PurhcasedServices",
                column: "JobId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PurhcasedServices");

            migrationBuilder.DropTable(
                name: "Jobs");
        }
    }
}
