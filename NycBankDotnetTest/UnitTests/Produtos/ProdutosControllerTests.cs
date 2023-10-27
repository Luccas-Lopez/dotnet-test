using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.Components.DictionaryAdapter.Xml;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using NycBankDotnetTest.Controllers;
using NycBankDotnetTest.Data;
using NycBankDotnetTest.DTOS;
using NycBankDotnetTest.Models;
using NycBankDotnetTest.Services.ProdutosService;
using Xunit;


namespace UnitTests.ControllersTests.ProdutosControllerTests
{
    public class ProdutosControllerTests
    {

        [Fact]
        public async Task ProdutosController_DeveRetornar_UmaLista()
        {
            var produtoServiceMock = new Mock<IProdutoService>();
            var produtosController = new ProdutosController(produtoServiceMock.Object);

            // Arrange
            var expectedValue = new List<Produto>
            {
                new Produto
                {
                     Id = 1,
                    Nome = "Produto1",
                    Preco = 100,
                    Categorias = new List<Categoria>()
                },
                new Produto
                {
                    Id = 2,
                    Nome = "Produto2",
                    Preco = 200,
                    Categorias = new List<Categoria>()
                }
            };

            produtoServiceMock.Setup(service => service.ListarProdutos()).ReturnsAsync(expectedValue);

            // Act
            var result = await produtosController.ListarProdutos();

            // Assert
            var okResult = Assert.IsType<ActionResult<List<Produto>>>(result);
            var model = Assert.IsAssignableFrom<List<Produto>>(okResult.Value);
            Assert.Equal(expectedValue, model);
        }

        [Fact]
        public async Task BuscarProdutoPorId_DeveRetornarProduto_QuandoEncontrado()
        {
            var produtoServiceMock = new Mock<IProdutoService>();
            var produtosController = new ProdutosController(produtoServiceMock.Object);
            // Arrange
            var expectedProduto = new Produto
            {
                Id = 1,
                Nome = "Produto1",
                Preco = 100,
                Categorias = []
            };

            produtoServiceMock.Setup(service => service.BuscarProdutoPorId(expectedProduto.Id)).ReturnsAsync(expectedProduto);

            // Act
            var result = await produtosController.BuscarProdutoPorId(expectedProduto.Id);

            //Assert
            var okResult = Assert.IsType<ActionResult<Produto>>(result);
            var model = Assert.IsAssignableFrom<OkObjectResult>(okResult.Result);
            Assert.Equal(expectedProduto, model.Value);
        }

        [Fact]
        public async Task CadastrarProduto_SemCategoria_DeveRetornarListaComNovoProduto_E_CategoriaVazia()
        {
            var produtoServiceMock = new Mock<IProdutoService>();
            var produtosController = new ProdutosController(produtoServiceMock.Object);
            // Arrange
            var novoProduto = new ProdutoCreateDto
            {
                Nome = "Produto2",
                Preco = 200,
                Categorias = new List<Categoria> { }
            };

            var expectedList = new List<Produto>
            {
                new Produto { Nome = "Produto1", Preco = 100},
                new Produto { Nome = novoProduto.Nome, Preco = novoProduto.Preco},

            };


            produtoServiceMock.Setup(service => service.CadastrarProduto(novoProduto)).ReturnsAsync(expectedList);

            // Act
            var result = await produtosController.CadastrarProduto(novoProduto);

            //Assert
            var okResult = Assert.IsType<ActionResult<List<Produto>>>(result);
            var model = Assert.IsAssignableFrom<OkObjectResult>(okResult.Result);
            Assert.Equal(expectedList, model.Value);
        }

