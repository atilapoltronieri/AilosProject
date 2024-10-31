using Questao5.Domain.Attributes;
using Questao5.Domain.Language;
using Questao5.Infrastructure.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace Questao5.Domain.Entities.Business
{
    public class MovimentoDto
    {
        [Required]
        public string Requisicao { get; set; }
        [Required(ErrorMessage = BusinessErrorMessages.InvalidAccount)]
        public string IdConta { get; set; }
        [Required, Range(0.01, double.MaxValue, ErrorMessage = BusinessErrorMessages.InvalidValue) ]
        public double Valor { get; set; }
        [Required, TipoMovimentacaoValidation]
        public string TipoMovimentacao { get; set; }
    }
}
