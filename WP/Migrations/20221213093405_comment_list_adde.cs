using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WP.Migrations
{
    /// <inheritdoc />
    public partial class commentlistadde : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    commentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    commentUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ProductID = table.Column<int>(type: "int", nullable: true),
                    commentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    commentString = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.commentID);
                    table.ForeignKey(
                        name: "FK_Comments_AspNetUsers_commentUserId",
                        column: x => x.commentUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comments_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_commentUserId",
                table: "Comments",
                column: "commentUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ProductID",
                table: "Comments",
                column: "ProductID");
        }
    }
}
