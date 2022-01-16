using System;
using Fibonacci.Services;
using Fibonacci.Services.ServiceFactory;
using IOServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fibonacci.Test
{
    internal static class DependencyContainer
    {
        internal static IServiceProvider GetContainer(string appsettingsFileName)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(appsettingsFileName)
                .Build();

            var configSection = configuration.GetSection("ApplicationSettings");

            //Setup DI
            return new ServiceCollection()
                .AddTransient<IInputService, InputFromConsoleService>()
                .AddTransient<IInputSelectionFactory, InputSelectionFactory>()
                .AddSingleton<IInputService, InputFromFileService>()
                .AddSingleton<IOutputService, OutputToConsoleService>()
                .Configure<ApplicationSettings>(configSection)
                .AddTransient<IFibonacciService, FibonacciService>()
                .BuildServiceProvider();
        }

    }
}