using Microsoft.EntityFrameworkCore.Migrations;

namespace Example.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleKey = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleKey);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    HashedPassword = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Salt = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Surname = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserName);
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    UserName = table.Column<string>(type: "TEXT", nullable: false),
                    RoleKey = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => new { x.UserName, x.RoleKey });
                    table.ForeignKey(
                        name: "FK_Subscriptions_Roles_RoleKey",
                        column: x => x.RoleKey,
                        principalTable: "Roles",
                        principalColumn: "RoleKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Subscriptions_Users_UserName",
                        column: x => x.UserName,
                        principalTable: "Users",
                        principalColumn: "UserName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleKey", "Description" },
                values: new object[] { "ADMIN", "User with the highest privileges." });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleKey", "Description" },
                values: new object[] { "USER", "User with read/write privileges." });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleKey", "Description" },
                values: new object[] { "GUEST", "User with only read privileges" });

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_RoleKey",
                table: "Subscriptions",
                column: "RoleKey");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
