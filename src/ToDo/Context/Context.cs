using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using ToDo.Models;

namespace ToDo.Context
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options) { }

        public DbSet<Categoria> Categorias => Set<Categoria>();
        public DbSet<Usuario> Usuarios => Set<Usuario>();
        public DbSet<Tarefa> Tarefas => Set<Tarefa>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                 .HasMany(usuario => usuario.Tarefas)
                 .WithOne(tarefa => tarefa.Usuario)
                 .HasForeignKey(tarefa => tarefa.UsuarioId)
                 .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Tarefa>()
                .HasOne(tarefa => tarefa.Usuario)
                .WithMany(usuario => usuario.Tarefas)
                .HasForeignKey(tarefa => tarefa.UsuarioId);

            modelBuilder.Entity<Tarefa>()
                .HasMany(t => t.Categorias)
                .WithMany(c => c.Tarefas);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
