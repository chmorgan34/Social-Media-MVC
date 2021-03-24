using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Social_Media_MVC.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Entries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Upvotes = table.Column<int>(type: "int", nullable: false),
                    Downvotes = table.Column<int>(type: "int", nullable: false),
                    VoteScore = table.Column<int>(type: "int", nullable: false),
                    AuthorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntryId = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entries_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Entries_Entries_EntryId",
                        column: x => x.EntryId,
                        principalTable: "Entries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserEntry",
                columns: table => new
                {
                    HiddenById = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    HiddenEntriesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserEntry", x => new { x.HiddenById, x.HiddenEntriesId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserEntry_AspNetUsers_HiddenById",
                        column: x => x.HiddenById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserEntry_Entries_HiddenEntriesId",
                        column: x => x.HiddenEntriesId,
                        principalTable: "Entries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserEntry1",
                columns: table => new
                {
                    SavedById = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SavedEntriesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserEntry1", x => new { x.SavedById, x.SavedEntriesId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserEntry1_AspNetUsers_SavedById",
                        column: x => x.SavedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserEntry1_Entries_SavedEntriesId",
                        column: x => x.SavedEntriesId,
                        principalTable: "Entries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserEntry_HiddenEntriesId",
                table: "ApplicationUserEntry",
                column: "HiddenEntriesId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserEntry1_SavedEntriesId",
                table: "ApplicationUserEntry1",
                column: "SavedEntriesId");

            migrationBuilder.CreateIndex(
                name: "IX_Entries_AuthorId",
                table: "Entries",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Entries_EntryId",
                table: "Entries",
                column: "EntryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserEntry");

            migrationBuilder.DropTable(
                name: "ApplicationUserEntry1");

            migrationBuilder.DropTable(
                name: "Entries");
        }
    }
}
