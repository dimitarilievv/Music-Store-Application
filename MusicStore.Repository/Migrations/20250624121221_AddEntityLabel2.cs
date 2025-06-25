using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicStore.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddEntityLabel2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Albums_Label_LabelId",
                table: "Albums");

            migrationBuilder.DropForeignKey(
                name: "FK_Artists_Label_LabelId",
                table: "Artists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Label",
                table: "Label");

            migrationBuilder.RenameTable(
                name: "Label",
                newName: "Labels");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Labels",
                table: "Labels",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_Labels_LabelId",
                table: "Albums",
                column: "LabelId",
                principalTable: "Labels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Artists_Labels_LabelId",
                table: "Artists",
                column: "LabelId",
                principalTable: "Labels",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Albums_Labels_LabelId",
                table: "Albums");

            migrationBuilder.DropForeignKey(
                name: "FK_Artists_Labels_LabelId",
                table: "Artists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Labels",
                table: "Labels");

            migrationBuilder.RenameTable(
                name: "Labels",
                newName: "Label");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Label",
                table: "Label",
                column: "Id");

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
    }
}
