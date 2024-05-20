using Metalcoin.Core.Abstracts;
using Metalcoin.Core.Domain;
using Metalcoin.Core.Enums;
using Metalcoin.Core.Interfaces.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetalCoin.Infra.Data.Repositories
{
    public class CupomRepository : Repository<Cupom>, ICupomRepository
    {
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        public CupomRepository(AppDbContext appDbContext) : base(appDbContext) { }

        public async Task<Cupom> BuscarPorCodigo(string codigo)
        {
            var resultado = await DbSet.Where(c => c.Codigo == codigo).FirstOrDefaultAsync();
            return resultado;
        }

        public virtual async Task AtualizarStatusVencido()
        {
            await _semaphore.WaitAsync();
            try
            {
                await Db.Database.ExecuteSqlRawAsync("UPDATE Cupons SET Status = @status WHERE DataValidade < @dataAtual",
                    new SqlParameter("@status", (int)TipoStatus.Expirado),
                    new SqlParameter("@dataAtual", DateTime.Now));
                await Salvar();
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public virtual async Task AtualizarStatusTotalUtilizado()
        {
            await _semaphore.WaitAsync();
            try
            {
                await Db.Database.ExecuteSqlRawAsync("UPDATE Cupons SET Status = @status WHERE QuantidadeLiberada <= QuantidadeUsada",
                new SqlParameter("@status", (int)TipoStatus.TotalmenteUtilizado));

                await Salvar();
            }
            finally
            {
                _semaphore.Release();
            }
           
        }

        public async Task<List<Cupom>> ObterTodosValidos()
        {
            return await DbSet.Where(c => c.Status == 0).ToListAsync();
        }
    }
}
