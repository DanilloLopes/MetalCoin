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
            var responseErroCodigo = await ResponseErroCodigo(cupom.Codigo);
            if (responseErroCodigo.ErroMensage != null && responseErroCodigo.Id == cupom.Id)
            {
                return responseErroCodigo;
            }

            var responseErroValidade = ResponseErroValidade(cupom.DataValidade);
            if (responseErroValidade.ErroMensage != null)
            {
                return responseErroValidade;
            }

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

            var responseErroCodigo = await ResponseErroCodigo(cupom.Codigo);
            if (responseErroCodigo.ErroMensage != null)
            {
                return responseErroCodigo;
            }

            var responseErroValidade = ResponseErroValidade(cupom.DataValidade);
            if (responseErroValidade.ErroMensage != null)
            {
                return responseErroValidade;
            }
           
            
            var cupomDb = new Cupom
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

            await _cupomRepository.Adicionar(cupomDb);

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

        public async Task<bool> DeletarCategoria(Guid id)
        {
            throw new NotImplementedException();
        }

        private CupomResponse ResponseErroValidade(DateTime data)
        {
            var response = new CupomResponse();
            if (data <= DateTime.Now)
            {
                response.ErroMensage = "Data de valida do cupom inválida.";
            }
            return response;
        }

        private async Task<CupomResponse> ResponseErroCodigo(string codigo)
        {
            var cupomExiste = await _cupomRepository.BuscarPorCodigo(codigo);

            var response = new CupomResponse();
            if (cupomExiste != null)
            {
                response.Id = cupomExiste.Id;
                response.ErroMensage = "Cupom já registrado.";
            }
            
            return response;
        }
    }
}
