using System;
using IOServices;
using IOServices.Interfaces;
using IOServices.ServiceFactory;
using LabyrinthTask.Domain;
using LabyrinthTask.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LabyrinthTask
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
                .AddSingleton<IInputService, InputFromFileService<ApplicationSettings >>()
                .AddSingleton<IOutputService, OutputToFileService<ApplicationSettings>>()
                .AddTransient<ILabyrinthService, LabyrinthService>()
                .Configure<ApplicationSettings>(configSection)
                .AddTransient<IInputServiceFactory, InputServiceFactory<ApplicationSettings>>()
                .AddTransient<IOutputServiceFactory,OutputServiceFactory<ApplicationSettings>>()
                .AddTransient<OutputToConsoleService>()
                .BuildServiceProvider();
        }

    }
}