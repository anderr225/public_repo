using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiCFT.Migrations
{
    public partial class tmpProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TmpProp",
                table: "Projects",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TmpProp",
                table: "Projects");
        }
    }
}
