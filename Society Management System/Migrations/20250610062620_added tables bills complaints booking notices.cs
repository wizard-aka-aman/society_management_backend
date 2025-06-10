using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Society_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class addedtablesbillscomplaintsbookingnotices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    BillsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlatIdFlatsId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GeneratedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    PaidDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.BillsId);
                    table.ForeignKey(
                        name: "FK_Bills_Flats_FlatIdFlatsId",
                        column: x => x.FlatIdFlatsId,
                        principalTable: "Flats",
                        principalColumn: "FlatsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlatIdFlatsId = table.Column<int>(type: "int", nullable: false),
                    BookingDate = table.Column<DateOnly>(type: "date", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.BookingsId);
                    table.ForeignKey(
                        name: "FK_Bookings_Flats_FlatIdFlatsId",
                        column: x => x.FlatIdFlatsId,
                        principalTable: "Flats",
                        principalColumn: "FlatsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Complaints",
                columns: table => new
                {
                    ComplaintsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlatIdFlatsId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateResolved = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Complaints", x => x.ComplaintsId);
                    table.ForeignKey(
                        name: "FK_Complaints_Flats_FlatIdFlatsId",
                        column: x => x.FlatIdFlatsId,
                        principalTable: "Flats",
                        principalColumn: "FlatsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notices",
                columns: table => new
                {
                    NoticesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SocietyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notices", x => x.NoticesId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bills_FlatIdFlatsId",
                table: "Bills",
                column: "FlatIdFlatsId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_FlatIdFlatsId",
                table: "Bookings",
                column: "FlatIdFlatsId");

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_FlatIdFlatsId",
                table: "Complaints",
                column: "FlatIdFlatsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Complaints");

            migrationBuilder.DropTable(
                name: "Notices");
        }
    }
}
