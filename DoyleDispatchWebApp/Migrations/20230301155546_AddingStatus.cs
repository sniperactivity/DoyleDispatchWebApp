using Microsoft.EntityFrameworkCore.Migrations;

namespace DoyleDispatchWebApp.Migrations
{
    public partial class AddingStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "Status",
                table: "Packages",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Packages");
        }
    }
}
