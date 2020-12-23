using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class Dislikes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DisLikes",
                columns: table => new
                {
                    DisLikerId = table.Column<int>(type: "INTEGER", nullable: false),
                    DisLikedId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisLikes", x => new { x.DisLikerId, x.DisLikedId });
                    table.ForeignKey(
                        name: "FK_DisLikes_Photos_DisLikedId",
                        column: x => x.DisLikedId,
                        principalTable: "Photos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DisLikes_Users_DisLikerId",
                        column: x => x.DisLikerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DisLikes_DisLikedId",
                table: "DisLikes",
                column: "DisLikedId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DisLikes");
        }
    }
}
