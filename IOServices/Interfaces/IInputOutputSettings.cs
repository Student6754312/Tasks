namespace IOServices.Interfaces
{
    public interface IInputOutputSettings
    {
        string DefaultInputService { get; set; }
        string InputFilePath { get; set; }
        string OutputFilePath { get; set; }
    }
}