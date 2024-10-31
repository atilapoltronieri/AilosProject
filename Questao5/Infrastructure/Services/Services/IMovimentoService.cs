using Questao5.Domain.Entities.Business;
using Questao5.Infrastructure.Database.CommandStore.Responses;

namespace Questao5.Infrastructure.Services.Services
{
    public interface IMovimentoService
    {
        Task<Guid> FazerMovimento(MovimentoDto movimento);
        Task<ContaSaldo> ContaSaldo(string idConta);
    }
}
