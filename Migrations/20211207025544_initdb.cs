using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mohajer.Migrations
{
    public partial class initdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Removed = table.Column<DateTime>(nullable: true),
                    IsRemove = table.Column<bool>(nullable: true),
                    ParentCategoryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categores_Categores_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "Categores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Nazarats",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Category = table.Column<int>(nullable: false),
                    Ip = table.Column<string>(nullable: true),
                    AtInserted = table.Column<DateTime>(nullable: false),
                    Removed = table.Column<DateTime>(nullable: true),
                    IsRemove = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nazarats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Full_Name = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    AtInserted = table.Column<DateTime>(nullable: false),
                    Ip = table.Column<string>(nullable: true),
                    Removed = table.Column<DateTime>(nullable: true),
                    IsRemove = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Title = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Tages",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Inserted = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    Removed = table.Column<DateTime>(nullable: true),
                    IsRemove = table.Column<bool>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Password = table.Column<string>(nullable: true),
                    First_Name = table.Column<string>(nullable: true),
                    Last_Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    IsWorkUser = table.Column<bool>(nullable: false),
                    Token = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Prodoctes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Inserted = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    Removed = table.Column<DateTime>(nullable: true),
                    IsRemove = table.Column<bool>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    ImageResized = table.Column<string>(nullable: true),
                    Media = table.Column<string>(nullable: true),
                    CountentType = table.Column<int>(nullable: false),
                    SliderShow = table.Column<bool>(nullable: false),
                    Link = table.Column<string>(nullable: true),
                    Tages = table.Column<string>(nullable: true),
                    UserId = table.Column<long>(nullable: false),
                    CategoryId = table.Column<long>(nullable: false),
                    CategoryId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prodoctes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prodoctes_Categores_CategoryId1",
                        column: x => x.CategoryId1,
                        principalTable: "Categores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Prodoctes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    CommentMsg = table.Column<string>(nullable: true),
                    AnserMsg = table.Column<string>(nullable: true),
                    ProdoctId = table.Column<long>(nullable: false),
                    Removed = table.Column<DateTime>(nullable: true),
                    IsRemove = table.Column<bool>(nullable: true),
                    IsActive = table.Column<bool>(nullable: true),
                    Ip = table.Column<string>(nullable: true),
                    AtInserted = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Prodoctes_ProdoctId",
                        column: x => x.ProdoctId,
                        principalTable: "Prodoctes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categores",
                columns: new[] { "Id", "IsRemove", "ParentCategoryId", "Removed", "Title" },
                values: new object[] { 1, false, null, null, "مذهبی" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "First_Name", "Image", "Inserted", "IsActive", "IsRemove", "IsWorkUser", "Last_Name", "Password", "Phone", "Removed", "Role", "Token", "Updated" },
                values: new object[] { 1L, "kampergig@gmail.com", "Kambiz", "/UploudProfile/avatar.png", new DateTime(2021, 12, 7, 6, 25, 43, 653, DateTimeKind.Local).AddTicks(3880), true, false, false, "zare", "123", "09108496094", null, "Admin", null, new DateTime(2021, 12, 7, 6, 25, 43, 657, DateTimeKind.Local).AddTicks(2723) });

            migrationBuilder.CreateIndex(
                name: "IX_Categores_ParentCategoryId",
                table: "Categores",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ProdoctId",
                table: "Comments",
                column: "ProdoctId");

            migrationBuilder.CreateIndex(
                name: "IX_Prodoctes_CategoryId1",
                table: "Prodoctes",
                column: "CategoryId1");

            migrationBuilder.CreateIndex(
                name: "IX_Prodoctes_UserId",
                table: "Prodoctes",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Nazarats");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "Tages");

            migrationBuilder.DropTable(
                name: "Prodoctes");

            migrationBuilder.DropTable(
                name: "Categores");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
