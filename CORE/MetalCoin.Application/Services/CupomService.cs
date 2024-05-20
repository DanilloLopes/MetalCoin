using Metalcoin.Core.Domain;
using Metalcoin.Core.Dtos.Request;
using Metalcoin.Core.Dtos.Response;
using Metalcoin.Core.Interfaces.Repositories;
using Metalcoin.Core.Interfaces.Services;

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
            AtualizarBanco();

            var responseErroValidade = ResponseErroValidade(cupom.DataValidade);
            if (responseErroValidade.ErroMensage != null)
            {
                return responseErroValidade;
            }
            
            var responseErroCupom = await ResponseErroCupom(cupom.Id);
            if (responseErroCupom.ErroMensage != null)
            {
                return responseErroCupom;
            }

            var responseErroCodigo = await ResponseErroCodigo(cupom.Codigo);
            if (responseErroCodigo.ErroMensage != null && responseErroCodigo.Id != cupom.Id)
            {
                return responseErroCodigo;
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

        public async Task<CupomResponse> DeletarCupom(Guid id)
        {
            var responseErroCupom = await ResponseErroCupom(id);
            if (responseErroCupom.ErroMensage != null)
            {
                return responseErroCupom;
            }

            var cupomDb = await _cupomRepository.ObterPorId(id);

            var response = new CupomResponse();


            if (cupomDb.QuantidadeUsada != 0)
            {
                response.ErroMensage = "Existem pedidos utilizando este cupom. Operação de exclusão cancelada.";
                return response;
            }
            

            await _cupomRepository.Remover(id);
            return response;
        }

        public async Task<CupomResponse> AtivarCupom(Guid id)
        {
            AtualizarBanco();
            var responseErroCupom = await ResponseErroCupom(id);
            if (responseErroCupom.ErroMensage != null)
            {
                return responseErroCupom;
            }

            var cupomDb = await _cupomRepository.ObterPorId(id);

            var response = new CupomResponse();

            if(cupomDb.Status == Metalcoin.Core.Enums.TipoStatus.Ativo)
            {
                response.ErroMensage = "Cupom já está ativo.";
                return response;
            }

            if (cupomDb.Status == Metalcoin.Core.Enums.TipoStatus.Expirado)
            {
                response.ErroMensage = "Cupom está expirado, status não pode ser alterado.";
                return response;
            }

            if (cupomDb.Status == Metalcoin.Core.Enums.TipoStatus.TotalmenteUtilizado)
            {
                response.ErroMensage = "Cupom já foi totalmente utilizado, status não pode ser alterado.";
                return response;
            }

            cupomDb.Status = Metalcoin.Core.Enums.TipoStatus.Ativo;

            await _cupomRepository.Atualizar(cupomDb);

            return response;
        }

        public async Task<CupomResponse> DesativarCupom(Guid id)
        {
            AtualizarBanco();
            var responseErroCupom = await ResponseErroCupom(id);
            if (responseErroCupom.ErroMensage != null)
            {
                return responseErroCupom;
            }

            var cupomDb = await _cupomRepository.ObterPorId(id);

            var response = new CupomResponse();

            if (cupomDb.Status == Metalcoin.Core.Enums.TipoStatus.Desativado)
            {
                response.ErroMensage = "Cupom já está Desativado.";
                return response;
            }

            if (cupomDb.Status == Metalcoin.Core.Enums.TipoStatus.Expirado)
            {
                response.ErroMensage = "Cupom está expirado, status não pode ser alterado.";
                return response;
            }

            if (cupomDb.Status == Metalcoin.Core.Enums.TipoStatus.TotalmenteUtilizado)
            {
                response.ErroMensage = "Cupom já foi totalmente utilizado, status não pode ser alterado.";
                return response;
            }

            cupomDb.Status = Metalcoin.Core.Enums.TipoStatus.Desativado;

            await _cupomRepository.Atualizar(cupomDb);

            return response;
        }

        private CupomResponse ResponseErroValidade(DateTime data)
        {
            AtualizarBanco();
            var response = new CupomResponse();
            if (data <= DateTime.Now)
            {
                response.ErroMensage = "Data de valida do cupom inválida. Adicione uma data maior do que a data atual.";
            }
            return response;
        }

        private async Task<CupomResponse> ResponseErroCodigo(string codigo)
        {
            AtualizarBanco();
            var cupomExiste = await _cupomRepository.BuscarPorCodigo(codigo);

            var response = new CupomResponse();
            if (cupomExiste != null)
            {
                response.Id = cupomExiste.Id;
                response.ErroMensage = "Código de cupom já registrado.";
            }
            
            return response;
        }

        private async Task<CupomResponse> ResponseErroCupom(Guid id)
        {
            AtualizarBanco();
            var cupom = await _cupomRepository.ObterPorId(id);

            var response = new CupomResponse();
            if (cupom == null)
            {
                response.ErroMensage = "Cupom não existe.";
            }

            return response;
        }

        private async void AtualizarBanco()
        {
            await _cupomRepository.AtualizarStatusTotalUtilizado();
            await _cupomRepository.AtualizarStatusVencido();
        }
    }
}
