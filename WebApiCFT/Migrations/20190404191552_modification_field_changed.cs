using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiCFT.Migrations
{
    public partial class modification_field_changed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ModificationTime",
                table: "Projects",
                newName: "LastModificationTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastModificationTime",
                table: "Projects",
                newName: "ModificationTime");
        }
    }
}
