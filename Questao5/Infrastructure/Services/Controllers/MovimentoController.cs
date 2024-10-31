using Microsoft.AspNetCore.Mvc;
using Questao5.Domain.Entities.Business;
using Questao5.Infrastructure.Services.Services;

namespace Questao5.Infrastructure.Services.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovimentoController : ControllerBase
    {
        public IMovimentoService movimentoService;

        public MovimentoController(IMovimentoService movimentoService)
        {
            this.movimentoService = movimentoService;
        }

        [HttpPost(Name = "MovimentacaoDeConta")]
        public async Task<IActionResult> MovimentacaoDeConta(MovimentoDto movimentacao)
        {
            var newGuid = await movimentoService.FazerMovimento(movimentacao);

            return Ok(newGuid);
        }

        [HttpGet(Name = "SaldoConta")]
        public async Task<IActionResult> SaldoConta(string idConta)
        {
            var contaSaldo = await movimentoService.ContaSaldo(idConta);

            return Ok(contaSaldo);
        }
    }
}
