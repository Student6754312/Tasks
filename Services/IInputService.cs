namespace IOServices
{
    public interface IInputService
    {
        string? GetStringFromUserConsole();
        string? GetFromFile(string filePath);
    }
}