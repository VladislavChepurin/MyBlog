using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBlog.Migrations
{
    public partial class migration4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentTable_ArticleTable_ArticleId1",
                table: "CommentTable");

            migrationBuilder.DropIndex(
                name: "IX_CommentTable_ArticleId1",
                table: "CommentTable");

            migrationBuilder.DropColumn(
                name: "ArticleId1",
                table: "CommentTable");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "TegTable",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "ArticleId",
                table: "CommentTable",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "CommentTable",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "TegsId",
                table: "ArticleTeg",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "ArticlesId",
                table: "ArticleTeg",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ArticleTable",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.CreateIndex(
                name: "IX_CommentTable_ArticleId",
                table: "CommentTable",
                column: "ArticleId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentTable_ArticleTable_ArticleId",
                table: "CommentTable",
                column: "ArticleId",
                principalTable: "ArticleTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentTable_ArticleTable_ArticleId",
                table: "CommentTable");

            migrationBuilder.DropIndex(
                name: "IX_CommentTable_ArticleId",
                table: "CommentTable");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "TegTable",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<string>(
                name: "ArticleId",
                table: "CommentTable",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "CommentTable",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<Guid>(
                name: "ArticleId1",
                table: "CommentTable",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TegsId",
                table: "ArticleTeg",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<Guid>(
                name: "ArticlesId",
                table: "ArticleTeg",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "ArticleTable",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.CreateIndex(
                name: "IX_CommentTable_ArticleId1",
                table: "CommentTable",
                column: "ArticleId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentTable_ArticleTable_ArticleId1",
                table: "CommentTable",
                column: "ArticleId1",
                principalTable: "ArticleTable",
                principalColumn: "Id");
        }
    }
}