        [Fact]
        public async Task CadastrarProduto_ComCategorias_DeveRetornarListaComNovoProduto()
        {
            var produtoServiceMock = new Mock<IProdutoService>();
            var produtosController = new ProdutosController(produtoServiceMock.Object);
            // Arrange

            var novaCategoria = new CategoriaCreateDto
            {
                Nome = "categoria_nova"
            };

            var novoProduto = new ProdutoCreateDto
            {
                Nome = "Produto2",
                Preco = 200,
                Categorias = new List<Categoria> {
                    new Categoria
                    {
                        Nome = novaCategoria.Nome
                    }
                }
            };

            var expectedList = new List<Produto>
            {
                new Produto { Nome = "Produto1", Preco = 100},
                new Produto { Nome = novoProduto.Nome, Preco = novoProduto.Preco, Categorias = novoProduto.Categorias},

            };

            produtoServiceMock.Setup(service => service.CadastrarProduto(novoProduto)).ReturnsAsync(expectedList);

            // Act
            var result = await produtosController.CadastrarProduto(novoProduto);

            //Assert
            var okResult = Assert.IsType<ActionResult<List<Produto>>>(result);
            var model = Assert.IsAssignableFrom<OkObjectResult>(okResult.Result);
            Assert.Equal(expectedList, model.Value);
        }

        [Fact]
        public async Task EditarProduto_DeveRetornar_UmaLista()
        {
            var produtoServiceMock = new Mock<IProdutoService>();
            var produtosController = new ProdutosController(produtoServiceMock.Object);
            // Arrange

            var expectedList = new List<Produto>
            {
                new Produto
                {
                     Id = 1,
                    Nome = "Produto1",
                    Preco = 100,
                    Categorias = new List<Categoria>()
                },
                new Produto
                {
                    Id = 2,
                    Nome = "Produto2",
                    Preco = 200,
                    Categorias = new List<Categoria>()
                }
            };

            var produtoEditado = new Produto
            {
                Nome = "Produto2",
                Preco = 250,
                Categorias = new List<Categoria>
                {
                    new Categoria
                    {
                        Nome = "categoria1"
                    }
                }
            };

            var expectedListEdit = new List<Produto>
            {
                new Produto
                {
                     Id = 1,
                    Nome = "Produto1",
                    Preco = 100,
                    Categorias = new List<Categoria>()
                },
                new Produto
                {
                    Id = 2,
                    Nome = "Produto2",
                    Preco = 250,
                    Categorias = new List<Categoria>
                    {
                        new Categoria
                        {
                            Nome = "categoria1"
                        }
                    }
                }
            };

            produtoServiceMock.Setup(service => service.EditarProduto(2, produtoEditado)).ReturnsAsync(expectedListEdit);

            // Act
            var result = await produtosController.EditarProduto(2, produtoEditado);

            //Assert
            var okResult = Assert.IsType<ActionResult<List<Produto>>>(result);
            var model = Assert.IsAssignableFrom<OkObjectResult>(okResult.Result);
            Assert.Equal(expectedListEdit, model.Value);
        }

        [Fact]
        public async Task ExcluirProduto_DeveRetornar_UmaLista()
        {
            var produtoServiceMock = new Mock<IProdutoService>();
            var produtosController = new ProdutosController(produtoServiceMock.Object);
            // Arrange

            var initialList = new List<Produto>
            {
                new Produto
                {
                     Id = 1,
                    Nome = "Produto1",
                    Preco = 100,
                    Categorias = new List<Categoria>()
                },
                new Produto
                {
                    Id = 2,
                    Nome = "Produto2",
                    Preco = 200,
                    Categorias = new List<Categoria>()
                }
            };

            var produtoDeletado = new Produto
            {
                Nome = "Produto2",
                Preco = 250,
                Categorias = new List<Categoria>
                {
                    new Categoria
                    {
                        Nome = "categoria1"
                    }
                }
            };

            var expectedListDelete = new List<Produto>
            {
                new Produto
                {
                     Id = 1,
                    Nome = "Produto1",
                    Preco = 100,
                    Categorias = new List<Categoria>()
                },
            };

            produtoServiceMock.Setup(service => service.ExcluirProduto(2)).ReturnsAsync(expectedListDelete);

            // Act
            var result = await produtosController.ExcluirProduto(2);

            //Assert
            var okResult = Assert.IsType<ActionResult<List<Produto>>>(result);
            var model = Assert.IsAssignableFrom<OkObjectResult>(okResult.Result);
            Assert.Equal(expectedListDelete, model.Value);
        }
    }
}


