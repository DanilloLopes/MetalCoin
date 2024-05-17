using Metalcoin.Core.Domain;
using Metalcoin.Core.Dtos.Request;
using Metalcoin.Core.Dtos.Response;
using Metalcoin.Core.Enums;
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
        public async Task<CupomResponse> AtualizarCupom(CupomAtualizarRequest cupom)
        {
            var cupomDb = await _cupomRepository.ObterPorId(cupom.Id);

            cupomDb.Codigo = cupom.Codigo;
            cupomDb.Descricao = cupom.Descricao;
            cupomDb.ValorDesconto = cupom.ValorDesconto;
            cupomDb.TipoDesconto = cupom.TipoDesconto;
            cupomDb.DataValidade = cupom.DataValidade;
            cupomDb.QuantidadeLiberada = cupom.QuantidadeLiberada;
            cupomDb.Status = cupom.Status;
            cupomDb.DataAlteracao = DateTime.Now;

            await _cupomRepository.Atualizar(cupomDb);

            var response = new CupomResponse
            {
                Id = cupomDb.Id,
                Codigo = cupomDb.Codigo,
                Descricao = cupomDb.Descricao,
                ValorDesconto = cupomDb.ValorDesconto,
                TipoDesconto = cupomDb.TipoDesconto,
                DataValidade = cupomDb.DataValidade,
                QuantidadeLiberada = cupomDb.QuantidadeLiberada,
                QuantidadeUsada = cupomDb.QuantidadeUsada,
                Status = cupomDb.Status,
                DataCadastro = cupomDb.DataCadastro,
                DataAlteracao = cupomDb.DataAlteracao
            };

            return response;
            
            
            
        }

        public async Task<CupomResponse> CadastrarCupom(CupomCadastrarRequest cupom)
        {
            var cupomExiste = await _cupomRepository.BuscarPorCodigo(cupom.Codigo);

            if (cupomExiste != null) return null;
            if (cupom.DataValidade == DateTime.Now) return null;

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

        }

        public async Task<bool> DeletarCategoria(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
