using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DevToDev.Infrastructure.Persistence.Migrations
{
    public partial class RemoveUnneededEntityFromIdentityModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RevokedTokens");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RevokedTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccessToken = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    RevocationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RevokedTokens", x => x.Id);
                });
        }
    }
}
