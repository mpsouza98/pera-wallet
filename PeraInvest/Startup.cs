using Microsoft.EntityFrameworkCore;
using PeraInvest.API.Clients;
using PeraInvest.API.Clients.auth;

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
            //services.AddDbContext<CarteiraContext>(
                //options => options.UseMySQL(connectionString: connectionString!));

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
