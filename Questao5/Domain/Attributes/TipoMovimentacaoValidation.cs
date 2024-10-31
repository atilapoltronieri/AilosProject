using Questao5.Domain.Language;
using System.ComponentModel.DataAnnotations;

namespace Questao5.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class TipoMovimentacaoValidation : ValidationAttribute
    {
        private Dictionary<string, string> TiposMovimentacao = new Dictionary<string, string>() { { "C", "Credito" }, { "D", "Debito" } };

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var valor = value?.ToString();

            if (string.IsNullOrWhiteSpace(valor))
                return new ValidationResult(BusinessErrorMessages.InvalidType);

            if (!TiposMovimentacao.ContainsKey(valor))
                return new ValidationResult(BusinessErrorMessages.InvalidType);

            return ValidationResult.Success;
        }
    }
}
