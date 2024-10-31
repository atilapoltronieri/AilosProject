using Questao5.Infrastructure.Database.CommandStore.Responses;

namespace Questao5.Infrastructure.Database.Repository
{
    public interface IContaCorrenteRepository
    {
        Task<ContaAtiva> GetContaAtiva(string idConta);
        Task<ContaSaldo> GetContaSaldo(string idConta);
    }
}
