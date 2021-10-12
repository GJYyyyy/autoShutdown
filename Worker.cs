using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace autoShutdown
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private Shutdown shutdown;
        // 超过这个秒数将会触发自动关机
        private readonly long autoShutdownSeconds = 8 * 3600;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            shutdown = new Shutdown();
        }

        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //_logger.LogInformation(CheckComputerFreeState.GetLastInputTime().ToString());
                //Console.WriteLine($"{autoShutdownSeconds} <= {CheckComputerFreeState.GetLastInputTime()}");
                if (autoShutdownSeconds <= CheckComputerFreeState.GetLastInputTime())
                {
                    shutdown.ExecuteShutdown();
                }
                else if(0 == CheckComputerFreeState.GetLastInputTime())
                {
                    shutdown.CancelShutdown();
                }
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
