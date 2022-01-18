using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Reflection;
using IOServices.Interfaces;
using Microsoft.Extensions.Options;

namespace IOServices.Base
{
    public abstract class InputFromFileBaseService<TA> : IInputService where TA : class
    {
        private readonly List<string> _fileStringsList;
        private readonly IFileSystem _fileSystem;
        private readonly TA _applicationSettings;
        private readonly string _filePath;

        private int _index = 0;

        
        public InputFromFileBaseService(IOptions<TA> options, IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
            _applicationSettings = options.Value;
            _filePath = GetFilePath(_applicationSettings);
            _fileStringsList = LoadInputFile(_filePath);
        }

        private List<string> LoadInputFile(string filePath)
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
            if (_fileStringsList.Count == 0)
            {
                throw new FormatException($"File {_filePath} is Empty");
            }
            if (_index >= _fileStringsList.Count)
            {
                _index = 0;
            }
            return _fileStringsList[_index++];
        }

        private string GetFilePath(TA appSettings)
        {
            Type type = _applicationSettings.GetType();
            PropertyInfo propertyInfo = type.GetProperty($"InputFilePath");
            var value = propertyInfo.GetValue(_applicationSettings).ToString();
            
            return value;
        }
    }
}