using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BCPAO.PermitManager.Data.Migrations
{
    public partial class Change01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.EnsureSchema(
                name: "bcpao");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                newSchema: "bcpao");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                newSchema: "bcpao");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                newSchema: "bcpao");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                newSchema: "bcpao");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                newSchema: "bcpao");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                newSchema: "bcpao");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                newSchema: "bcpao");

            migrationBuilder.CreateTable(
                name: "Permits",
                schema: "bcpao",
                columns: table => new
                {
                    PermitId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<string>(nullable: true),
                    DistrictAuthority = table.Column<string>(nullable: true),
                    FinalDate = table.Column<DateTime>(nullable: false),
                    IssueDate = table.Column<DateTime>(nullable: false),
                    ParcelId = table.Column<string>(nullable: true),
                    PermitCode = table.Column<string>(nullable: true),
                    PermitDesc = table.Column<string>(nullable: true),
                    PermitNumber = table.Column<string>(nullable: true),
                    PermitStatus = table.Column<string>(nullable: true),
                    PermitValue = table.Column<decimal>(nullable: false),
                    PropertyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permits", x => x.PermitId);
                });

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "bcpao",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "bcpao",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                schema: "bcpao",
                table: "AspNetUserTokens",
                column: "UserId",
                principalSchema: "bcpao",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                schema: "bcpao",
                table: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Permits",
                schema: "bcpao");

            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                schema: "bcpao",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                schema: "bcpao",
                table: "AspNetRoles");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                schema: "bcpao");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                schema: "bcpao");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                schema: "bcpao");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                schema: "bcpao");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                schema: "bcpao");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                schema: "bcpao");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                schema: "bcpao");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName");
        }
    }
}
