using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WP.Migrations
{
    /// <inheritdoc />
    public partial class commentaddedbackagain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_commentUserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Products_ProductID",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_commentUserId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "commentUserId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "ProductID",
                table: "Comments",
                newName: "productID");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_ProductID",
                table: "Comments",
                newName: "IX_Comments_productID");

            migrationBuilder.AlterColumn<int>(
                name: "productID",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Products_productID",
                table: "Comments",
                column: "productID",
                principalTable: "Products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Products_productID",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "productID",
                table: "Comments",
                newName: "ProductID");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_productID",
                table: "Comments",
                newName: "IX_Comments_ProductID");

            migrationBuilder.AlterColumn<int>(
                name: "ProductID",
                table: "Comments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "commentUserId",
                table: "Comments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_commentUserId",
                table: "Comments",
                column: "commentUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_commentUserId",
                table: "Comments",
                column: "commentUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Products_ProductID",
                table: "Comments",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ID");
        }
    }
}
