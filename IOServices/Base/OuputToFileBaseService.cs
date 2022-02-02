using System;
using System.IO;
using System.IO.Abstractions;
using System.Reflection;
using IOServices.Interfaces;
using Microsoft.Extensions.Options;

namespace IOServices.Base
{
    public abstract class OutputToFileBaseService<TA> : IOutputService where TA : class
    {
        private readonly IFileSystem _fileSystem;
        private string? _filePath;
        private readonly TA _applicationSettings;

        public OutputToFileBaseService(IOptions<TA> options, IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
            _applicationSettings = options.Value;
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
            _filePath ??= CreateFile(GetFilePath());
            using StreamWriter streamWriter = _fileSystem.File.AppendText(_filePath);
            streamWriter.WriteLine(str);
            streamWriter.Close();
        }

        private string? GetFilePath()
        {
            Type type = _applicationSettings.GetType();
            PropertyInfo? propertyInfo = type.GetProperty($"OutputFilePath");

            var value = propertyInfo?.GetValue(_applicationSettings)?.ToString();
            
            return value;
        }
    }
}