using System;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using MovieBooking.Applications;
using MovieBooking.Services;

namespace MovieBooking
{
    public static class Program
    {
        private static void Main()
        {
            MainAsync().GetAwaiter().GetResult();
        }

        private static async Task MainAsync()
        {
            HostApplicationBuilder builder = Host.CreateApplicationBuilder();
            builder.Services
                .AddSingleton(new IoProcessor(Console.In, Console.Out))
                .AddDbContextCollection()
                .AddSingleton<Application>()
                .AddHostedService<ApplicationService>()
                .AddLogging(builder => builder.ClearProviders());
            using IHost app = builder.Build();
            await app.RunAsync();
        }
    }
}
