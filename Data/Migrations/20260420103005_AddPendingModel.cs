using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodayJokesApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPendingModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "createdAt",
                table: "Comments",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "content",
                table: "Comments",
                newName: "Content");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Comments",
                newName: "createdAt");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Comments",
                newName: "content");
        }
    }
}
