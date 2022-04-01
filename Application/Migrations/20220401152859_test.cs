using Microsoft.EntityFrameworkCore.Migrations;

namespace Application.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "Country",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Country_PersonId",
                table: "Country",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Country_Persons_PersonId",
                table: "Country",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Country_Persons_PersonId",
                table: "Country");

            migrationBuilder.DropIndex(
                name: "IX_Country_PersonId",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "Country");
        }
    }
}
