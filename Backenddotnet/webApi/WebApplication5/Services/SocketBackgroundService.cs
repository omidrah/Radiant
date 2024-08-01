namespace WebApplication5.Services
{
    public class SocketBackgroundService : BackgroundService
    {
        private readonly ILogger<SocketBackgroundService> _logger;
        private readonly SocketService _socketService;

        public SocketBackgroundService(ILogger<SocketBackgroundService> logger, SocketService socketService)
        {
            _logger = logger;
            _socketService = socketService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("SocketBackgroundService is starting.");

            stoppingToken.Register(() =>
                _logger.LogInformation("SocketBackgroundService is stopping."));

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("SocketBackgroundService is doing background work.");

                // Here you can call the socket service to perform some periodic work
                byte[] dummyData = new byte[10]; // Replace with actual data
                await _socketService.SendDataAsync(dummyData, "192.168.1.15", 7);

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }

            _logger.LogInformation("SocketBackgroundService has stopped.");
        }
    }
}
