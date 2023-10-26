namespace NycBankDotnetTest.Services.ProdutosService
{
    public interface IProdutoService
    {
        Task<Produto?> BuscarProdutoPorId(int id);
        Task<List<Produto>?> CadastrarProduto(Produto produto);
        Task<List<Produto>?> EditarProduto(int id, Produto request);
        Task<List<Produto>?> ExcluirProduto(int id);
        Task<List<Produto>> ListarProdutos();
    }
}
