using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace Etiqa.Security
{
    public sealed class ApiKeyAuthFilterAsync : IAsyncAuthorizationFilter
    {
        private readonly IConfiguration configuration;

        public ApiKeyAuthFilterAsync(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (context != null)
            {
                StringValues extractedApiKey = new();
                if (!context.HttpContext.Request.Headers.TryGetValue(AuthConstants.ApiKeyHeaderName, out extractedApiKey))
                    Return(context);

                var apiKey = configuration.GetSection(AuthConstants.ApiKeySectionName).Value;

                if (apiKey == null ||
                    (apiKey != null && !apiKey.Equals(extractedApiKey)))
                    Return(context);
            }

            return Task.CompletedTask;
        }

        private Task Return(AuthorizationFilterContext context)
        {
            context.Result = new UnauthorizedObjectResult("Invalid or missing Api key.");
            return Task.CompletedTask;
        }
    }

}
