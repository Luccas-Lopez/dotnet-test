# Projeto Processo Seletivo NYCBank - Crud de Produtos e Categorias

O Projeto desenvolvido, é uma API RESTful, onde inicialmente foi constrúido o CRUD para produtos, e posteriormente para Categorias, utilizando EfCore e pacotes derivados deste, o banco foi construído com migrations, e os Testes Unitários com xunit, moq e o InMemoryDatabase também parte do EfCore.<br>
Para organização das tarefas criei um quadro no trello com cada passo tirado da descrição do desafio proposto, segue o link abaixo:<br>
https://trello.com/b/HdTQqQ7L/processo-seletivo-nyc-bank-restful-api-de-produtos<br>
Utilizei como guia de estilos esta documentação, para convenção de nomes, em métodos, variáveis e etc:<br>
https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/identifier-names

## Rodando o Projeto
* Copie esta url (https://github.com/Luccas-Lopez/dotnet-test)
* Abra o Terminal do Git e digite o comando: git clone -b LucasNepomusceno https://github.com/Luccas-Lopez/dotnet-test
* Caso não esteja instalado ou esteja em versão inferior será necessário instalar o sdk .NET 
* Abra a solução do Projeto
* Para criar o Banco a ser usado, no menu ferramentas abra o Package Manager Console e digite os seguintes comandos um de cada vez:
  - dotnet ef database update TabeladeProdutos
  - dotnet ef database update RelacionamentoProdutosCategorias
  - dotnet ef database update ProdutosCategoriasNulo
  - dotnet ef database update IdTabelaRelacionamento
* Após isso, poderá rodar o projeto normalmente e testar as funcionalidades com o Swagger ou algum outro programa de sua preferência

## Rotas
### Produtos
#### Produtos[Get]
- Retorna todos os Produtos da Aplicação.

#### Produtos/{id}[Get]
- Retorna o produto cujo o id é equivalente ao passado.

#### Produtos[Post]
- Cria um novo produto
- Recebe como parâmetros: nome, preco, e opcional categoria, caso a categoria passada não exista ela é criada na tabela de categorias ao enviar a requisição.

#### Produtos/{id}[Put]
- Edita os dados do produto cujo o id é equivalente ao passado
- Recebe como parâmetros: nome, preco, e opcional categoria, nesta requisição só é possível adicionar categorias existentes à lista.

#### Produtos/{id}[Delete]
- Exclui o produto cujo o id é equivalente ao passado.

### Categorias
#### Categorias[Get]
- Retorna todos os Categorias da Aplicação.

#### Categorias/{id}[Get]
- Retorna a categoria cujo o id é equivalente ao passado.

#### Categorias[Post]
- Cria uma nova categoria
- Recebe como parâmetro: nome;
  
#### Categorias/{id}[Put]
- Edita os dados da categoria cujo o id é equivalente ao passado
- Recebe como parâmetro: nome.

#### Categorias/{id}[Delete]
- Exclui a categoria cujo o id é equivalente ao passado.
