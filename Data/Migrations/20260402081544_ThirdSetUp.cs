using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodayJokesApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class ThirdSetUp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Profile",
                table: "Jokes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "user",
                table: "Jokes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Profile",
                table: "Jokes");

            migrationBuilder.DropColumn(
                name: "user",
                table: "Jokes");
        }
    }
}
