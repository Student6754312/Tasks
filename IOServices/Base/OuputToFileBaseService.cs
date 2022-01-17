using System.IO;
using System.IO.Abstractions;

namespace IOServices.Base
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
    }
}