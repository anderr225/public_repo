using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiCFT.Migrations
{
    public partial class drop_all : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Projects_ProjectId",
                table: "Tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Projects",
                table: "Projects");

            migrationBuilder.RenameTable(
                name: "Tasks",
                newName: "Task");

            migrationBuilder.RenameTable(
                name: "Projects",
                newName: "Project");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_ProjectId",
                table: "Task",
                newName: "IX_Task_ProjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Task",
                table: "Task",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Project",
                table: "Project",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Project",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreationTime", "LastModificationTime" },
                values: new object[] { new DateTime(2019, 4, 9, 8, 49, 1, 203, DateTimeKind.Local).AddTicks(2729), new DateTime(2019, 4, 9, 8, 49, 1, 203, DateTimeKind.Local).AddTicks(7729) });

            migrationBuilder.UpdateData(
                table: "Task",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreationTime", "LastModificationTime" },
                values: new object[] { new DateTime(2019, 4, 9, 8, 49, 1, 204, DateTimeKind.Local).AddTicks(2730), new DateTime(2019, 4, 9, 8, 49, 1, 204, DateTimeKind.Local).AddTicks(2730) });

            migrationBuilder.AddForeignKey(
                name: "FK_Task_Project_ProjectId",
                table: "Task",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Task_Project_ProjectId",
                table: "Task");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Task",
                table: "Task");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Project",
                table: "Project");

            migrationBuilder.RenameTable(
                name: "Task",
                newName: "Tasks");

            migrationBuilder.RenameTable(
                name: "Project",
                newName: "Projects");

            migrationBuilder.RenameIndex(
                name: "IX_Task_ProjectId",
                table: "Tasks",
                newName: "IX_Tasks_ProjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Projects",
                table: "Projects",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreationTime", "LastModificationTime" },
                values: new object[] { new DateTime(2019, 4, 9, 7, 5, 43, 125, DateTimeKind.Local).AddTicks(6966), new DateTime(2019, 4, 9, 7, 5, 43, 126, DateTimeKind.Local).AddTicks(1966) });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreationTime", "LastModificationTime" },
                values: new object[] { new DateTime(2019, 4, 9, 7, 5, 43, 126, DateTimeKind.Local).AddTicks(6967), new DateTime(2019, 4, 9, 7, 5, 43, 126, DateTimeKind.Local).AddTicks(6967) });

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Projects_ProjectId",
                table: "Tasks",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
