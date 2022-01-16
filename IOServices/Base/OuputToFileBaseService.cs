using System;
using System.IO;
using System.IO.Abstractions;

namespace IOServices
{
    public class OutputToFileBaseService : IOutputService
    {
        private readonly IFileSystem _fileSystem;
        private readonly string _filePath;

        public OutputToFileBaseService() : this(new FileSystem()) { }

        public OutputToFileBaseService(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
            _filePath = CreateFile();
        }

        public string CreateFile(string filePath = "outputfile.txt")
        {
            using (_fileSystem.File.CreateText(filePath))
            {
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
    }
}