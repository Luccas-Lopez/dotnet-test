using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NycBankDotnetTest.Migrations
{
    /// <inheritdoc />
    public partial class ProdutosCategoriasNulo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoriaProduto",
                table: "CategoriaProduto");

            migrationBuilder.AddColumn<int>(
                name: "CategoriaProdutoId",
                table: "CategoriaProduto",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoriaProduto",
                table: "CategoriaProduto",
                column: "CategoriaProdutoId");

            migrationBuilder.AlterColumn<int>(
                name: "CategoriasId",
                table: "CategoriaProduto",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ProdutosId",
                table: "CategoriaProduto",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
