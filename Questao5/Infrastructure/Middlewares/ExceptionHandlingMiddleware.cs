using Microsoft.Extensions.Logging;
using Questao5.Infrastructure.Exceptions;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using static System.Net.Mime.MediaTypeNames;

namespace Questao5.Infrastructure.Middlewares
{
    [ExcludeFromCodeCoverage]
    public record ExceptionResponse(HttpStatusCode StatusCode, string Description);

    [ExcludeFromCodeCoverage]
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionHandlingMiddleware> logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            logger.LogError(exception, "An unexpected error occurred.");

            ExceptionResponse response = exception switch
            {
                BusinessException ex => new ExceptionResponse(HttpStatusCode.BadRequest, ex.Message),
                ApplicationException _ => new ExceptionResponse(HttpStatusCode.BadRequest, "Application exception occurred."),
                KeyNotFoundException _ => new ExceptionResponse(HttpStatusCode.NotFound, "The request key not found."),
                UnauthorizedAccessException _ => new ExceptionResponse(HttpStatusCode.Unauthorized, "Unauthorized."),
                _ => new ExceptionResponse(HttpStatusCode.InternalServerError, "Internal server error. Please retry later.")
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)response.StatusCode;
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
