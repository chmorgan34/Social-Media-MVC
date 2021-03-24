using Microsoft.EntityFrameworkCore.Migrations;

namespace Social_Media_MVC.Data.Migrations
{
    public partial class AddVoting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserEntry_AspNetUsers_HiddenById",
                table: "ApplicationUserEntry");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserEntry_Entries_HiddenEntriesId",
                table: "ApplicationUserEntry");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserEntry1_AspNetUsers_SavedById",
                table: "ApplicationUserEntry1");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserEntry1_Entries_SavedEntriesId",
                table: "ApplicationUserEntry1");

            migrationBuilder.RenameColumn(
                name: "SavedEntriesId",
                table: "ApplicationUserEntry1",
                newName: "HiddenEntriesId");

            migrationBuilder.RenameColumn(
                name: "SavedById",
                table: "ApplicationUserEntry1",
                newName: "HiddenById");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUserEntry1_SavedEntriesId",
                table: "ApplicationUserEntry1",
                newName: "IX_ApplicationUserEntry1_HiddenEntriesId");

            migrationBuilder.RenameColumn(
                name: "HiddenEntriesId",
                table: "ApplicationUserEntry",
                newName: "DownvotedEntriesId");

            migrationBuilder.RenameColumn(
                name: "HiddenById",
                table: "ApplicationUserEntry",
                newName: "DownvotedById");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUserEntry_HiddenEntriesId",
                table: "ApplicationUserEntry",
                newName: "IX_ApplicationUserEntry_DownvotedEntriesId");

            migrationBuilder.CreateTable(
                name: "ApplicationUserEntry2",
                columns: table => new
                {
                    SavedById = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SavedEntriesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserEntry2", x => new { x.SavedById, x.SavedEntriesId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserEntry2_AspNetUsers_SavedById",
                        column: x => x.SavedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserEntry2_Entries_SavedEntriesId",
                        column: x => x.SavedEntriesId,
                        principalTable: "Entries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserEntry3",
                columns: table => new
                {
                    UpvotedById = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UpvotedEntriesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserEntry3", x => new { x.UpvotedById, x.UpvotedEntriesId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserEntry3_AspNetUsers_UpvotedById",
                        column: x => x.UpvotedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserEntry3_Entries_UpvotedEntriesId",
                        column: x => x.UpvotedEntriesId,
                        principalTable: "Entries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserEntry2_SavedEntriesId",
                table: "ApplicationUserEntry2",
                column: "SavedEntriesId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserEntry3_UpvotedEntriesId",
                table: "ApplicationUserEntry3",
                column: "UpvotedEntriesId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserEntry_AspNetUsers_DownvotedById",
                table: "ApplicationUserEntry",
                column: "DownvotedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserEntry_Entries_DownvotedEntriesId",
                table: "ApplicationUserEntry",
                column: "DownvotedEntriesId",
                principalTable: "Entries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserEntry1_AspNetUsers_HiddenById",
                table: "ApplicationUserEntry1",
                column: "HiddenById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserEntry1_Entries_HiddenEntriesId",
                table: "ApplicationUserEntry1",
                column: "HiddenEntriesId",
                principalTable: "Entries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserEntry_AspNetUsers_DownvotedById",
                table: "ApplicationUserEntry");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserEntry_Entries_DownvotedEntriesId",
                table: "ApplicationUserEntry");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserEntry1_AspNetUsers_HiddenById",
                table: "ApplicationUserEntry1");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserEntry1_Entries_HiddenEntriesId",
                table: "ApplicationUserEntry1");

            migrationBuilder.DropTable(
                name: "ApplicationUserEntry2");

            migrationBuilder.DropTable(
                name: "ApplicationUserEntry3");

            migrationBuilder.RenameColumn(
                name: "HiddenEntriesId",
                table: "ApplicationUserEntry1",
                newName: "SavedEntriesId");

            migrationBuilder.RenameColumn(
                name: "HiddenById",
                table: "ApplicationUserEntry1",
                newName: "SavedById");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUserEntry1_HiddenEntriesId",
                table: "ApplicationUserEntry1",
                newName: "IX_ApplicationUserEntry1_SavedEntriesId");

            migrationBuilder.RenameColumn(
                name: "DownvotedEntriesId",
                table: "ApplicationUserEntry",
                newName: "HiddenEntriesId");

            migrationBuilder.RenameColumn(
                name: "DownvotedById",
                table: "ApplicationUserEntry",
                newName: "HiddenById");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUserEntry_DownvotedEntriesId",
                table: "ApplicationUserEntry",
                newName: "IX_ApplicationUserEntry_HiddenEntriesId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserEntry_AspNetUsers_HiddenById",
                table: "ApplicationUserEntry",
                column: "HiddenById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserEntry_Entries_HiddenEntriesId",
                table: "ApplicationUserEntry",
                column: "HiddenEntriesId",
                principalTable: "Entries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserEntry1_AspNetUsers_SavedById",
                table: "ApplicationUserEntry1",
                column: "SavedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserEntry1_Entries_SavedEntriesId",
                table: "ApplicationUserEntry1",
                column: "SavedEntriesId",
                principalTable: "Entries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
