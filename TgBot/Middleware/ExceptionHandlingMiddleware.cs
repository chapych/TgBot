using Microsoft.AspNetCore.Mvc;

namespace TgBot.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                var guid = Guid.NewGuid();
                var problemDetails = new ProblemDetails
                {
                    Title = "Server Error",
                    Instance = guid.ToString(),
                    Detail = exception.Message
                };
                _logger.LogError(new EventId(0, guid.ToString()), exception, exception.Message);
                
                await context.Response.WriteAsJsonAsync(problemDetails);
            }
        }
    }
}
