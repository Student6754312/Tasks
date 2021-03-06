using IOServices.Interfaces;

namespace IOServices.Tests.Base
{
    public class InputOutputSettings : IInputOutputSettings
    {
        public InputOutputSettings(string defaultInputService = "File", string inputFilePath = "input.txt", string outputFilePath = "output.txt")
        {
            DefaultInputService = defaultInputService;
            InputFilePath = inputFilePath;
            OutputFilePath = outputFilePath;
        }

        public string DefaultInputService { get; set; }
        public string InputFilePath { get; set; } 
        public string OutputFilePath { get; set; }
    }
}