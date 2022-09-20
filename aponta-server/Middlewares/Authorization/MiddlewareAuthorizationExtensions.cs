using apontaServer.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace apontaServer.Middlewares.Authorization
{
    public static class MiddlewareAuthorizationExtensions
    {
        public static IApplicationBuilder UseMiddlewareAuthorization(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MiddlewareAuthentication>();
        }
    }
}
