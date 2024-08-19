using AutoMapper;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Mysqlx.Crud;
using PeraInvest.API.Clients;
using PeraInvest.API.Clients.auth;
using PeraInvest.API.Commands.Handlers;
using PeraInvest.API.Controllers.Mappers;
using PeraInvest.Domain.CarteiraAggregate;
using PeraInvest.Domain.CarteiraAggregate.Repository;
using PeraInvest.Infrastructure;
using PeraInvest.Infrastructure.Repositories;

namespace PeraInvest {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) {
            var connectionString = Configuration.GetConnectionString("Default");

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddMemoryCache();
            services.AddDbContext<CarteiraContext>(options => options.UseMySQL(connectionString: connectionString!));
            services.AddDbContext<AtivoFinanceiroContext>(options => options.UseMySQL(connectionString: connectionString!));

            services.AddSingleton(new ClientCredentialsTokenRequest(
                url: Configuration["Authentication:GenerateTokenUrl"] ?? throw new NullReferenceException(),
                clientId: Configuration["Authentication:ClientId"] ?? throw new NullReferenceException(),
                clientSecret: Configuration["Authentication:ClientSecret"] ?? throw new NullReferenceException(),
                grantType: Configuration["Authentication:GrantType"] ?? throw new NullReferenceException()
            ));

            services.AddTransient<AuthTokenHandler>();

            services.AddHttpClient<UsuarioClient>(client => {
                client.BaseAddress = new Uri("http://localhost:18080/admin/realms/pera-invest/users");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            }).AddHttpMessageHandler<AuthTokenHandler>();

            services.AddHttpClient<AuthTokenClient>(client => {
                client.BaseAddress = new Uri("http://localhost:18080/realms/pera-invest/protocol/openid-connect/token");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            services.AddControllers();

            services.AddControllers()
                .AddJsonOptions(options => {
                    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
                });

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Startup>());

            services.AddScoped<IAtivoFinanceiroRepository, AtivoFinanceiroRepository>();

            services.AddAutoMapper(typeof(Startup));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
