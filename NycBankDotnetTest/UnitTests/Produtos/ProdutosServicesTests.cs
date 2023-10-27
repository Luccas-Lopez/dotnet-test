using Microsoft.EntityFrameworkCore;
using NycBankDotnetTest.Data;
using Xunit;

namespace UnitTests.Produtos
{
    public class ProdutosServicesTests
    {
        private ProdutoService _produtoService;

        public class CompararIgualdadeEntreListas : IEqualityComparer<Produto>
        {
            public bool Equals(Produto x, Produto y)
            {
                if (x == null && y == null)
                    return true;
                else if (x == null || y == null)
                    return false;
                else
                    return x.Id == y.Id && x.Nome == y.Nome && x.Preco == y.Preco;
            }

            public int GetHashCode(Produto obj)
            {
                return obj.Id.GetHashCode() ^ obj.Nome.GetHashCode() ^ obj.Preco.GetHashCode();
            }
        }

        [Fact]
        public async Task BuscarProduto_PorId_DeveRetornar_UmProduto()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new DataContext(options);
            _produtoService = new ProdutoService(context);

            var expected = new Produto { Id = 1, Nome = "Produto 1", Preco = 110 };

            context.Produtos.Add(expected);
            await context.SaveChangesAsync(); 

            // Act
            var actual = await _produtoService.BuscarProdutoPorId(expected.Id);

            // Assert
            Assert.Equal(expected.Id, actual?.Id);
            Assert.Equal(expected.Nome, actual?.Nome);
            Assert.Equal(expected.Preco, actual?.Preco);
        }

        [Fact]
        public async Task ListarProdutos_DeveRetornar_UmaLista()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new DataContext(options);
            _produtoService = new ProdutoService(context);

            var expectedList = new List<Produto> { new Produto { Id = 1, Nome = "Produto 1", Preco = 110 }, new Produto { Id = 2, Nome = "Produto 2", Preco = 220 } };

            context.Produtos.AddRange(expectedList);
            await context.SaveChangesAsync(); 

            // Act
            var actual = await _produtoService.ListarProdutos();

            // Assert
            Assert.Equal(expectedList, actual);
        }
        
        [Fact]
        public async Task CadastrarProduto_DeveRetornar_UmaLista()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new DataContext(options);
            _produtoService = new ProdutoService(context);

            var produtoCadastrado = new ProdutoCreateDto
            {
                Nome = "Produto 1",
                Preco = 110,
                Categorias = new List<Categoria>()
            };

            var expectedList = new List<Produto> { new Produto { Id = 1, Nome = produtoCadastrado.Nome, Preco = produtoCadastrado.Preco, Categorias = new List<Categoria>() } };

            // Act
            var actual = await _produtoService.CadastrarProduto(produtoCadastrado);
            await context.SaveChangesAsync(); 

            // Assert
            Assert.Equal(expectedList, actual, new CompararIgualdadeEntreListas());
        }        
        
        [Fact]
        public async Task ExcluirProduto_DeveRetornar_UmaLista()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new DataContext(options);
            _produtoService = new ProdutoService(context);

            var initialList = new List<Produto> { new Produto { Id = 1, Nome = "Produto 1", Preco = 110 }, new Produto { Id = 2, Nome = "Produto 2", Preco = 220 } };

            context.Produtos.AddRange(initialList);
            await context.SaveChangesAsync();

            var expectedDeleteList = new List<Produto> { new Produto { Id = 1, Nome = "Produto 1", Preco = 110 }};

            // Act
            // id do segundo produto da lista inicial
            var actual = await _produtoService.ExcluirProduto(2);
            await context.SaveChangesAsync(); 

            // Assert
            Assert.Equal(expectedDeleteList, actual, new CompararIgualdadeEntreListas());
        }     
        
        [Fact]
        public async Task EditarProduto_DeveRetornar_UmaLista()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new DataContext(options);
            _produtoService = new ProdutoService(context);

            var initialList = new List<Produto> { new Produto { Id = 1, Nome = "Produto 1", Preco = 110 }, new Produto { Id = 2, Nome = "Produto 2", Preco = 220 } };

            var produtoAtualizado = new Produto
            {
                Nome = "Produto 2",
                Preco = 200
            };

            context.Produtos.AddRange(initialList);
            await context.SaveChangesAsync();


            var expectedEditList = new List<Produto> { new Produto { Id = 1, Nome = "Produto 1", Preco = 110 }, new Produto { Id = 2, Nome = "Produto 2", Preco = 200 } };

            // Act
            // id do segundo produto da lista inicial
            var actual = await _produtoService.EditarProduto(2, produtoAtualizado);
            await context.SaveChangesAsync(); 

            // Assert
            Assert.Equal(expectedEditList, actual, new CompararIgualdadeEntreListas());
        }
    }
}
