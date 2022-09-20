using apontaServer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;

namespace apontaServer.Middlewares.Authorization
{
    public class MiddlewareAuthentication
    {
        private readonly RequestDelegate _next;

        public MiddlewareAuthentication(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, TokenService tokenService)
        {
            if(httpContext.Request.Headers.ContainsKey("Authorization"))
            {
                var authorization = httpContext.Request.Headers["Authorization"];
                
                if(!tokenService.ValidarToken(authorization.ToString().Split(" ")[1]))
                    httpContext.Response.StatusCode = 800;
            }

            await _next(httpContext);
        }
    }
}
