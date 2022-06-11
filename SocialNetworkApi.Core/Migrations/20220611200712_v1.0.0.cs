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
                values: new object[,]
                {
                    { 1, null, "admin@mail.com", "Admin", null, "Admin", "$2a$11$DANETUu7Ygt5FdeKsYKHe.7ZJdV.tCNrakdypwkiTqzxPxOEzpGQK", 1 },
                    { 2, null, "blowe@mail.com", "Barrett", "MASC", "Lowe", "$2a$11$ULBWj1lVeB0WqT3pfEXqeOf5zlvC4TkLH/IyvcmPYGGDdbX0IT/UC", 2 },
                    { 3, null, "cdisney@mail.com", "Curtis", "MASC", "Disney", "$2a$11$C7gKRmurmSbZ8mOPTxOl5ul3RwtENueoM0ocx6lz9vISjNZNjGt0.", 2 },
                    { 4, null, "sowston@mail.com", "Stafford", "MASC", "Owston", "$2a$11$W4lpMnJU7FUS2nLi6OF9LuU0mlh0ekAKGnelb/bqPHATCoGMpSyD.", 2 },
                    { 5, null, "klowe@mail.com", "Kira", "FEM", "Lowe", "$2a$11$UB6B3c2iNJpqrkFTvb2fu./UFTJHcN07qhv/QbntQyQ2AnytIgDQC", 2 },
                    { 6, null, "khaward@mail.com", "Kira", "FEM", "Haward", "$2a$11$YGdIJnhZXbVsCJZoK3MLyOZH9Yeh0HvVqMw3UUVuf44eJ0B50o2bS", 2 },
                    { 7, null, "rlowe@mail.com", "Ronda", "FEM", "Lowe", "$2a$11$Uu.oY5pj5qjPlKlWQtlj2u8d8nPnT2I4HNPEZHNDyp2owed3LdOWq", 2 },
                    { 8, null, "esweet@mail.com", "Everett", "MASC", "Sweet", "$2a$11$wK5QWbD4eyR/Ca2VCgnvAOmpzl0Mjj8OLMRRynb7df7BC5M8.uWQm", 2 },
                    { 9, null, "dnicolson@mail.com", "Doyle", "MASC", "Nicolson", "$2a$11$D9Ku.2LFIAXd6WW3Cq1WRupjEFKpuTt/wbL.2zUPE0T3OOXmSMaCe", 2 },
                    { 10, null, "pclaus@mail.com", "Pablo", "MASC", "Claus", "$2a$11$Yazv0zi/vZPV7Jwy2ZO9oOE7kniPBaDWaDKDlDNj3z08EKCd7FJwq", 2 }
                });

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
