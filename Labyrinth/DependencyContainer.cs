using System;
using IOServices;
using IOServices.ServicesFactory;
using IOServices.ServicesFactory.Base;
using Labyrinth.Domain;
using Labyrinth.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Labyrinth
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
                .AddTransient<IOutputService, OutputToFileService>()
                .AddTransient<ILabyrinthService, LabyrinthService>()
                .Configure<ApplicationSettings>(configSection)
                .AddTransient<IInputServiceFactory, InputServiceFactory>()
                .AddTransient<IOutputServiceFactory,OutputServiceFactory >()
                .BuildServiceProvider();
        }

    }
}