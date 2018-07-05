using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ProjectWebAppMock.Migrations
{
    public partial class newIntital : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Workstream_WorkstreamId",
                table: "Projects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Workstream",
                table: "Workstream");

            migrationBuilder.RenameTable(
                name: "Workstream",
                newName: "Workstreams");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Workstreams",
                table: "Workstreams",
                column: "WorkstreamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Workstreams_WorkstreamId",
                table: "Projects",
                column: "WorkstreamId",
                principalTable: "Workstreams",
                principalColumn: "WorkstreamId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Workstreams_WorkstreamId",
                table: "Projects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Workstreams",
                table: "Workstreams");

            migrationBuilder.RenameTable(
                name: "Workstreams",
                newName: "Workstream");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Workstream",
                table: "Workstream",
                column: "WorkstreamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Workstream_WorkstreamId",
                table: "Projects",
                column: "WorkstreamId",
                principalTable: "Workstream",
                principalColumn: "WorkstreamId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
