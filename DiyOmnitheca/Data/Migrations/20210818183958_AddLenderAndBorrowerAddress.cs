using Microsoft.EntityFrameworkCore.Migrations;

namespace DiyOmnitheca.Data.Migrations
{
    public partial class AddLenderAndBorrowerAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Borrowers");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Lenders",
                newName: "Address");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Borrowers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Borrowers");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Lenders",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Borrowers",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");
        }
    }
}
