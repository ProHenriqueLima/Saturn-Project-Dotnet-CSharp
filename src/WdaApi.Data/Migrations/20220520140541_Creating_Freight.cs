using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WdaApi.Data.Migrations
{
    public partial class Creating_Freight : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProfilePermissions");

            migrationBuilder.DropTable(
                name: "permissions");

            migrationBuilder.CreateTable(
                name: "freights_status",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_freights_status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "freights",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateAt = table.Column<DateTime>(nullable: false),
                    UpdateAt = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    DateInit = table.Column<DateTime>(nullable: false),
                    DateEnd = table.Column<DateTime>(nullable: false),
                    IsTrackedTruck = table.Column<string>(type: "varchar(10)", nullable: false),
                    IsSafeTruck = table.Column<string>(type: "varchar(10)", nullable: false),
                    Weight = table.Column<double>(type: "double", nullable: false),
                    Observations = table.Column<string>(type: "varchar(256)", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    FreightStatusId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_freights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_freights_freights_status_FreightStatusId",
                        column: x => x.FreightStatusId,
                        principalTable: "freights_status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_freights_FreightStatusId",
                table: "freights",
                column: "FreightStatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "freights");

            migrationBuilder.DropTable(
                name: "freights_status");

            migrationBuilder.CreateTable(
                name: "permissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Description = table.Column<string>(type: "varchar(255)", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProfilePermissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    PermissionId = table.Column<Guid>(type: "char(36)", nullable: false),
                    ProfileCustomerId = table.Column<Guid>(type: "char(36)", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfilePermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfilePermissions_permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfilePermissions_profiles_ProfileCustomerId",
                        column: x => x.ProfileCustomerId,
                        principalTable: "profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProfilePermissions_PermissionId",
                table: "ProfilePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfilePermissions_ProfileCustomerId",
                table: "ProfilePermissions",
                column: "ProfileCustomerId");
        }
    }
}
