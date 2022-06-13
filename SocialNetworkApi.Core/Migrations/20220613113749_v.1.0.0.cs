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
                name: "ApplicationRole",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationRole", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationUser_ApplicationRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "ApplicationRole",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ApplicationRole",
                columns: new[] { "RoleId", "Name" },
                values: new object[] { 1, "Admin" });

            migrationBuilder.InsertData(
                table: "ApplicationRole",
                columns: new[] { "RoleId", "Name" },
                values: new object[] { 2, "User" });

            migrationBuilder.InsertData(
                table: "ApplicationUser",
                columns: new[] { "Id", "Birthday", "Email", "FirstName", "Gender", "LastName", "PasswordHash", "RoleId" },
                values: new object[,]
                {
                    { 1, null, "admin@mail.com", "Admin", null, "Admin", "$2a$11$KW4TFQZ7LFHqTMkdk34Tnu6XRnmfMDdHgJzhgtqvhUmjytp/ZJf2O", 1 },
                    { 2, null, "blowe@mail.com", "Barrett", "MASC", "Lowe", "$2a$11$5t.3J63THAXaXIqVKH62YOBtMw3QGBobHSgLJeZmKBymPEBZEvvje", 2 },
                    { 3, null, "cdisney@mail.com", "Curtis", "MASC", "Disney", "$2a$11$TO6FRJmBL2.8Uho47Ob1R.W8pTlQCu3jsyNmAjFjwZqte0/1XbCjO", 2 },
                    { 4, null, "sowston@mail.com", "Stafford", "MASC", "Owston", "$2a$11$SJqy1S76/jdgfSdBFDfFwuPHQyUZ9cbcMH1Y0ivGM2nATLn4TiQTW", 2 },
                    { 5, null, "klowe@mail.com", "Kira", "FEM", "Lowe", "$2a$11$0I.FQrf9612ykfm9Ed0qI.tt8Is32mWcmabMz58XK4WLeGhYbqJcS", 2 },
                    { 6, null, "khaward@mail.com", "Kira", "FEM", "Haward", "$2a$11$BATO5L1pVodZme3/4vM4VO1C9g0wOhUQr73Q8WjTnCJX1N5alGNym", 2 },
                    { 7, null, "rlowe@mail.com", "Ronda", "FEM", "Lowe", "$2a$11$E5lz7reF0awJLJ9/QqZ4ou2RBFTbKLm2z0XLYBnnvAnrIdFSQ0aNK", 2 },
                    { 8, null, "esweet@mail.com", "Everett", "MASC", "Sweet", "$2a$11$E70Apq5prmS58ndfHtAKsOFf6KJyGN3uBuDATs1XpXj3IkP/84GGW", 2 },
                    { 9, null, "dnicolson@mail.com", "Doyle", "MASC", "Nicolson", "$2a$11$ihLUbWcJVIwlwmfh3emf0.ZXGR2vyA4nDOzG7XuL449sCjAvj.BOC", 2 },
                    { 10, null, "pclaus@mail.com", "Pablo", "MASC", "Claus", "$2a$11$Fb1WlLznUhZekbSvioGeCuMlK7dXUuwJzAaAefIuGbSVzD8vEV1H2", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationRole_Name",
                table: "ApplicationRole",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUser_Email",
                table: "ApplicationUser",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUser_RoleId",
                table: "ApplicationUser",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUser");

            migrationBuilder.DropTable(
                name: "ApplicationRole");
        }
    }
}
