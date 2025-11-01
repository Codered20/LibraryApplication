using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestApplication.Migrations
{
    /// <inheritdoc />
    public partial class Second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Genre_GenreName",
                table: "Genre");

            migrationBuilder.DropIndex(
                name: "IX_Authors_authorName",
                table: "Authors");

            migrationBuilder.CreateIndex(
                name: "IX_Genre_GenreName",
                table: "Genre",
                column: "GenreName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Authors_authorName",
                table: "Authors",
                column: "authorName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Genre_GenreName",
                table: "Genre");

            migrationBuilder.DropIndex(
                name: "IX_Authors_authorName",
                table: "Authors");

            migrationBuilder.CreateIndex(
                name: "IX_Genre_GenreName",
                table: "Genre",
                column: "GenreName");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_authorName",
                table: "Authors",
                column: "authorName");
        }
    }
}
