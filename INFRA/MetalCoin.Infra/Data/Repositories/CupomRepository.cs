using Metalcoin.Core.Domain;
using Metalcoin.Core.Interfaces.Repositories;
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
        public CupomRepository(AppDbContext appDbContext) : base(appDbContext) { }

        public async Task<Cupom> BuscarPorCodigo(string codigo)
        {
            var resultado = await DbSet.Where(c => c.Codigo == codigo).FirstOrDefaultAsync();
            return resultado;
        }
    }
}
