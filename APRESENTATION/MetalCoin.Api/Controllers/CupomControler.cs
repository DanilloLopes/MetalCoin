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

        public CupomControler(ICupomRepository cupomRepository)
        {
            _cupomRepository = cupomRepository;
            
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

        //[HttpPost]
        //[Route("cupons/cadastrar")]
        //public async Task<ActionResult> CadastrarCupom([FromBody] CupomCadastrarRequest categoria)
        //{
        //    if (categoria == null) return BadRequest("Informe o nome da categoria");

        //    var response = await _categoriaService.CadastrarCategoria(categoria);

        //    if (response == null) return BadRequest("Categoria já existe");

        //    return Created("cadastrar", response);
        //}

        #endregion
    }
}
