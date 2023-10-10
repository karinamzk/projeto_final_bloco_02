using farmacia.Model;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>().ToTable("tb_produtos");
           
        }

        // Registrar DbSet - Objeto responsável por manipular a Tabela
        public DbSet<Produto> Produtos { get; set; } = null!;



    }
}
