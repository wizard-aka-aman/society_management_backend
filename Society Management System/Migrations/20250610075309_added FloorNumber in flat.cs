using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Society_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class addedFloorNumberinflat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FloorNumber",
                table: "Flats",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FloorNumber",
                table: "Flats");
        }
    }
}
