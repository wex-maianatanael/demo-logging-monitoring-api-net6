using Microsoft.EntityFrameworkCore;

namespace Demo.Api.CustomMiddlewares
{
    public class CriticalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CriticalExceptionMiddleware> _logger;

        public CriticalExceptionMiddleware(RequestDelegate next, ILogger<CriticalExceptionMiddleware> logger)
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
            catch (DbUpdateException ex)
            {
                if(ex.Message.Contains("database"))
                {
                    _logger.LogCritical(ex, "Fatal error occurred in the application.");
                }
            }
        }
    }
}
