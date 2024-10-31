using Questao5.Infrastructure.Database.CommandStore.Responses;

namespace Questao5.Infrastructure.Services.Services
{
    public interface IContaCorrenteService
    {
        Task<ContaAtiva> ContaAtiva(string idConta);
        Task<ContaSaldo> ContaSaldo(string idConta);
    }
}
