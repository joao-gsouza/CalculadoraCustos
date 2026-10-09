using CalculadoraCustos.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalculadoraCustos.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<ProdutoBase> ProdutosBase { get; set; }
        public DbSet<Prato> Pratos { get; set; }
        public DbSet<ItemFichaTecnica> ItensFichaTecnica { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProdutoBase>()
                .Property(p => p.PrecoEmbalagem)
                .HasPrecision(10,2);

            modelBuilder.Entity<ItemFichaTecnica>()
                .HasKey(i => i.Id);

            modelBuilder.Entity<ItemFichaTecnica>()
                .HasOne(i => i.Prato)
                .WithMany(p => p.Ingredientes)
                .HasForeignKey(i => i.PratoId);

            modelBuilder.Entity<ItemFichaTecnica>()
                .HasOne(i => i.ProdutoBase)
                .WithMany()
                .HasForeignKey(i => i.ProdutoBaseId);
        }
    }
}
