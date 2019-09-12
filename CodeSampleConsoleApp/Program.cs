using System;
using System.Collections.Generic;
using System.Net.Http;
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
            //var host = new HostBuilder()
            //    .ConfigureServices((hostBuilderContext, services) =>
            //    {
            //        services.AddHostedService<SampleBackgroundTask>();
            //        //services.AddHostedService<AnotherSampleHostedService>();

            //    });
            //await  host.RunConsoleAsync();
            //Console.WriteLine("Stop");
            List<IHostedService> services = new List<IHostedService>();
            services.Add(new HostedService());
            services.Add(new HostedService());
            services.Add(new HostedService());
            services.Add(new HostedService());

            foreach (var item in services)
            {
                await item.StartAsync(CancellationToken.None).ConfigureAwait(false);
                Console.WriteLine("Start Complete");
            }
            Console.WriteLine("1111");
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            await new SampleBackgroundTask().StartAsync(CancellationToken.None).ConfigureAwait(true);
            Console.WriteLine("2222");
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);


            Console.ReadKey();
        }

    }
    public class HostedService : IHostedService
    {
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("start");
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            HttpClient client = new HttpClient();
            var res = await client.GetAsync("https://www.bing.com").ConfigureAwait(false);
            Console.WriteLine(res.StatusCode);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }


    public class SampleBackgroundTask : BackgroundService
    {
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
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
            Console.WriteLine("start");
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            HttpClient client = new HttpClient();
            var res = await client.GetAsync("https://www.bing.com");
            Console.WriteLine(res.StatusCode);
            //return Task.Run(async () =>
            //{
            //    while (!stoppingToken.IsCancellationRequested)
            //    {
            //        Console.WriteLine(DateTime.Now);
            //        await Task.Delay(1000);
            //    }
            //});
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
