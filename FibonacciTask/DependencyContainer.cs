using System;
using FibonacciTask.Domain;
using FibonacciTask.Services;
using IOServices;
using IOServices.Interfaces;
using IOServices.ServiceFactory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
                .AddSingleton<IInputService, InputFromFileService<ApplicationSettings>>()
                .AddTransient<IOutputService, OutputToFileService<ApplicationSettings>>()
                .AddTransient<IFibonacciService, FibonacciService>()
                .Configure<ApplicationSettings>(configSection)
                .AddTransient<IInputServiceFactory, InputServiceFactory<ApplicationSettings>>()
                .AddTransient<IOutputServiceFactory,OutputServiceFactory<ApplicationSettings> >()
                .AddTransient<OutputToConsoleService>()
                .BuildServiceProvider();
        }

    }
}