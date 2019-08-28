using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders.Embedded;
using Microsoft.Extensions.FileProviders;
using System.Reflection;
using System.IO;

namespace FileLoad
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            var assembly = Assembly.GetEntryAssembly();
            services.AddSingleton<IFileProvider>(new EmbeddedFileProvider(assembly));

            var provider = services.BuildServiceProvider();
            var fileProvider = provider.GetService<IFileProvider>();
            var file = fileProvider.GetFileInfo("file.hello.txt");
            if(file == null)
            {
                throw new FileNotFoundException();
            }
            using(var stream = file.CreateReadStream())
            {
                var anotherStream = file.CreateReadStream();
            }

            Console.WriteLine("Hello World!");
        }
    }
}
