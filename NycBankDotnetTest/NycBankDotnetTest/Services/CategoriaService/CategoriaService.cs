using Microsoft.EntityFrameworkCore;
using NycBankDotnetTest.Data;
using NycBankDotnetTest.DTOS;

namespace NycBankDotnetTest.Services.CategoriaService
{
    public class CategoriaService : ICategoriaService
    {
        private readonly DataContext _context;

        public CategoriaService(DataContext context)
        {
            _context = context;
        }
        public async Task<Categoria?> BuscarCategoriaPorId(int id)
        {
            var categoria = await _context.Categorias
                .Include(p => p.Produtos)
                .FirstOrDefaultAsync(p => p.Id == id);


            if (categoria is null)
            {
                return null;
            }
            return categoria; ;
        }

        public async Task<List<Categoria>?> CadastrarCategoria(CategoriaCreateDto request)
        {
            var novaCategoria = new Categoria
            {
                Nome = request.Nome
            };

            _context.Categorias.Add(novaCategoria);
            await _context.SaveChangesAsync();
            return await _context.Categorias.ToListAsync();
        }

        public async Task<List<Categoria>?> EditarCategoria(int id, Categoria request)
        {
            var categoria = await _context.Categorias.FindAsync(id);

            if (categoria is null)
            {
                return null;
            }

            if (request.Nome != string.Empty && request.Nome != categoria.Nome)
            {
                categoria.Nome = request.Nome;
            }
            else
            {
                categoria.Nome = categoria.Nome;
            }


            await _context.SaveChangesAsync();
            return await _context.Categorias.ToListAsync();
        }

        public async Task<List<Categoria>?> ExcluirCategoria(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria is null)
                return null;

            _context.Remove(categoria);
            await _context.SaveChangesAsync();
            return await _context.Categorias.ToListAsync();
        }

        public async Task<List<Categoria>> ListarCategorias()
        {
            var categoria = await _context.Categorias
                .Include(p => p.Produtos)
                .ToListAsync();

            return categoria;
        }
    }
}
