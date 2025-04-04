using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Change_TicketCategory_To_SingleKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TicketCategories",
                table: "TicketCategories");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "TicketCategories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_TicketCategories",
                table: "TicketCategories",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TicketCategories_TicketId_CategoryId",
                table: "TicketCategories",
                columns: new[] { "TicketId", "CategoryId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TicketCategories",
                table: "TicketCategories");

            migrationBuilder.DropIndex(
                name: "IX_TicketCategories_TicketId_CategoryId",
                table: "TicketCategories");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TicketCategories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TicketCategories",
                table: "TicketCategories",
                columns: new[] { "TicketId", "CategoryId" });
        }
    }
}
