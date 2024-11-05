using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFriendStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Zodiac",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "ZodiacId",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "gender",
                table: "User",
                type: "nchar(10)",
                fixedLength: true,
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "LikeZodiac",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "Friend",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<byte>(
                name: "status",
                table: "Friend",
                type: "tinyint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_ZodiacId",
                table: "User",
                column: "ZodiacId");

            migrationBuilder.CreateIndex(
                name: "IX_LikeZodiac_UserId",
                table: "LikeZodiac",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LikeZodiac_ZodiacLikeId",
                table: "LikeZodiac",
                column: "ZodiacLikeId");

            migrationBuilder.CreateIndex(
                name: "IX_Friend_FriendId",
                table: "Friend",
                column: "FriendId");

            migrationBuilder.CreateIndex(
                name: "IX_Friend_UserId",
                table: "Friend",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Friend_User",
                table: "Friend",
                column: "UserId",
                principalTable: "User",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Friend_User1",
                table: "Friend",
                column: "FriendId",
                principalTable: "User",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_LikeZodiac_User",
                table: "LikeZodiac",
                column: "UserId",
                principalTable: "User",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_LikeZodiac_Zodiac",
                table: "LikeZodiac",
                column: "ZodiacLikeId",
                principalTable: "Zodiac",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Zodiac",
                table: "User",
                column: "ZodiacId",
                principalTable: "Zodiac",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friend_User",
                table: "Friend");

            migrationBuilder.DropForeignKey(
                name: "FK_Friend_User1",
                table: "Friend");

            migrationBuilder.DropForeignKey(
                name: "FK_LikeZodiac_User",
                table: "LikeZodiac");

            migrationBuilder.DropForeignKey(
                name: "FK_LikeZodiac_Zodiac",
                table: "LikeZodiac");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Zodiac",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_ZodiacId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_LikeZodiac_UserId",
                table: "LikeZodiac");

            migrationBuilder.DropIndex(
                name: "IX_LikeZodiac_ZodiacLikeId",
                table: "LikeZodiac");

            migrationBuilder.DropIndex(
                name: "IX_Friend_FriendId",
                table: "Friend");

            migrationBuilder.DropIndex(
                name: "IX_Friend_UserId",
                table: "Friend");

            migrationBuilder.DropColumn(
                name: "gender",
                table: "User");

            migrationBuilder.DropColumn(
                name: "status",
                table: "Friend");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Zodiac",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "ZodiacId",
                table: "User",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "LikeZodiac",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "Friend",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");
        }
    }
}
