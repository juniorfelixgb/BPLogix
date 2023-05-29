using BPLogix.BooksCvsGenerator.Domain.Exceptions;
using BPLogix.BooksCvsGenerator.Domain.Shared;
using System.Net;
using System.Net.Mime;
using System.Text.Json;

namespace BPLogix.BooksCvsGenerator.Infrastructure.Middlewares
{
    public class ErrorHandlerMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
			try
			{
                await next(context);
			}
			catch (Exception ex)
			{
                await ErrorHandlerProccesor(context, ex);
			}
        }

        private async Task ErrorHandlerProccesor(HttpContext context, Exception ex)
        {
            var code = ex switch
            {
                ArgumentNullException => HttpStatusCode.NotFound,
                ArgumentException or InvalidOperationException => HttpStatusCode.InternalServerError,
                NullReferenceException or BookNotProcessedException => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError,
            };
            var result = JsonSerializer.Serialize(new { isSuccess = false, message = ex.Message, code });
            context.Response.ContentType = MediaTypeNames.Application.Json;
            context.Response.StatusCode = Convert.ToInt32(code);
            await context.Response.WriteAsync(result);
        }
    }
}