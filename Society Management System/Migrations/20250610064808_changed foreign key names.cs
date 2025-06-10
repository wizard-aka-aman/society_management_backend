using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Society_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class changedforeignkeynames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Flats_FlatIdFlatsId",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Flats_FlatIdFlatsId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Complaints_Flats_FlatIdFlatsId",
                table: "Complaints");

            migrationBuilder.DropForeignKey(
                name: "FK_Flats_Users_AssignedUserIdUsersId",
                table: "Flats");

            migrationBuilder.RenameColumn(
                name: "AssignedUserIdUsersId",
                table: "Flats",
                newName: "UsersId");

            migrationBuilder.RenameIndex(
                name: "IX_Flats_AssignedUserIdUsersId",
                table: "Flats",
                newName: "IX_Flats_UsersId");

            migrationBuilder.RenameColumn(
                name: "FlatIdFlatsId",
                table: "Complaints",
                newName: "FlatsId");

            migrationBuilder.RenameIndex(
                name: "IX_Complaints_FlatIdFlatsId",
                table: "Complaints",
                newName: "IX_Complaints_FlatsId");

            migrationBuilder.RenameColumn(
                name: "FlatIdFlatsId",
                table: "Bookings",
                newName: "FlatsId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_FlatIdFlatsId",
                table: "Bookings",
                newName: "IX_Bookings_FlatsId");

            migrationBuilder.RenameColumn(
                name: "FlatIdFlatsId",
                table: "Bills",
                newName: "FlatsId");

            migrationBuilder.RenameIndex(
                name: "IX_Bills_FlatIdFlatsId",
                table: "Bills",
                newName: "IX_Bills_FlatsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Flats_FlatsId",
                table: "Bills",
                column: "FlatsId",
                principalTable: "Flats",
                principalColumn: "FlatsId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Flats_FlatsId",
                table: "Bookings",
                column: "FlatsId",
                principalTable: "Flats",
                principalColumn: "FlatsId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Complaints_Flats_FlatsId",
                table: "Complaints",
                column: "FlatsId",
                principalTable: "Flats",
                principalColumn: "FlatsId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Flats_Users_UsersId",
                table: "Flats",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "UsersId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Flats_FlatsId",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Flats_FlatsId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Complaints_Flats_FlatsId",
                table: "Complaints");

            migrationBuilder.DropForeignKey(
                name: "FK_Flats_Users_UsersId",
                table: "Flats");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "Flats",
                newName: "AssignedUserIdUsersId");

            migrationBuilder.RenameIndex(
                name: "IX_Flats_UsersId",
                table: "Flats",
                newName: "IX_Flats_AssignedUserIdUsersId");

            migrationBuilder.RenameColumn(
                name: "FlatsId",
                table: "Complaints",
                newName: "FlatIdFlatsId");

            migrationBuilder.RenameIndex(
                name: "IX_Complaints_FlatsId",
                table: "Complaints",
                newName: "IX_Complaints_FlatIdFlatsId");

            migrationBuilder.RenameColumn(
                name: "FlatsId",
                table: "Bookings",
                newName: "FlatIdFlatsId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_FlatsId",
                table: "Bookings",
                newName: "IX_Bookings_FlatIdFlatsId");

            migrationBuilder.RenameColumn(
                name: "FlatsId",
                table: "Bills",
                newName: "FlatIdFlatsId");

            migrationBuilder.RenameIndex(
                name: "IX_Bills_FlatsId",
                table: "Bills",
                newName: "IX_Bills_FlatIdFlatsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Flats_FlatIdFlatsId",
                table: "Bills",
                column: "FlatIdFlatsId",
                principalTable: "Flats",
                principalColumn: "FlatsId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Flats_FlatIdFlatsId",
                table: "Bookings",
                column: "FlatIdFlatsId",
                principalTable: "Flats",
                principalColumn: "FlatsId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Complaints_Flats_FlatIdFlatsId",
                table: "Complaints",
                column: "FlatIdFlatsId",
                principalTable: "Flats",
                principalColumn: "FlatsId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Flats_Users_AssignedUserIdUsersId",
                table: "Flats",
                column: "AssignedUserIdUsersId",
                principalTable: "Users",
                principalColumn: "UsersId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
