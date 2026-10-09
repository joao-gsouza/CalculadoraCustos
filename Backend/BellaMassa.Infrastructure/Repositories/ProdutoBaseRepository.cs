using BellaMassa.Domain.Interfaces;
using BellaMassa.Domain.Models;
using BellaMassa.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BellaMassa.Infrastructure.Repositories
{
    public class ProdutoBaseRepository : IProdutoBaseRepository
    {
        private readonly AppDbContext _context;

        public ProdutoBaseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProdutoBase>> ObterTodosAsync()
        {
            return await _context.ProdutosBase.ToListAsync();
        }

        public async Task<ProdutoBase?> ObterPorIdAsync(int id)
        {
            return await _context.ProdutosBase.FindAsync(id);
        }

        public async Task<ProdutoBase> AdicionarAsync(ProdutoBase produto)
        {
            await _context.ProdutosBase.AddAsync(produto);
            await _context.SaveChangesAsync();

            return produto;
        }

        public async Task AtualizarAsync(ProdutoBase produto)
        {
            _context.ProdutosBase.Update(produto);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(int id)
        {
            var produto = await _context.ProdutosBase.FindAsync(id);
            if (produto != null)
            {
                _context.ProdutosBase.Remove(produto);
                await _context.SaveChangesAsync();
            }
        }
    }
}
