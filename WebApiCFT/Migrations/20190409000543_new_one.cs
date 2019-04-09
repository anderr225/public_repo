using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiCFT.Migrations
{
    public partial class new_one : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "CreationTime", "Description", "LastModificationTime", "Name" },
                values: new object[] { 1, new DateTime(2019, 4, 9, 7, 5, 43, 125, DateTimeKind.Local).AddTicks(6966), "Default_project_description", new DateTime(2019, 4, 9, 7, 5, 43, 126, DateTimeKind.Local).AddTicks(1966), "Default_project_name" });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "CreationTime", "Description", "LastModificationTime", "Name", "Priority", "ProjectId", "Status" },
                values: new object[] { 1, new DateTime(2019, 4, 9, 7, 5, 43, 126, DateTimeKind.Local).AddTicks(6967), "Default_task_description", new DateTime(2019, 4, 9, 7, 5, 43, 126, DateTimeKind.Local).AddTicks(6967), "Default_task_name", 1, 1, 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
