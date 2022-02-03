using IOServices.Interfaces;

namespace FibonacciTask
{
    public class ApplicationSettings :IInputOutputSettings
    {
        public string DefaultInputService { get; set; } = null!;
        public string InputFilePath { get; set; } = null!;
        public string OutputFilePath { get; set; } = null!;
    }
}