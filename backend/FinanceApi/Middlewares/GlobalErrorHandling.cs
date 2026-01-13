using System.Net;
using System.Text.Json;

namespace FinanceAPI.Middlewares
{
    public class GlobalErrorHandling
    {
        private readonly RequestDelegate _next;

        public GlobalErrorHandling(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERRO CRÍTICO: {ex.Message}");

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = new { mensagem = "Ocorreu um erro interno no servidor. Tente mais tarde." };
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }
}