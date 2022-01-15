using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;

namespace IOServices
{
    public class InputFromFileService : IInputService
    {
        private List<string> _fileStringsList = new ();
        private int _index = 0;

        private IOutputService _outputService;
        
        public InputFromFileService(IOutputService outputService)
        { 
            _outputService = outputService;

            var filePath = "input.txt";
            
            if (File.Exists(filePath))
            {
                using (StreamReader streamReader = new FileSystem().File.OpenText(filePath))
                {
                    string? line;
                    while ((line = streamReader?.ReadLine()?.Trim()) != null)
                    {
                        if (!string.IsNullOrEmpty(line)) _fileStringsList.Add(line);
                    }
                }
            }
        }

        public string? Input()
        {
            if (_index >= _fileStringsList.Count)
            {
                _index = 0;
            }
            var str = _fileStringsList[_index++];
            _outputService.ConsoleOutputLine($"{str}\n");
            return str;
        }
    }
}