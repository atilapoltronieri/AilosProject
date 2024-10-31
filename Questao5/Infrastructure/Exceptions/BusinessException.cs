namespace Questao5.Infrastructure.Exceptions
{
    public class BusinessException : Exception
    {
        const string DefaultMessage = "Business rules broken. Please contact your admin.";

        public BusinessException() : this(DefaultMessage)
        {
        }

        public BusinessException(string? message) : this(message, new Exception(DefaultMessage))
        {
        }

        public BusinessException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
