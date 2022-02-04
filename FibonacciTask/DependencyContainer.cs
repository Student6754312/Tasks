using System;
using FibonacciTask.Domain;
using FibonacciTask.Services;
using IOServices;
using IOServices.Interfaces;
using IOServices.ServiceFactory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace FibonacciTask
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
                .AddTransient<ITaskSolution, TaskSolution>()
                .AddTransient<IInputService, InputFromConsoleService>()
                .AddTransient<IOutputService, OutputToConsoleService>()
                .AddSingleton<IInputService, InputFromFileService>()
                .AddSingleton<IOutputService, OutputToFileService>()
                .AddTransient<IFibonacciService, FibonacciService>()
                .Configure<ApplicationSettings>(configSection)
                .AddSingleton(srvProvider =>
                    (IInputOutputSettings) srvProvider.GetService<IOptions<ApplicationSettings>>()!.Value)
                .AddTransient<IInputServiceFactory, InputServiceFactory>()
                .AddTransient<IOutputServiceFactory,OutputServiceFactory>()
                .AddTransient<OutputToConsoleService>()
                .BuildServiceProvider();
        }

    }
}