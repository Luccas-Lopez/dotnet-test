namespace NycBankDotnetTest.DTOS
{
    public record struct ProdutoCreateDto(
        string Nome,
        double Preco,
        List<Categoria> Categorias
        );
}
