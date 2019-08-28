using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace WebTest
{
    class Program
    {
        static void Main(string[] args)
        {
            TestCase1();
            Console.WriteLine("Hello World!");
        }

        static void TestCase1()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddHttpClient("dishplan", options =>
             {
                 options.BaseAddress = new Uri("http://10.1.2.157:13146/api/dishplan/current");
             }).AddHttpMessageHandler<MyHttpHandler>();
            services.AddTransient<MyHttpHandler>();
            var provider = services.BuildServiceProvider();

        }
    }

    public class MyHttpHandler:DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.TryAddWithoutValidation("content-type", "application/json");
            return base.SendAsync(request, cancellationToken);
        }
    }
}
