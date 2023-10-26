using Microsoft.EntityFrameworkCore;
using NycBankDotnetTest.Data;
using NycBankDotnetTest.DTOS;
using NycBankDotnetTest.Models;

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
            var produto = await _context.Produtos
                .Include(p => p.Categorias)
                .FirstOrDefaultAsync(p => p.Id == id);


            if (produto is null)
            {
                return null;
            }
            return produto;
        }

        public async Task<List<Produto>?> CadastrarProduto(ProdutoCreateDto request)
        {
            var novoProduto = new Produto
            {
                Nome = request.Nome,
                Preco = request.Preco,
                Categorias = new List<Categoria>()
            };

            foreach (var categoriaDto in request.Categorias)
            {
                var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.Nome == categoriaDto.Nome);

                if (categoria == null)
                {
                    categoria = new Categoria { Nome = categoriaDto.Nome };
                    _context.Categorias.Add(categoria);
                }

                novoProduto.Categorias.Add(categoria);
            }

            _context.Produtos.Add(novoProduto);
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

            if (request.Nome != string.Empty && request.Nome != produto.Nome)
            {
                produto.Nome = request.Nome;
            }
            else {
                produto.Nome = produto.Nome;
            }

            produto.Preco = request.Preco;

            produto.Categorias = request.Categorias;

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
            var produtos = await _context.Produtos
                .Include(p => p.Categorias)
                .ToListAsync();

            return produtos;
        }
    }
}
