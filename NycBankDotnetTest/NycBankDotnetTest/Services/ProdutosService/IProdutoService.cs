using NycBankDotnetTest.DTOS;

namespace NycBankDotnetTest.Services.ProdutosService
{
    public interface IProdutoService
    {
        Task<Produto?> BuscarProdutoPorId(int id);
        Task<List<Produto>?> CadastrarProduto(ProdutoCreateDto request);
        Task<List<Produto>?> EditarProduto(int id, Produto request);
        Task<List<Produto>?> ExcluirProduto(int id);
        Task<List<Produto>> ListarProdutos();
    }
}
