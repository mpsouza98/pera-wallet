namespace PeraInvest.API.Batch {
    public class CalculoValorizacaoCarteira : IHostedService, IDisposable {
        private Timer timer;
        private readonly ILogger<CalculoValorizacaoCarteira> logger;
        private readonly int EXECUTE_AT_HOUR = 3;
        private readonly int EXECUTE_AT_MINUTE = 0;

        public CalculoValorizacaoCarteira(ILogger<CalculoValorizacaoCarteira> logger) {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task StartAsync(CancellationToken cancellationToken) {
            logger.LogInformation("Inicializando serviço CalculoValorizacaoCarteira");

            timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));

            return Task.CompletedTask;
        }

        private void DoWork(object state) {
            logger.LogInformation("ScheduledJobService is running at: {time}", DateTimeOffset.Now);

            var now = DateTime.Now;
            #if DEBUG
                var nextRun = DateTime.Now.AddSeconds(10);
            #else
                nextRun = DateTime.Today.AddDays(1).AddHours(EXECUTE_AT_HOUR).AddMinutes(EXECUTE_AT_MINUTE);
            #endif
            var timeToNextRun = nextRun - now;

            logger.LogInformation("Next job run scheduled in: {0} hours", timeToNextRun.TotalHours);

            timer.Change(timeToNextRun, TimeSpan.FromHours(24));

            if(now.Hour != EXECUTE_AT_HOUR && now.Minute != EXECUTE_AT_MINUTE) return;

            logger.LogInformation("Job is running");
        }

        public Task StopAsync(CancellationToken cancellationToken) {
            logger.LogInformation("CronJobService is stopping.");

            timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose() {
            timer?.Dispose();
        }
    }
}
