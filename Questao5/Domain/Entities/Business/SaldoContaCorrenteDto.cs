using Questao5.Domain.Language;
using System.ComponentModel.DataAnnotations;

namespace Questao5.Domain.Entities.Business
{
    public class SaldoContaCorrenteDto
    {
        [Required(ErrorMessage = BusinessErrorMessages.InvalidAccount)]
        public string IdContaCorrente { get; set; }
    }
}
