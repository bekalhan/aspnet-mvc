using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WP.Migrations
{
    /// <inheritdoc />
    public partial class commentlistadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductID",
                table: "Comments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ProductID",
                table: "Comments",
                column: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Products_ProductID",
                table: "Comments",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Products_ProductID",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_ProductID",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "ProductID",
                table: "Comments");
        }
    }
}
