using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicStore.Repository.Migrations
{
    /// <inheritdoc />
    public partial class ChangesAlbum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApiId",
                table: "Albums",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApiId",
                table: "Albums");
        }
    }
}
