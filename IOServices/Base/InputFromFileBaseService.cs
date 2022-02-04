using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using IOServices.Interfaces;

namespace IOServices.Base
{
    public abstract class InputFromFileBaseService : IInputService 
    {
        private readonly IFileSystem _fileSystem;
        private readonly IInputOutputSettings _inputOutputSettings;
        private List<string>? _fileStringsList;

        private int _index;
        
        protected InputFromFileBaseService(IInputOutputSettings inputOutputSettings, IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
            _inputOutputSettings = inputOutputSettings;
        }

        private List<string> LoadInputFile(string? filePath)
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
            _fileStringsList ??= LoadInputFile(_inputOutputSettings.InputFilePath);

            if (_fileStringsList.Count == 0)
            {
                throw new FormatException($"File {_inputOutputSettings.InputFilePath} is Empty");
            }
            if (_index >= _fileStringsList.Count)
            {
                _index = 0;
            }
            return _fileStringsList[_index++];
        }
    }
}