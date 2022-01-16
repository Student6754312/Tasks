﻿using System;
using IOServices;
using Labyrinth.Services;
using Labyrinth.Services.ServiceFactory;
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
                .AddTransient<IInputService, InputFromConsoleService>()
                .AddTransient<IInputSelectionFactory, InputSelectionFactory>()
                .AddSingleton<IInputService, InputFromFileService>()
                .AddSingleton<IOutputService, OutputService>()
                .Configure<ApplicationSettings>(configSection)
                .AddTransient<ILabyrinthService, LabyrinthService>()
                .BuildServiceProvider();
        }

    }
}