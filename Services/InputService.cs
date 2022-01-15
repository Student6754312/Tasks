using System;
using System.IO;
using System.IO.Abstractions;
using System.Text;


namespace IOServices
{

    public class InputService : IInputService
    {

        readonly IFileSystem _fileSystem;

        public InputService() : this(new FileSystem()) { }

        public InputService(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public string? GetStringFromUserConsole()
        {
            return Console.ReadLine()?.Trim();
        }

        public string? GetFromFile(string filePath)
        {
            if (!_fileSystem.File.Exists(filePath)) throw new FileNotFoundException($"File {filePath} not found");
            
            StringBuilder sb = new StringBuilder();

            //using (StreamReader sr = new StreamReader(filePath, System.Text.Encoding.Default))
            using (StreamReader sr = _fileSystem.File.OpenText(filePath))
            {
                string? line;
                while ((line = sr?.ReadLine()?.Trim()) != null)
                {
                    if (!String.IsNullOrEmpty(line)) sb.AppendLine(line);
                }
            }

            return sb.ToString();
        }
    }
}
