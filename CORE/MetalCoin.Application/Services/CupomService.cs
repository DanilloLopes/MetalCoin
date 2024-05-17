using Metalcoin.Core.Domain;
using Metalcoin.Core.Dtos.Request;
using Metalcoin.Core.Dtos.Response;
using Metalcoin.Core.Interfaces.Repositories;
using Metalcoin.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetalCoin.Application.Services
{
    
    public class CupomService : ICupomService
    {
        private readonly ICupomRepository _cupomRepository;
        public CupomService(ICupomRepository repository)
        {
            _cupomRepository = repository;
        }
        public async Task<CupomResponse> AtualizarCategoria(CupomAtualizarRequest cupom)
        {
            
            
            
            
            
            throw new NotImplementedException();
        }

        public async Task<CupomResponse> CadastrarCategoria(CupomCadastrarRequest cupom)
        {
            var cupomExiste = await _cupomRepository.BuscarPorCodigo(cupom.Codigo);

            if (cupomExiste != null) return null;

            var cupomEntidade = new Cupom
            {
                Codigo = cupom.Codigo,
                Descricao = cupom.Descricao,
                ValorDesconto = cupom.ValorDesconto,
                TipoDesconto = cupom.TipoDesconto,
                DataValidade = cupom.DataValidade,
                QuantidadeLiberada = cupom.QuantidadeLiberada,
                QuantidadeUsada = 0,
                Status = Metalcoin.Core.Enums.TipoStatus.Ativo,
                DataCadastro = DateTime.Now,
                DataAlteracao = DateTime.Now
            };

            await _cupomRepository.Adicionar(cupomEntidade);

            var response = new CupomResponse
            {
                Id = cupomEntidade.Id,
                Descricao = cupomEntidade.Descricao,
                ValorDesconto = cupomEntidade.ValorDesconto,
                TipoDesconto = cupomEntidade.TipoDesconto,
                DataValidade = cupomEntidade.DataValidade,
                QuantidadeLiberada = cupomEntidade.QuantidadeLiberada,
                QuantidadeUsada = cupomEntidade.QuantidadeUsada,
                Status = cupomEntidade.Status,
                DataCadastro = cupomEntidade.DataCadastro,
                DataAlteracao = cupomEntidade.DataAlteracao
            };

            return response;


            throw new NotImplementedException();
        }

        public async Task<bool> DeletarCategoria(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
