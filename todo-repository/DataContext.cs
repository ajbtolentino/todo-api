using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using todo_domain;

namespace todo_repository;

public class DataContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite("Data Source=todo.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Item>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();
    }

    public DbSet<Item> Items { get; private set; }
}

