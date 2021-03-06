using apontaServer.Database;
using apontaServer.Repositories;
using apontaServer.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System.Text;

namespace aponta_server
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            var key = Encoding.ASCII.GetBytes(Configuration.GetValue<string>("KeyAuth"));

            services.AddControllers().AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null);

            services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressConsumesConstraintForFormFileParameters = false;
                    options.SuppressInferBindingSourcesForParameters = true;
                    options.SuppressModelStateInvalidFilter = false;
                    options.SuppressMapClientErrors = false;
                    options.ClientErrorMapping[StatusCodes.Status404NotFound].Link =
                        "https://httpstatuses.com/404";
                });

            services.AddCors(op => op.AddDefaultPolicy(
                builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                )) ;

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            //INTERFACE
            services.AddSingleton<IConexao>(sp => new Conexao(connectionString));
            services.AddScoped<IMetodosGenericosRepositorio>(sp => new MetodosGenericosRepositorio(sp.GetService<IConexao>()));
            services.AddScoped<ILoginRepositorio>(sp => new LoginRepositorio(sp.GetService<IConexao>()));
            services.AddScoped<ITarefaRepositorio>(sp => new TarefaRepositorio(sp.GetService<IConexao>()));
            services.AddScoped<IApontamentoRepositorio>(sp => new ApontamentoRepositorio(sp.GetService<IConexao>()));
            services.AddScoped<ITempoTotalTarefaRepositorio>(sp => new TempoTotalTarefaRepositorio(sp.GetService<IConexao>()));

            //SERVICE
            services.AddScoped(sp => new TokenService(Configuration));
            services.AddScoped(sp => new MetodosGenericosService(sp.GetService<IMetodosGenericosRepositorio>()));
            services.AddScoped(sp => new LoginService(sp.GetService<ILoginRepositorio>(), sp.GetService<TokenService>()));
            services.AddScoped(sp => new TarefaService(sp.GetService<ITarefaRepositorio>()));
            services.AddScoped(sp => new ApontamentoService(sp.GetService<IApontamentoRepositorio>(), sp.GetService<TarefaService>(), sp.GetService<LoginService>(), sp.GetService<TempoTotalTarefaService>(), sp.GetService<MetodosGenericosService>()));
            services.AddScoped(sp => new TempoTotalTarefaService(sp.GetService<ITempoTotalTarefaRepositorio>(), sp.GetService<TarefaService>(), sp.GetService<LoginService>()));
            services.AddScoped(sp => new TarefaService(sp.GetService<ITarefaRepositorio>()));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
        }
    }
}
