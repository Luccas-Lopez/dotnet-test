using Microsoft.EntityFrameworkCore;
using NycBankDotnetTest.Migrations;

namespace NycBankDotnetTest.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=.\\SQLexpress;Database=produtos_categorias_db;Trusted_Connection=true;TrustServerCertificate=true;");
        }

        public DbSet<Produto> Produtos { get; set; }

        public DbSet<Categoria> Categorias { get; set; }
    }
}
