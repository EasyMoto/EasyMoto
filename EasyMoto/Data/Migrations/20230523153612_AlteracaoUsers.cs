using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyMoto.Data.Migrations
{
    public partial class AlteracaoUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CodigoPostal",
                table: "Utilizadores",
                newName: "CodPostal");

            migrationBuilder.AlterColumn<string>(
                name: "Telemovel",
                table: "Utilizadores",
                type: "nvarchar(9)",
                maxLength: 9,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "NIF",
                table: "Utilizadores",
                type: "nvarchar(9)",
                maxLength: 9,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CodPostal",
                table: "Utilizadores",
                newName: "CodigoPostal");

            migrationBuilder.AlterColumn<string>(
                name: "Telemovel",
                table: "Utilizadores",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(9)",
                oldMaxLength: 9);

            migrationBuilder.AlterColumn<string>(
                name: "NIF",
                table: "Utilizadores",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(9)",
                oldMaxLength: 9);
        }
    }
}
