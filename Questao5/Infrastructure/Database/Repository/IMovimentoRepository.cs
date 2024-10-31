using Questao5.Domain.Entities.Business;

namespace Questao5.Infrastructure.Database.Repository
{
    public interface IMovimentoRepository
    {
        Task<Guid> InserirMovimento(MovimentoDto movimento);
        Task<double> SaldoConta(string idConta);
    }
}
