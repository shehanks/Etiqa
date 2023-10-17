using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace Etiqa.Security
{
    public sealed class ApiKeyAuthMiddleware
    {
        private readonly RequestDelegate next;

        private readonly IConfiguration configuration;

        public ApiKeyAuthMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            this.next = next;
            this.configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context != null)
            {
                StringValues extractedApiKey = new();
                if (!context.Request.Headers.TryGetValue(AuthConstants.ApiKeyHeaderName, out extractedApiKey))
                    await Return(context);

                var apiKey = configuration.GetSection(AuthConstants.ApiKeySectionName).Value;

                if (apiKey == null ||
                    (apiKey != null && !apiKey.Equals(extractedApiKey)))
                    await Return(context);
            }

            await next(context);
        }

        private async Task Return(HttpContext context)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Invalid or missing Api key.");
            return;
        }
    }
}
