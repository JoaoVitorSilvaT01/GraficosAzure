using Microsoft.EntityFrameworkCore;

namespace AzureOrbis.Components.Servicos
{
    public class ServicoTemporal
    {
        private readonly PostgresContext _context;

        public ServicoTemporal(PostgresContext context)
        {
            _context = context;
        }

        public async Task<List<BaseResumo>> GetConsumptionDataAsync()
        {
            var data = await _context.BaseResumos
                .GroupBy(cd => cd.Periodo)
                .Select(g => new BaseResumo
                {
                    Periodo = g.Key,
                    QtdConsumo = g.Sum(cd => cd.QtdConsumo)
                })
                .OrderBy(cd => cd.Periodo)
                .ToListAsync();

            return data;
        }
    }

    

}
