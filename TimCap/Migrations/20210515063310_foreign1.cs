using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TimCap.Migrations
{
    public partial class foreign1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Outs",
                table: "Outs");

            migrationBuilder.AlterColumn<int>(
                name: "OutId",
                table: "Outs",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Outs",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Outs",
                table: "Outs",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Outs_OutId",
                table: "Outs",
                column: "OutId");

            migrationBuilder.AddForeignKey(
                name: "FK_Outs_Caps_OutId",
                table: "Outs",
                column: "OutId",
                principalTable: "Caps",
                principalColumn: "CapId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Outs_Caps_OutId",
                table: "Outs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Outs",
                table: "Outs");

            migrationBuilder.DropIndex(
                name: "IX_Outs_OutId",
                table: "Outs");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Outs");

            migrationBuilder.AlterColumn<int>(
                name: "OutId",
                table: "Outs",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Outs",
                table: "Outs",
                column: "OutId");
        }
    }
}
