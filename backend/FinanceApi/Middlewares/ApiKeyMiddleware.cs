namespace FinanceAPI.Middlewares
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private const string APIKEYNAME = "x-api-key";

        public ApiKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var configuration = context.RequestServices.GetRequiredService<IConfiguration>();
            var apiKeyCorreta = configuration.GetValue<string>("ApiKey");

            if (!context.Request.Headers.TryGetValue(APIKEYNAME, out var extractedApiKey))
            {
                context.Response.StatusCode = 401; // Não autorizado
                await context.Response.WriteAsync("API Key não fornecida. (Use o header 'x-api-key')");
                return;
            }

            if (!apiKeyCorreta.Equals(extractedApiKey))
            {
                context.Response.StatusCode = 403; // Proibido
                await context.Response.WriteAsync("API Key inválida!");
                return;
            }

            await _next(context);
        }
    }
}