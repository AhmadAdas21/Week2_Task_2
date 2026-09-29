using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;

namespace Week2_Task_2
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
    HttpContext context,
    Exception exception,
    CancellationToken cancellationToken)
        {
            context.Response.StatusCode =
                StatusCodes.Status500InternalServerError;

            context.Response.ContentType = "application/json";

            var response = new
            {
                StatusCode = 500,
                Message = exception.Message,
                InnerException = exception.InnerException?.Message
            };

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response),
                cancellationToken);

            return true;
        }
    }
}
