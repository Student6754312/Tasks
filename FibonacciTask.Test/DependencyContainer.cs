using System;
using Fibonacci.Domain;
using Fibonacci.Services;
using IOServices;
using IOServices.Base;
using IOServices.ServiceFactory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fibonacci.Test
{
    internal static class DependencyContainer
    {
        internal static IServiceProvider GetContainer(string appsettingsFileName)
        {
            //Setup DI
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(appsettingsFileName)
                .Build();

            var configSection = configuration.GetSection("ApplicationSettings");

            //Setup DI
            return new ServiceCollection()
                .AddTransient<ITaskSolution, TaskSolution>()
                .AddTransient<IInputService, InputFromConsoleService>()
                .AddTransient<IOutputService, OutputToConsoleService>()
                .AddSingleton<IInputService, InputFromFileService<ApplicationSettings>>()
                .AddTransient<IOutputService, OutputToFileService<ApplicationSettings>> ()
                .AddTransient<IFibonacciService, FibonacciService>()
                .Configure<ApplicationSettings>(configSection)
                .AddTransient<IInputServiceFactory, InputServiceFactory<ApplicationSettings>>()
                .AddTransient<IOutputServiceFactory, OutputServiceFactory<ApplicationSettings>>()
                .AddTransient<OutputToConsoleService>()
                .BuildServiceProvider();
        }

    }
}