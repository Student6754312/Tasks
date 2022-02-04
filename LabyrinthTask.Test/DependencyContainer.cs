using System;
using IOServices;
using IOServices.Interfaces;
using IOServices.ServiceFactory;
using LabyrinthTask.Domain;
using LabyrinthTask.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace LabyrinthTask.Test
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
                .AddTransient<ITaskSolution, TaskSolution>()
                .AddTransient<IInputService, InputFromConsoleService>()
                .AddTransient<IOutputService, OutputToConsoleService>()
                .AddTransient<IOutputService, OutputToFileService>()
                .AddSingleton<IInputService, InputFromFileService>()
                .AddTransient<ILabyrinthService, LabyrinthService>()
                .Configure<ApplicationSettings>(configSection)
                .AddSingleton(srvProvider =>
                    (IInputOutputSettings)srvProvider.GetService<IOptions<ApplicationSettings>>()!.Value)
                .AddTransient<IInputServiceFactory, InputServiceFactory>()
                .AddTransient<IOutputServiceFactory, OutputServiceFactory>()
                .AddTransient<OutputToConsoleService>()
                .BuildServiceProvider();
        }

    }
}