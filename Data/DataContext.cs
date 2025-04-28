using Microsoft.EntityFrameworkCore;
using Shop.Models;

namespace Shop.Data
{

public class DataContext : DbContext // uma classe que herda de DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    { }

    //Representacao das nossas tabelas em memória(dbset-busca em memoria)
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
    }
}