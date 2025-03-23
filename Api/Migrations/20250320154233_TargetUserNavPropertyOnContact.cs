using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class TargetUserNavPropertyOnContact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TargetUserId",
                table: "Contacts",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_TargetUserId",
                table: "Contacts",
                column: "TargetUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Users_TargetUserId",
                table: "Contacts",
                column: "TargetUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Users_TargetUserId",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_TargetUserId",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "TargetUserId",
                table: "Contacts");
        }
    }
}
