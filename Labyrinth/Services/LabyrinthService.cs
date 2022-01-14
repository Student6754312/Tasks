using System;
using System.Collections.Generic;
using Labyrinth.Domain;

namespace Labyrinth.Services
{
    public class LabyrinthService : ILabyrinthService
    {
        private IInputStringService _inputStringService;
        private ILabyrinth _labyrinth;
        private IOutputStringService _outputService;

        public LabyrinthService(IInputStringService inputStringService, IOutputStringService iOutputService)
        {
            _inputStringService = inputStringService;
            _outputService = iOutputService;
        }

        public void CreateLabyrinth(ILabyrinth labyrinth)
        {
            _labyrinth = labyrinth;

            for (int i = 0; i < labyrinth.L; i++)
            {
                _outputService.ConsoleOutupt("\n");

                for (int j = 0; j < labyrinth.R; j++)
                {
                    string? inputString = _inputStringService.GetStringFromUser();

                    for (int k = 0; k < labyrinth.C; k++)
                    {
                        if (inputString != null && inputString.Length == labyrinth.C)
                        {
                            _labyrinth.LabyrinthArray[i, j, k] = new Quader(inputString[k], new QuaderLocation(i, j, k));
                        }
                        else
                        {
                            throw new FormatException($"Wrong Number of Quaders in a row '{inputString}'");
                        }
                    }
                }
            }
        }

        public bool BreadthFirstSearch(ILabyrinth labyrinth, out int time)
        {
            time = 1;
            var quadersQueue = new Queue<IQuader>();

            var startQuader = FindQuader(QuaderTypes.Start, labyrinth) ?? throw new FormatException("Not Found 'S' Quader");

            quadersQueue.Enqueue(startQuader);

            while (quadersQueue.Count > 0)
            {
                var currentQuader = quadersQueue.Dequeue();
                
                var adjacencyList = CreateAdjacencyList(currentQuader, labyrinth);

                foreach (var quaderLocation in adjacencyList)
                {
                    time++;

                    var nextQuader = labyrinth.LabyrinthArray[quaderLocation.X, quaderLocation.Y, quaderLocation.Z];
                    
                    if (nextQuader.Type == QuaderTypes.Exit) return true;
                    
                    if (nextQuader.Type == QuaderTypes.Air)
                    {
                        nextQuader.Value = currentQuader.Value + 1;
                        nextQuader.Type = QuaderTypes.Visited;
                        quadersQueue.Enqueue(nextQuader);
                    }
                }
            }
            return false;
        }

        public List<QuaderLocation> CreateAdjacencyList(IQuader quader, ILabyrinth labyrinth)
        {
            QuaderLocation quaderLocation = quader.Location;

            var adjacencyList = new List<QuaderLocation>();

            int x = quaderLocation.X;
            int y = quaderLocation.Y;
            int z = quaderLocation.Z;
            
            if (x + 1 < labyrinth.L)
            {
                adjacencyList.Add(new QuaderLocation(x + 1, y, z));
            }
            if (y + 1 < labyrinth.R)
            {
                adjacencyList.Add(new QuaderLocation(x, y + 1, z));
            }
            if (z + 1 < labyrinth.C)
            {
                adjacencyList.Add(new QuaderLocation(x, y, z + 1));
            }
            if (quaderLocation.X - 1 >= 0)
            {
                adjacencyList.Add(new QuaderLocation(x - 1, y, z));
            }
            if (quaderLocation.Y - 1 >= 0)
            {
                adjacencyList.Add(new QuaderLocation(x, y - 1, z));
            }
            if (quaderLocation.Z - 1 >= 0)
            {
                adjacencyList.Add(new QuaderLocation(x, y, z - 1));
            }
            return adjacencyList;
        }

        public IQuader? FindQuader(QuaderTypes quaderType, ILabyrinth labyrinth)
        {
            for (int i = 0; i < labyrinth.L; i++)
            {
                for (int j = 0; j < labyrinth.R; j++)
                {
                    for (int k = 0; k < labyrinth.C; k++)
                    {
                        if (labyrinth.LabyrinthArray[i, j, k].Type == quaderType)
                        {
                            return labyrinth.LabyrinthArray[i, j, k];
                        }
                    }
                }
            }
            return null;
        }

        public void PrintLabyrinth(ILabyrinth labyrinth)
        {
            for (int i = 0; i < labyrinth.L; i++)
            {
                _outputService.ConsoleOutupt(Environment.NewLine);
                for (int j = 0; j < labyrinth.R; j++)
                {
                    for (int k = 0; k < labyrinth.C; k++)
                    {
                        _outputService.ConsoleOutupt(labyrinth.LabyrinthArray[i, j, k].View.ToString());
                    }
                    _outputService.ConsoleOutupt(Environment.NewLine);
                }
            }
        }

        public int FindShortestPath(ILabyrinth labyrinth)
        {
            var exitQuader = FindQuader(QuaderTypes.Exit, labyrinth) ?? throw new FormatException("Not Found 'E' Quader");

            var adjacencyList = CreateAdjacencyList(exitQuader, labyrinth);

            int minTime = Int32.MaxValue;

            foreach (var quaderLocation in adjacencyList)
            {
                int time = labyrinth.LabyrinthArray[quaderLocation.X, quaderLocation.Y, quaderLocation.Z].Value;
                if (time > 0 && time < minTime)
                {
                    minTime = time;
                }
            }

            return minTime;
        }
    }
}