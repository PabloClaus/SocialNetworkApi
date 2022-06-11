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
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    { 1, null, "admin@mail.com", "Admin", null, "Admin", "$2a$11$L24W9Z4GOi1EP4.jdmEy/uCi0c/RYPHadaosM8V4pMsn8mL6FulCe", 1 },
                    { 2, null, "blowe@mail.com", "Barrett", "MASC", "Lowe", "$2a$11$w5UVe1vts0adJvQ4mlQYS.51GrrYSC8Bkvq1iCh/BHVWZhy/DWpE6", 2 },
                    { 3, null, "cdisney@mail.com", "Curtis", "MASC", "Disney", "$2a$11$Y2DpH/LMMLK598cnPxu2kO95peKXIFIvpEzktwK8Wf/Nyvmh0Bskq", 2 },
                    { 4, null, "sowston@mail.com", "Stafford", "MASC", "Owston", "$2a$11$jdLtz.u2PF0JAwanF1O9Kei5FtCtLPh61km0g7C5pSf3hsrwm/Ff.", 2 },
                    { 5, null, "klowe@mail.com", "Kira", "FEM", "Lowe", "$2a$11$dUQznXUI/RGI3WH6zOKVlu8j8OGXwpcHHMDT9LUzSnFwJhwKdI8ci", 2 },
                    { 6, null, "khaward@mail.com", "Kira", "FEM", "Haward", "$2a$11$DgKMw45peEpj2Ej9QlUjPuTzv1Bo1UWyQ4zg883udwELyZAu3DxzC", 2 },
                    { 7, null, "rlowe@mail.com", "Ronda", "FEM", "Lowe", "$2a$11$pcKPJnix6UYra4RHJh2GXufg7LyMh2tuOQAzAu6Knxj/vIb5M9GaC", 2 },
                    { 8, null, "esweet@mail.com", "Everett", "MASC", "Sweet", "$2a$11$FJ2Jxh/p0puKkA/SbVZur.G6Y6t54/pMWiG5wfU0uqG2YEbDU5UlO", 2 },
                    { 9, null, "dnicolson@mail.com", "Doyle", "MASC", "Nicolson", "$2a$11$EZsCROPtRlxdoGz742S4POHBMqsbZ72Anpl62VE0oB1GlI4nh5ju2", 2 },
                    { 10, null, "pclaus@mail.com", "Pablo", "MASC", "Claus", "$2a$11$d8Q5N2FmIbsrb/k2OEMvDeqOtLRHoeoUTr7XS0NUVfBkYMDQmX0G2", 2 }
                });

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
