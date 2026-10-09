using CalculadoraCustos.Domain.Interfaces;
using CalculadoraCustos.Domain.Models;
using CalculadoraCustos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalculadoraCustos.Infrastructure.Repositories
{
    public class PratoRepository : IPratoRepository
    {
        readonly AppDbContext _context;
        public PratoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Prato>> ObterTodosAsync()
        {
            return await _context.Pratos.ToListAsync();
        }

        public async Task<Prato?> ObterPorIdAsync(int id)
        {
            return await _context.Pratos.FindAsync(id);
        }

        public async Task<Prato?> ObterPratoComIngredientesAsync(int id)
        {
            return await _context.Pratos
                .Include(p => p.Ingredientes)
                    .ThenInclude(i => i.ProdutoBase)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Prato> AdicionarAsync(Prato prato)
        {
            await _context.Pratos.AddAsync(prato);
            await _context.SaveChangesAsync();

            return prato;
        }

        public async Task AtualizarAsync(Prato prato)
        {
            _context.Pratos.Update(prato);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(int id)
        {
            var prato = await _context.Pratos.FindAsync(id);
            if (prato != null)
            {
                _context.Pratos.Remove(prato);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ItemFichaTecnica?> ObterIngredienteAsync(int pratoId, int produtoBaseId)
        {
            return await _context.ItensFichaTecnica
                .FirstOrDefaultAsync(i => i.PratoId == pratoId && i.ProdutoBaseId == produtoBaseId);
        }

        public async Task<ItemFichaTecnica> AdicionarIngredienteAsync(ItemFichaTecnica item)
        {
            await _context.ItensFichaTecnica.AddAsync(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task RemoverIngredienteAsync(ItemFichaTecnica item)
        {
            _context.ItensFichaTecnica.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}
