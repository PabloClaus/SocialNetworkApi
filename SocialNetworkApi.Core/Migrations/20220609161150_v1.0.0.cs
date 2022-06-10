using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialNetworkApi.Core.Migrations
{
    public partial class v100 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationRol",
                columns: table => new
                {
                    RolId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationRol", x => x.RolId);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RolId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationUser_ApplicationRol_RolId",
                        column: x => x.RolId,
                        principalTable: "ApplicationRol",
                        principalColumn: "RolId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ApplicationRol",
                columns: new[] { "RolId", "Name" },
                values: new object[] { 1, "Admin" });

            migrationBuilder.InsertData(
                table: "ApplicationRol",
                columns: new[] { "RolId", "Name" },
                values: new object[] { 2, "User" });

            migrationBuilder.InsertData(
                table: "ApplicationUser",
                columns: new[] { "Id", "Birthday", "Email", "FirstName", "Gender", "LastName", "PasswordHash", "RolId" },
                values: new object[] { 1, null, "admin@admin.com", "Admin", null, "Admin", "$2a$11$biVc.Av.hFNDSrrQFTHuw.HOPZa2JvnA0Vb3NY0wdH1BezJkOyIXm", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUser_RolId",
                table: "ApplicationUser",
                column: "RolId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUser");

            migrationBuilder.DropTable(
                name: "ApplicationRol");
        }
    }
}
