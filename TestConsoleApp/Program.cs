using System;
using System.Threading.Tasks;
using LF.AspNetCore.EventBus;
using LF.AspNetCore.EventBus.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var year = 10;
            var nextYear = ++year;


            IServiceCollection services = new ServiceCollection();
            services.AddMemoryQueueEventBus();
            var provider = services.BuildServiceProvider();

            var eventBus = provider.GetService<IEventBus>();


            eventBus.Publish<SampelEvent>(new SampelEvent() { Name = "tim lv", Exception = true });
            Console.WriteLine("publish complete");
            Task.Delay(5000).ContinueWith( t =>
            {
                eventBus.Publish<SampelEvent>(new SampelEvent() { Name = "tim lv hahah", Exception = true });
            });
            Console.ReadKey();
            Console.WriteLine("Hello World!");
        }
    }

    public class SampleEventHandler : IEventHandler<SampelEvent>
    {
        public void Handle(SampelEvent @event)
        {
            Console.WriteLine(@event.Name);
        }
    }
    public class SampelEvent : IEvent
    {
        public string Name { get; set; }
        public bool Exception { get; set; }
    }
}
