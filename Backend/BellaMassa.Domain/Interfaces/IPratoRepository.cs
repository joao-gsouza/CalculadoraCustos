using BellaMassa.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BellaMassa.Domain.Interfaces
{
    public interface IPratoRepository
    {
        Task<IEnumerable<Prato>> ObterTodosAsync();
        Task<Prato?> ObterPorIdAsync(int id);
        Task<Prato?> ObterPratoComIngredientesAsync(int id);
        Task<Prato> AdicionarAsync(Prato prato);
        Task AtualizarAsync(Prato prato);
        Task RemoverAsync(int id);

        Task<ItemFichaTecnica?> ObterIngredienteAsync(int pratoId, int produtoBaseId);
        Task<ItemFichaTecnica> AdicionarIngredienteAsync(ItemFichaTecnica item);
        Task RemoverIngredienteAsync(ItemFichaTecnica item);
    }
}
