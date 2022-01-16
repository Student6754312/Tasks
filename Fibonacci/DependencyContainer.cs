using System;
using Fibonacci.Services;
using Fibonacci.Services.ServiceFactory;
using IOServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fibonacci
{
    internal static class DependencyContainer
    {
        internal static IServiceProvider GetContainer()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var configSection = configuration.GetSection("ApplicationSettings");

            //Setup DI
            return new ServiceCollection()
                .AddTransient<IInputService, InputFromConsoleService>()
                .AddTransient<IInputSelectionFactory, InputSelectionFactory>()
                .AddTransient<IFibonacciService, FibonacciService>()
                .AddSingleton<IInputService, InputFromFileService>()
                .AddSingleton<IOutputService, OutputService>()
                .Configure<ApplicationSettings>(configSection)
                .BuildServiceProvider();
        }

    }
}