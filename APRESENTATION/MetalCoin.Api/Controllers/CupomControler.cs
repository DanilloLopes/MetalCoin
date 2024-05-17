using Metalcoin.Core.Dtos.Categorias;
using Metalcoin.Core.Dtos.Request;
using Metalcoin.Core.Interfaces.Repositories;
using Metalcoin.Core.Interfaces.Services;
using MetalCoin.Application.Services;
using MetalCoin.Infra.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MetalCoin.Api.Controllers
{
    [ApiController]
    public class CupomControler : ControllerBase
    {
        private readonly ICupomRepository _cupomRepository;
        private readonly ICupomService _cupomService;

        public CupomControler(ICupomRepository cupomRepository, ICupomService cupomService)
        {
            _cupomRepository = cupomRepository;
            _cupomService = cupomService;
        }

        #region HTTP GET

        [HttpGet]
        [Route("cupons/todos")]
        public async Task<ActionResult> ObterTodosCupons()
        {
            var listaCupons = await _cupomRepository.ObterTodos();

            if (listaCupons.Count == 0) return NoContent();

            return Ok(listaCupons);
        }



        #endregion

        #region HTTP POST

        [HttpPost]
        [Route("cupons/cadastrar")]
        public async Task<ActionResult> CadastrarCupom([FromBody] CupomCadastrarRequest cupom)
        {
            if (cupom == null) return BadRequest("Informe os dados do cupom");

            var response = await _cupomService.CadastrarCupom(cupom);

            if (response == null) return BadRequest("Erro nos dados!");

            return Created("cadastrar", response);
        }

        #endregion

        #region HTTP POST

        [HttpPut]
        [Route("cupons/atualizar")]
        public async Task<ActionResult> AtualizarCupom([FromBody] CupomAtualizarRequest cupom)
        {
            if (cupom == null) return BadRequest("Nenhum valor chegou na API");

            var response = await _cupomService.AtualizarCupom(cupom);

            return Ok(response);
        }

        #endregion
    }
}
