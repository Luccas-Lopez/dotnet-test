using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.Components.DictionaryAdapter.Xml;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NycBankDotnetTest.Controllers;
using NycBankDotnetTest.Services.CategoriaService;

namespace UnitTests.ControllersTests.CategoriasControllerTests
{
    public class CategoriasControllerTests
    {

        [Fact]
        public async Task CategoriasController_DeveRetornar_UmaLista()
        {
            var CategoriaServiceMock = new Mock<ICategoriaService>();
            var CategoriasController = new CategoriasController(CategoriaServiceMock.Object);

            // Arrange
            var expectedValue = new List<Categoria>
            {
                new Categoria
                {
                     Id = 1,
                    Nome = "Categoria1",
                },
                new Categoria
                {
                    Id = 2,
                    Nome = "Categoria2",
                }
            };

            CategoriaServiceMock.Setup(service => service.ListarCategorias()).ReturnsAsync(expectedValue);

            // Act
            var result = await CategoriasController.ListarCategorias();

            // Assert
            var okResult = Assert.IsType<ActionResult<List<Categoria>>>(result);
            var model = Assert.IsAssignableFrom<List<Categoria>>(okResult.Value);
            Assert.Equal(expectedValue, model);
        }

        [Fact]
        public async Task BuscarCategoriaPorId_DeveRetornarCategoria_QuandoEncontrado()
        {
            var CategoriaServiceMock = new Mock<ICategoriaService>();
            var CategoriasController = new CategoriasController(CategoriaServiceMock.Object);
            // Arrange
            var expectedCategoria = new Categoria
            {
                Id = 1,
                Nome = "Categoria1",
            };

            CategoriaServiceMock.Setup(service => service.BuscarCategoriaPorId(expectedCategoria.Id)).ReturnsAsync(expectedCategoria);

            // Act
            var result = await CategoriasController.BuscarCategoriaPorId(expectedCategoria.Id);

            //Assert
            var okResult = Assert.IsType<ActionResult<Categoria>>(result);
            var model = Assert.IsAssignableFrom<OkObjectResult>(okResult.Result);
            Assert.Equal(expectedCategoria, model.Value);
        }

        [Fact]
        public async Task CadastrarCategoria_SemCategoria_DeveRetornarListaComNovoCategoria_E_CategoriaVazia()
        {
            var CategoriaServiceMock = new Mock<ICategoriaService>();
            var CategoriasController = new CategoriasController(CategoriaServiceMock.Object);
            // Arrange
            var novoCategoria = new CategoriaCreateDto
            {
                Nome = "Categoria2",
            };

            var expectedList = new List<Categoria>
            {
                new Categoria { Nome = "Categoria1"},
                new Categoria { Nome = novoCategoria.Nome},

            };


            CategoriaServiceMock.Setup(service => service.CadastrarCategoria(novoCategoria)).ReturnsAsync(expectedList);

            // Act
            var result = await CategoriasController.CadastrarCategoria(novoCategoria);

            //Assert
            var okResult = Assert.IsType<ActionResult<List<Categoria>>>(result);
            var model = Assert.IsAssignableFrom<OkObjectResult>(okResult.Result);
            Assert.Equal(expectedList, model.Value);
        }

        [Fact]
        public async Task EditarCategoria_DeveRetornar_UmaLista()
        {
            var CategoriaServiceMock = new Mock<ICategoriaService>();
            var CategoriasController = new CategoriasController(CategoriaServiceMock.Object);
            // Arrange

            var initialList = new List<Categoria>
            {
                new Categoria
                {
                     Id = 1,
                    Nome = "Categoria1",
                },
                new Categoria
                {
                    Id = 2,
                    Nome = "Categoria2",
                }
            };

            var CategoriaEditado = new Categoria
            {
                Nome = "NovaCategoria2",
            };

            var expectedListEdit = new List<Categoria>
            {
                new Categoria
                {
                     Id = 1,
                    Nome = "Categoria1",
                },
                new Categoria
                {
                    Id = 2,
                    Nome = "NovaCategoria2",
                }
            };

            CategoriaServiceMock.Setup(service => service.EditarCategoria(2, CategoriaEditado)).ReturnsAsync(expectedListEdit);

            // Act
            var result = await CategoriasController.EditarCategoria(2, CategoriaEditado);

            //Assert
            var okResult = Assert.IsType<ActionResult<List<Categoria>>>(result);
            var model = Assert.IsAssignableFrom<OkObjectResult>(okResult.Result);
            Assert.Equal(expectedListEdit, model.Value);
        }

        [Fact]
        public async Task ExcluirCategoria_DeveRetornar_UmaLista()
        {
            var CategoriaServiceMock = new Mock<ICategoriaService>();
            var CategoriasController = new CategoriasController(CategoriaServiceMock.Object);
            // Arrange

            var initialList = new List<Categoria>
            {
                new Categoria
                {
                     Id = 1,
                    Nome = "Categoria1"
                },
                new Categoria
                {
                    Id = 2,
                    Nome = "Categoria2"
                }
            };

            var CategoriaDeletado = new Categoria
            {
                Nome = "Categoria2"
            };

            var expectedListDelete = new List<Categoria>
            {
                new Categoria
                {
                     Id = 1,
                    Nome = "Categoria1"
                },
            };

            CategoriaServiceMock.Setup(service => service.ExcluirCategoria(2)).ReturnsAsync(expectedListDelete);

            // Act
            var result = await CategoriasController.ExcluirCategoria(2);

            //Assert
            var okResult = Assert.IsType<ActionResult<List<Categoria>>>(result);
            var model = Assert.IsAssignableFrom<OkObjectResult>(okResult.Result);
            Assert.Equal(expectedListDelete, model.Value);
        }
    }
}


