using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PoisnFang.Todo.Server.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "z_TodoUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedById = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedById = table.Column<int>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false),
                    DeletedById = table.Column<int>(nullable: true),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    AppUserId = table.Column<int>(nullable: false),
                    AspNetUserId = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    DisplayName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: false),
                    NormalizedEmail = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_z_TodoUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "z_TodoLists",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedById = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedById = table.Column<int>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false),
                    DeletedById = table.Column<int>(nullable: true),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    SiteId = table.Column<int>(nullable: false),
                    TodoUserId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_z_TodoLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_z_TodoLists_z_TodoUsers_TodoUserId",
                        column: x => x.TodoUserId,
                        principalTable: "z_TodoUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "z_TodoTasks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedById = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedById = table.Column<int>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false),
                    DeletedById = table.Column<int>(nullable: true),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    TodoListId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    DueDate = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    IsImportant = table.Column<bool>(nullable: false),
                    IsCompeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_z_TodoTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_z_TodoTasks_z_TodoLists_TodoListId",
                        column: x => x.TodoListId,
                        principalTable: "z_TodoLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "z_Steps",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedById = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedById = table.Column<int>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false),
                    DeletedById = table.Column<int>(nullable: true),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    TodoTaskId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    IsCompeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_z_Steps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_z_Steps_z_TodoTasks_TodoTaskId",
                        column: x => x.TodoTaskId,
                        principalTable: "z_TodoTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_z_Steps_TodoTaskId",
                table: "z_Steps",
                column: "TodoTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_z_TodoLists_TodoUserId",
                table: "z_TodoLists",
                column: "TodoUserId");

            migrationBuilder.CreateIndex(
                name: "IX_z_TodoTasks_TodoListId",
                table: "z_TodoTasks",
                column: "TodoListId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "z_Steps");

            migrationBuilder.DropTable(
                name: "z_TodoTasks");

            migrationBuilder.DropTable(
                name: "z_TodoLists");

            migrationBuilder.DropTable(
                name: "z_TodoUsers");
        }
    }
}
