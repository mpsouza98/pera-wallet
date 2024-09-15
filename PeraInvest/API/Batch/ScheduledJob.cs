using PeraInvest.API.Services;

namespace PeraInvest.API.Batch {
    public sealed class ScheduledJob : IHostedService, IAsyncDisposable {
        private Timer? timer;
        private readonly IServiceScopeFactory serviceScopeFactory;
        private readonly ILogger<ScheduledJob> logger;
        private readonly int EXECUTE_AT_HOUR = 3;
        private readonly int EXECUTE_AT_MINUTE = 0;

        public ScheduledJob(IServiceScopeFactory serviceScopeFactory, ILogger<ScheduledJob> logger) {
            this.serviceScopeFactory = serviceScopeFactory;
            this.logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken) {
            logger.LogInformation("Inicializando serviço ScheduledJob");

            timer = new Timer(
                callback: async state => await DoWorkAsync(),
                state: null,
                dueTime: TimeSpan.Zero,
                period: TimeSpan.FromMinutes(1));

            return Task.CompletedTask;
        }

        private async Task DoWorkAsync() {
            logger.LogInformation("ScheduledJobService executando no tempo: {time}", DateTimeOffset.Now);

            var now = DateTime.Now;
            var nextRun = DateTime.Today.AddDays(1).AddHours(EXECUTE_AT_HOUR).AddMinutes(EXECUTE_AT_MINUTE);
            var timeToNextRun = nextRun - now;

            logger.LogInformation("Proximo job à ser executando em: {0} horas", timeToNextRun.TotalHours);

            timer.Change(timeToNextRun, TimeSpan.FromHours(24));

            #if DEBUG
            logger.LogInformation("Executando rotina DEBUG");
            #else
                if(now.Hour != EXECUTE_AT_HOUR && now.Minute != EXECUTE_AT_MINUTE) return;
            #endif

            logger.LogInformation("Executando rotina de calculo valorizacao carteiras");

            using (IServiceScope scope = serviceScopeFactory.CreateScope()) {
                ICalculoService carteiraService =
                    scope.ServiceProvider.GetRequiredService<ICalculoService>();

                await carteiraService.ExecutaRotinaCalculoValorizacao();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) {
            logger.LogInformation("CronJobService esta parando");

            timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public async ValueTask DisposeAsync() {
            if (timer is IAsyncDisposable _timer) {
                await timer.DisposeAsync();
            }

            timer = null;
        }
    }
}
