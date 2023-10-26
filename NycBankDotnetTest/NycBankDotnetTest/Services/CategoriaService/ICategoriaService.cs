using NycBankDotnetTest.DTOS;

namespace NycBankDotnetTest.Services.CategoriaService
{
    public interface ICategoriaService
    {
        Task<Categoria?> BuscarCategoriaPorId(int id);
        Task<List<Categoria>?> CadastrarCategoria(CategoriaCreateDto request);
        Task<List<Categoria>?> EditarCategoria(int id, Categoria request);
        Task<List<Categoria>?> ExcluirCategoria(int id);
        Task<List<Categoria>> ListarCategorias();
    }
}
