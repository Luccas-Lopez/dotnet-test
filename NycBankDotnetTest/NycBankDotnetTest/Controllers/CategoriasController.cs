using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NycBankDotnetTest.DTOS;
using NycBankDotnetTest.Services.CategoriaService;

namespace NycBankDotnetTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaService _categoriasService;

        public CategoriasController(ICategoriaService categoriaService)
        {
            _categoriasService = categoriaService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Categoria>>> ListarCategorias()
        {
            return await _categoriasService.ListarCategorias();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Categoria>>> BuscarCategoriaPorId(int id)
        {
            var Categoria = await _categoriasService.BuscarCategoriaPorId(id);
            if (Categoria is null)
            {
                return NotFound("Este Categoria não foi encontrado");
            }
            return Ok(Categoria);
        }

        [HttpPost]
        public async Task<ActionResult<List<Categoria>>> CadastrarCategoria(CategoriaCreateDto request)
        {
            var result = await _categoriasService.CadastrarCategoria(request);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Categoria>>> EditarCategoria(int id, Categoria request)
        {
            var result = await _categoriasService.EditarCategoria(id, request);
            if (result is null)
            {
                return NotFound("Categoria não encontrado.");
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Categoria>>> ExlcuirCategoria(int id)
        {
            var result = await _categoriasService.ExcluirCategoria(id);
            if (result is null)
            {
                return NotFound("Categoria não encontrado.");
            }

            return Ok(result);
        }
    }
}
