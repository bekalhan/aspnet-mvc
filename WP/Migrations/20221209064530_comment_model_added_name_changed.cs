using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WP.Migrations
{
    /// <inheritdoc />
    public partial class commentmodeladdednamechanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comments_AspNetUsers_commentUserId",
                table: "comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_comments",
                table: "comments");

            migrationBuilder.RenameTable(
                name: "comments",
                newName: "Comments");

            migrationBuilder.RenameIndex(
                name: "IX_comments_commentUserId",
                table: "Comments",
                newName: "IX_Comments_commentUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comments",
                table: "Comments",
                column: "commentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_commentUserId",
                table: "Comments",
                column: "commentUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_commentUserId",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comments",
                table: "Comments");

            migrationBuilder.RenameTable(
                name: "Comments",
                newName: "comments");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_commentUserId",
                table: "comments",
                newName: "IX_comments_commentUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_comments",
                table: "comments",
                column: "commentID");

            migrationBuilder.AddForeignKey(
                name: "FK_comments_AspNetUsers_commentUserId",
                table: "comments",
                column: "commentUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
