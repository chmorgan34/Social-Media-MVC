using Microsoft.EntityFrameworkCore.Migrations;

namespace Social_Media_MVC.Data.Migrations
{
    public partial class AddCommentReferences : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entries_Entries_EntryId",
                table: "Entries");

            migrationBuilder.RenameColumn(
                name: "EntryId",
                table: "Entries",
                newName: "RepliedToId");

            migrationBuilder.RenameIndex(
                name: "IX_Entries_EntryId",
                table: "Entries",
                newName: "IX_Entries_RepliedToId");

            migrationBuilder.AddColumn<int>(
                name: "PostId",
                table: "Entries",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Entries_PostId",
                table: "Entries",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Entries_Entries_PostId",
                table: "Entries",
                column: "PostId",
                principalTable: "Entries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Entries_Entries_RepliedToId",
                table: "Entries",
                column: "RepliedToId",
                principalTable: "Entries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entries_Entries_PostId",
                table: "Entries");

            migrationBuilder.DropForeignKey(
                name: "FK_Entries_Entries_RepliedToId",
                table: "Entries");

            migrationBuilder.DropIndex(
                name: "IX_Entries_PostId",
                table: "Entries");

            migrationBuilder.DropColumn(
                name: "PostId",
                table: "Entries");

            migrationBuilder.RenameColumn(
                name: "RepliedToId",
                table: "Entries",
                newName: "EntryId");

            migrationBuilder.RenameIndex(
                name: "IX_Entries_RepliedToId",
                table: "Entries",
                newName: "IX_Entries_EntryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Entries_Entries_EntryId",
                table: "Entries",
                column: "EntryId",
                principalTable: "Entries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
