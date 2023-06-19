using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyMoto.Data.Migrations
{
    public partial class RegistoNovoUtilizador : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Utilizadores",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Utilizadores");
        }
    }
}
