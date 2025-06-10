using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Society_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class AddedtableUsersandFlats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UsersId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UsersId);
                });

            migrationBuilder.CreateTable(
                name: "Flats",
                columns: table => new
                {
                    FlatsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Block = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FlatNumber = table.Column<int>(type: "int", nullable: false),
                    AssignedUserIdUsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flats", x => x.FlatsId);
                    table.ForeignKey(
                        name: "FK_Flats_Users_AssignedUserIdUsersId",
                        column: x => x.AssignedUserIdUsersId,
                        principalTable: "Users",
                        principalColumn: "UsersId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flats_AssignedUserIdUsersId",
                table: "Flats",
                column: "AssignedUserIdUsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Flats");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
