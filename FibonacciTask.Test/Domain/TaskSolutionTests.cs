using System;
using System.Collections.Generic;
using System.IO;
using FibonacciTask.Domain;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace FibonacciTask.Test.Domain
{
    public class TaskSolutionTests
    {
        private IServiceProvider _serviceProvider;
        private ITaskSolution _taskSolution;

        public TaskSolutionTests()
        {
            _serviceProvider = DependencyContainer.GetContainer("appsettings.file.test.json");
            _taskSolution = _serviceProvider.GetRequiredService<ITaskSolution>();
        }

        [Fact]
        public void InputTest()
        {
            // Arrange
            var numList = new List<int>();

            // Act
            _taskSolution.Input(numList);

            //Assert
            Assert.Equal(6, numList.Count);
            Assert.Equal(5, numList[0]);
            Assert.Equal(7, numList[1]);
            Assert.Equal(11, numList[2]);

        }
        
        [Fact]
        public void OutputTest()
        {
            // Arrange
            var numList = new List<int>();
            numList.Add(11);

            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            // Act
            _taskSolution.Output(numList);

            //Assert
            Assert.Equal("Ausgabe:\n\r\nDie Fibonacci Zahl für 11 ist: 89", stringWriter.ToString().Trim());
        }
    }
}