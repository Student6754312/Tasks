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
        private readonly string _filePath;
        private readonly TA _applicationSettings;

        public OutputToFileBaseService(IOptions<TA> options, FileSystem fileSystem)
        {
            _fileSystem = fileSystem;
            _applicationSettings = options.Value;
            _filePath = CreateFile(GetFilePath(_applicationSettings));
        }

       private string CreateFile(string filePath = "output.txt")
        {
            using (StreamWriter streamWriter = _fileSystem.File.CreateText(filePath))
            {
                streamWriter.Close();
                return filePath;
            }
        }

        public virtual void Output(string str)
        {
            using (StreamWriter streamWriter = _fileSystem.File.AppendText(_filePath))
            {
                streamWriter.WriteLine(str);
                streamWriter.Close();
            }
        }

        private string GetFilePath(TA appSettings)
        {
            Type type = _applicationSettings.GetType();
            PropertyInfo propertyInfo = type.GetProperty($"OutputFilePath");
            return propertyInfo.GetValue(_applicationSettings).ToString();
        }
    }
}