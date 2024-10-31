using Questao5.Domain.Entities.Business;
using Questao5.Domain.Language;
using Questao5.Infrastructure.Database.CommandStore.Responses;
using Questao5.Infrastructure.Database.Repository;
using Questao5.Infrastructure.Exceptions;

namespace Questao5.Infrastructure.Services.Services
{
    public class MovimentoService : IMovimentoService
    {
        public readonly IMovimentoRepository movimentoRepository;
        public readonly IContaCorrenteService contaCorrenteService;

        public MovimentoService(IMovimentoRepository movimentoRepository, IContaCorrenteService contaCorrenteService)
        {
            this.movimentoRepository = movimentoRepository;
            this.contaCorrenteService = contaCorrenteService;
        }

        public async Task<Guid> FazerMovimento(MovimentoDto movimento)
        {
            var contaAtiva = await contaCorrenteService.ContaAtiva(movimento.IdConta);
            if (!contaAtiva.Ativo)
                throw new BusinessException(BusinessErrorMessages.InvalidAccount);

            return await movimentoRepository.InserirMovimento(movimento);
        }

        public async Task<ContaSaldo> ContaSaldo(string idConta)
        {
            var contaSalto = await contaCorrenteService.ContaSaldo(idConta);

            if (contaSalto == null)
                throw new BusinessException(BusinessErrorMessages.InvalidAccount);

            var saldo = await movimentoRepository.SaldoConta(idConta);

            contaSalto.Saldo = saldo;
            contaSalto.DataConsulta = DateTime.Now;

            return contaSalto;
        }
    }
}
