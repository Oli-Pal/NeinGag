using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class New : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Photos_LikeeId",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "AmountOfLikes",
                table: "Photos");

            migrationBuilder.RenameColumn(
                name: "LikeeId",
                table: "Likes",
                newName: "LikedId");

            migrationBuilder.RenameIndex(
                name: "IX_Likes_LikeeId",
                table: "Likes",
                newName: "IX_Likes_LikedId");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Photos_LikedId",
                table: "Likes",
                column: "LikedId",
                principalTable: "Photos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Photos_LikedId",
                table: "Likes");

            migrationBuilder.RenameColumn(
                name: "LikedId",
                table: "Likes",
                newName: "LikeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Likes_LikedId",
                table: "Likes",
                newName: "IX_Likes_LikeeId");

            migrationBuilder.AddColumn<int>(
                name: "AmountOfLikes",
                table: "Photos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Photos_LikeeId",
                table: "Likes",
                column: "LikeeId",
                principalTable: "Photos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
