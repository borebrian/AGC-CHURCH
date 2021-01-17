using Microsoft.EntityFrameworkCore.Migrations;

namespace AGC_CHURCH.Migrations
{
    public partial class V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "agpoCategorie",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 50, nullable: false),
                    strAgpo = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_agpoCategorie", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 50, nullable: false),
                    Branc = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Gender",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 50, nullable: false),
                    strGender = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gender", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 50, nullable: false),
                    Notification = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    Timestamp = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Phone = table.Column<string>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: false),
                    provider = table.Column<string>(nullable: false),
                    Role = table.Column<string>(nullable: false),
                    Gender = table.Column<string>(nullable: false),
                    Category = table.Column<string>(nullable: false),
                    ChurchBranch = table.Column<string>(nullable: false),
                    BabtismStatus = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "agpoCategorie");

            migrationBuilder.DropTable(
                name: "Branches");

            migrationBuilder.DropTable(
                name: "Gender");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
