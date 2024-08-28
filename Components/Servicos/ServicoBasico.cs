using Microsoft.EntityFrameworkCore;

namespace AzureOrbis.Components.Servicos
{
    public class ServicoBasico
    {
        private readonly PostgresContext _context;

        public ServicoBasico(PostgresContext context)
        {
            _context = context;
        }

        public async Task<List<BaseResumo>> GetEntitiesAsync()
        {
            return await _context.BaseResumos.ToListAsync();
        }
    }
}
