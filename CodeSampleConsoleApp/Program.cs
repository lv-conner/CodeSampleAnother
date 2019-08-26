using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;

namespace CodeSampleConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var host = new HostBuilder()
                .ConfigureServices((hostBuilderContext, services) =>
                {
                    services.AddHostedService<SampleBackgroundTask>();
                    //services.AddHostedService<AnotherSampleHostedService>();

                });
            await  host.RunConsoleAsync();
            Console.WriteLine("Stop");
        }

    }

    public class SampleBackgroundTask : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    Console.WriteLine("Printer is working.");
            //    await Task.Delay(1000);
            //}
            //if (stoppingToken.IsCancellationRequested)
            //{
            //    return;
            //}
            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    await Task.Run(() =>
            //    {
            //        Console.WriteLine(DateTime.Now);
            //    });
            //    await Task.Delay(1000);
            //}
            return Task.Run(async () =>
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    Console.WriteLine(DateTime.Now);
                    await Task.Delay(1000);
                }
            });
        }
    }

    public class AnotherSampleHostedService : IHostedService
    {
        private Task _task = null;
        public Task StartAsync(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return Task.CompletedTask;
            }
            _task = Task.Run(async () =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    Console.WriteLine(DateTime.Now);
                    await Task.Delay(1000);
                }
            });
            return _task;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine(_task.Status);
            return Task.CompletedTask;
        }
    }
}
