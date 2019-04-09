using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiCFT.Migrations
{
    public partial class noTmpProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TmpProp",
                table: "Projects");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TmpProp",
                table: "Projects",
                nullable: false,
                defaultValue: 0);
        }
    }
}
