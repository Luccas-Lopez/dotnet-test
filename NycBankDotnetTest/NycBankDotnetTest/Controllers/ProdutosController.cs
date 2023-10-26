using Microsoft.AspNetCore.Mvc;
using NycBankDotnetTest.Services.ProdutosService;

namespace NycBankDotnetTeste.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoService _produtoService;

        public ProdutosController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Produto>>> ListarProdutos()
        {
            return await _produtoService.ListarProdutos();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Produto>>> BuscarProdutoPorId(int id)
        {
            var produto = await _produtoService.BuscarProdutoPorId(id);
            if (produto is null)
            {
                return NotFound("Este produto não foi encontrado");
            }
            return Ok(produto);
        }

        [HttpPost]
        public async Task<ActionResult<List<Produto>>> CadastrarProduto(Produto produto)
        {
            var result = await _produtoService.CadastrarProduto(produto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Produto>>> EditarProduto(int id, Produto request)
        {
            var result = await _produtoService.EditarProduto(id, request);
            if (result is null)
            {
                return NotFound("Produto não encontrado.");
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Produto>>> ExlcuirProduto(int id)
        {
            var result = await _produtoService.ExcluirProduto(id);
            if (result is null)
            {
                return NotFound("Produto não encontrado.");
            }

            return Ok(result);
        }
    }
}
