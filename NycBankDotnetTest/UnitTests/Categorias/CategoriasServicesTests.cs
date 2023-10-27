using Microsoft.EntityFrameworkCore;
using NycBankDotnetTest.Data;
using NycBankDotnetTest.Services.CategoriaService;
using Xunit;

namespace UnitTests.Categorias
{
    public class CategoriasServicesTests
    {
        private CategoriaService _CategoriaService;

        public class CompararIgualdadeEntreListas : IEqualityComparer<Categoria>
        {
            public bool Equals(Categoria x, Categoria y)
            {
                if (x == null && y == null)
                    return true;
                else if (x == null || y == null)
                    return false;
                else
                    return x.Id == y.Id && x.Nome == y.Nome;
            }

            public int GetHashCode(Categoria obj)
            {
                return obj.Id.GetHashCode() ^ obj.Nome.GetHashCode();
            }
        }

        [Fact]
        public async Task BuscarCategoria_PorId_DeveRetornar_UmCategoria()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new DataContext(options);
            _CategoriaService = new CategoriaService(context);

            var expected = new Categoria { Id = 1, Nome = "Categoria 1"};

            context.Categorias.Add(expected);
            await context.SaveChangesAsync();

            // Act
            var actual = await _CategoriaService.BuscarCategoriaPorId(expected.Id);

            // Assert
            Assert.Equal(expected.Id, actual?.Id);
            Assert.Equal(expected.Nome, actual?.Nome);
        }

        [Fact]
        public async Task ListarCategorias_DeveRetornar_UmaLista()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new DataContext(options);
            _CategoriaService = new CategoriaService(context);

            var expectedList = new List<Categoria> { new Categoria { Id = 1, Nome = "Categoria 1"}, new Categoria { Id = 2, Nome = "Categoria 2" } };

            context.Categorias.AddRange(expectedList);
            await context.SaveChangesAsync();

            // Act
            var actual = await _CategoriaService.ListarCategorias();

            // Assert
            Assert.Equal(expectedList, actual);
        }

        [Fact]
        public async Task CadastrarCategoria_DeveRetornar_UmaLista()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new DataContext(options);
            _CategoriaService = new CategoriaService(context);

            var CategoriaCadastrado = new CategoriaCreateDto
            {
                Nome = "Categoria 1",
            };

            var expectedList = new List<Categoria> { new Categoria { Id = 1, Nome = CategoriaCadastrado.Nome } };

            // Act
            var actual = await _CategoriaService.CadastrarCategoria(CategoriaCadastrado);
            await context.SaveChangesAsync();

            // Assert
            Assert.Equal(expectedList, actual, new CompararIgualdadeEntreListas());
        }

        [Fact]
        public async Task ExcluirCategoria_DeveRetornar_UmaLista()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new DataContext(options);
            _CategoriaService = new CategoriaService(context);

            var initialList = new List<Categoria> { new Categoria { Id = 1, Nome = "Categoria 1" }, new Categoria { Id = 2, Nome = "Categoria 2" } };

            context.Categorias.AddRange(initialList);
            await context.SaveChangesAsync();

            var expectedDeleteList = new List<Categoria> { new Categoria { Id = 1, Nome = "Categoria 1" } };

            // Act
            // id do segundo Categoria da lista inicial
            var actual = await _CategoriaService.ExcluirCategoria(2);
            await context.SaveChangesAsync();

            // Assert
            Assert.Equal(expectedDeleteList, actual, new CompararIgualdadeEntreListas());
        }

        [Fact]
        public async Task EditarCategoria_DeveRetornar_UmaLista()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new DataContext(options);
            _CategoriaService = new CategoriaService(context);

            var initialList = new List<Categoria> { new Categoria { Id = 1, Nome = "Categoria 1" }, new Categoria { Id = 2, Nome = "Categoria 2" } };

            var CategoriaAtualizado = new Categoria
            {
                Nome = "NovaCategoria 2",
            };

            context.Categorias.AddRange(initialList);
            await context.SaveChangesAsync();


            var expectedEditList = new List<Categoria> { new Categoria { Id = 1, Nome = "Categoria 1" }, new Categoria { Id = 2, Nome = "NovaCategoria 2" } };

            // Act
            // id do segundo Categoria da lista inicial
            var actual = await _CategoriaService.EditarCategoria(2, CategoriaAtualizado);
            await context.SaveChangesAsync();

            // Assert
            Assert.Equal(expectedEditList, actual, new CompararIgualdadeEntreListas());
        }
    }
}
