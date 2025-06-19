using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Society_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class addedpropertybiilsalarmstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BillsId",
                table: "Alarms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Alarms_BillsId",
                table: "Alarms",
                column: "BillsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alarms_Bills_BillsId",
                table: "Alarms",
                column: "BillsId",
                principalTable: "Bills",
                principalColumn: "BillsId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alarms_Bills_BillsId",
                table: "Alarms");

            migrationBuilder.DropIndex(
                name: "IX_Alarms_BillsId",
                table: "Alarms");

            migrationBuilder.DropColumn(
                name: "BillsId",
                table: "Alarms");
        }
    }
}
