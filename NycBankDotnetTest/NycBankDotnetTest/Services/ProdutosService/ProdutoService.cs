using Microsoft.EntityFrameworkCore;
using NycBankDotnetTest.Data;

namespace NycBankDotnetTest.Services.ProdutosService
{
    public class ProdutoService : IProdutoService
    {
        private readonly DataContext _context;

        public ProdutoService(DataContext context)
        {
            _context = context;
        }

        public async Task<Produto?> BuscarProdutoPorId(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto is null)
            {
                return null;
            }
            return produto;
        }

        public async Task<List<Produto>?> CadastrarProduto(Produto produto)
        {
            _context.Add(produto);
            await _context.SaveChangesAsync();
            return await _context.Produtos.ToListAsync();
        }

        public async Task<List<Produto>?> EditarProduto(int id, Produto request)
        {
            var produto = await _context.Produtos.FindAsync(id);

            if (produto is null)
            {
                return null;
            }

            produto.Nome = request.Nome;

            produto.Preco = request.Preco;


            await _context.SaveChangesAsync();
            return await _context.Produtos.ToListAsync();

        }

        public async Task<List<Produto>?> ExcluirProduto(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto is null)
                return null;

            _context.Remove(produto);
            await _context.SaveChangesAsync();
            return await _context.Produtos.ToListAsync();
        }

        public async Task<List<Produto>> ListarProdutos()
        {
            var produtos = await _context.Produtos.ToListAsync();
            return produtos;
        }
    }
}
