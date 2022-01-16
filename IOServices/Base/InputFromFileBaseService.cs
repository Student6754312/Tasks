using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Reflection;

namespace IOServices
{
    public class InputFromFileBaseService : IInputService
    {
        private readonly List<string> _fileStringsList;
        private readonly IFileSystem _fileSystem;
        private int _index = 0;

        public InputFromFileBaseService() : this(new FileSystem()) { }

        public InputFromFileBaseService(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
            _fileStringsList = LoadInputFile();
        }

        public List<string> LoadInputFile(string filePath = "input.txt")
        {
            var fileStringsList = new List<string>();

            if (!_fileSystem.File.Exists(filePath))
            {
                throw new FileNotFoundException($"Input File {filePath} Not Found");
            }

            using (StreamReader streamReader = _fileSystem.File.OpenText(filePath))
            {
                string? line;
                while ((line = streamReader?.ReadLine()?.Trim()) != null)
                {
                    if (!string.IsNullOrEmpty(line)) fileStringsList.Add(line);
                }
            }

            return fileStringsList;
        }

        public virtual string? Input()
        {
            if (_index >= _fileStringsList.Count)
            {
                _index = 0;
            }
            return _fileStringsList[_index++];
        }
    }
}