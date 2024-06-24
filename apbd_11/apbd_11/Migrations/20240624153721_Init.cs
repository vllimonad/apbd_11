using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace apbd_11.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefreshTokenExp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Login", "Password", "RefreshToken", "RefreshTokenExp", "Salt" },
                values: new object[,]
                {
                    { 1, "wiun2@sdc.dc", "qqq", "1234", "", new DateTime(2024, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "" },
                    { 2, "ounfde@iec.cf", "www", "12345", "", new DateTime(2024, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "" },
                    { 3, "083nf@4pfr.5ty", "eee", "12346", "", new DateTime(2024, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
