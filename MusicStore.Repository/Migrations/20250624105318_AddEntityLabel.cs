using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicStore.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddEntityLabel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LabelId",
                table: "Artists",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LabelId",
                table: "Albums",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Label",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FoundedYear = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Label", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Artists_LabelId",
                table: "Artists",
                column: "LabelId");

            migrationBuilder.CreateIndex(
                name: "IX_Albums_LabelId",
                table: "Albums",
                column: "LabelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_Label_LabelId",
                table: "Albums",
                column: "LabelId",
                principalTable: "Label",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Artists_Label_LabelId",
                table: "Artists",
                column: "LabelId",
                principalTable: "Label",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Albums_Label_LabelId",
                table: "Albums");

            migrationBuilder.DropForeignKey(
                name: "FK_Artists_Label_LabelId",
                table: "Artists");

            migrationBuilder.DropTable(
                name: "Label");

            migrationBuilder.DropIndex(
                name: "IX_Artists_LabelId",
                table: "Artists");

            migrationBuilder.DropIndex(
                name: "IX_Albums_LabelId",
                table: "Albums");

            migrationBuilder.DropColumn(
                name: "LabelId",
                table: "Artists");

            migrationBuilder.DropColumn(
                name: "LabelId",
                table: "Albums");
        }
    }
}
