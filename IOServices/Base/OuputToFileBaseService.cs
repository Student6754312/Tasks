using System.IO;
using System.IO.Abstractions;
using IOServices.Interfaces;

namespace IOServices.Base
{
    public abstract class OutputToFileBaseService : IOutputService
    {
        private readonly IFileSystem _fileSystem;
        private string? _filePath;
        private readonly IInputOutputSettings _inputOutputSettings;

        protected OutputToFileBaseService(IInputOutputSettings inputOutputSettings, IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
            _inputOutputSettings = inputOutputSettings;
        }

        private string? CreateFile(string? filePath)
        {
            try
            {
                using StreamWriter streamWriter = _fileSystem.File.CreateText(filePath);
                streamWriter.Close();
                return filePath;
            }
            catch
            {
                throw new FileNotFoundException("Wrong Output File Path in appsettings.json, Or no permission to write");
            }
        }

        public virtual void Output(string str)
        {
            _filePath ??= CreateFile(_inputOutputSettings.OutputFilePath);
            using StreamWriter streamWriter = _fileSystem.File.AppendText(_filePath);
            streamWriter.WriteLine(str);
            streamWriter.Close();
        }
    }
}