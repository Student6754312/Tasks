using System;
using System.Collections.Generic;
using Fibonacci.Services;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Fibonacci.Test.Services
{
    public class FibonacciServiceTests
    {
        private IServiceProvider _serviceProvider;
      
        public FibonacciServiceTests()
       {
           _serviceProvider = DependencyContainer.GetContainer("appsettings.console.test.json");
       }
        [Theory]
        [MemberData("GenerateDigit")]
        public void FibTest(int x, long y)
        {
            // Arrange
            int c = 5;
            // Act
            var fibonacciService = _serviceProvider.GetService<IFibonacciService>();
            var f = fibonacciService.Fib(x);
            
            //Assert
            Assert.Equal(y, f);
        }

    
       public static IEnumerable<object[]> GenerateDigit()
        {
            yield return new object[] { 1, 1 };
            yield return new object[] { 2, 1 };
            yield return new object[] { 5 , 5 };
            yield return new object[] { 7, 13 };
            yield return new object[] { 11, 89 };
            yield return new object[] { 50, 12586269025 };
            yield return new object[] { 61, 2504730781961 };
            yield return new object[] { 70, 190392490709135 };
            yield return new object[] { 89, 1779979416004714189 };
        }
    }
}