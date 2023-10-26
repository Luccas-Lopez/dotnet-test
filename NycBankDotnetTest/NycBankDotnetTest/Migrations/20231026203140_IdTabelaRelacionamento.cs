using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NycBankDotnetTest.Migrations
{
    /// <inheritdoc />
    public partial class IdTabelaRelacionamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoriaProduto",
                table: "CategoriaProduto");

            migrationBuilder.DropColumn(
                name: "CategoriaProdutoId",
                table: "CategoriaProduto"
            );

            migrationBuilder.AddColumn<int>(
                name: "CategoriaProdutoId",
                table: "CategoriaProduto",
                type: "int",
                nullable: false).Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoriaProduto",
                table: "CategoriaProduto",
                column: "CategoriaProdutoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
