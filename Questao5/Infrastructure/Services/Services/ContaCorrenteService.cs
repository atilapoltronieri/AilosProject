using Questao5.Domain.Language;
using Questao5.Infrastructure.Database.CommandStore.Responses;
using Questao5.Infrastructure.Database.Repository;
using Questao5.Infrastructure.Exceptions;
using Xunit.Sdk;

namespace Questao5.Infrastructure.Services.Services
{
    public class ContaCorrenteService : IContaCorrenteService
    {
        public readonly IContaCorrenteRepository contaCorrenteRepository;

        public ContaCorrenteService(IContaCorrenteRepository contaCorrenteRepository)
        {
            this.contaCorrenteRepository = contaCorrenteRepository;
        }

        public async Task<ContaAtiva> ContaAtiva(string idConta)
        {
            return await contaCorrenteRepository.GetContaAtiva(idConta);
        }

        public async Task<ContaSaldo> ContaSaldo(string idConta)
        {
            return await contaCorrenteRepository.GetContaSaldo(idConta);
        }
    }
}
