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

        public void BreadthFirstSearch(ILabyrinth labyrint)
        {
            var quadersQueue = new Queue<IQuader>();

            var startQuader = FindQuader(QuaderTypes.Start, labyrint) ?? throw new FormatException("Not Found 'S' Quader");

            quadersQueue.Enqueue(startQuader);

            while (quadersQueue.Count > 0)
            {
                var currentQuader = quadersQueue.Dequeue();
                var adjacencyList = CreateAdjacencyList(currentQuader, labyrint);

                foreach (var quaderlocation in adjacencyList)
                {
                    var nextQuader = labyrint.LabyrinthArray[quaderlocation.X, quaderlocation.Y, quaderlocation.Z];
                    if (nextQuader.Value == 0)
                    {
                        nextQuader.Value = currentQuader.Value + 1;
                        quadersQueue.Enqueue(nextQuader);
                    }
                }
            }
        }

        public List<QuaderLocation> CreateAdjacencyList(IQuader quader, ILabyrinth labyrint)
        {
            QuaderLocation quaderLocation = quader.Location;

            var adjacencyList = new List<QuaderLocation>();

            if (quaderLocation.X + 1 < labyrint.LabyrinthArray.GetLength(0))
            {
                adjacencyList.Add(new QuaderLocation(quaderLocation.X + 1, quaderLocation.Y, quaderLocation.Z));
            }
            if (quaderLocation.Y + 1 < labyrint.LabyrinthArray.GetLength(1))
            {
                adjacencyList.Add(new QuaderLocation(quaderLocation.X, quaderLocation.Y + 1, quaderLocation.Z));
            }
            if (quaderLocation.Z + 1 < labyrint.LabyrinthArray.GetLength(2))
            {
                adjacencyList.Add(new QuaderLocation(quaderLocation.X, quaderLocation.Y, quaderLocation.Z + 1));
            }
            if (quaderLocation.X - 1 >= 0)
            {
                adjacencyList.Add(new QuaderLocation(quaderLocation.X - 1, quaderLocation.Y, quaderLocation.Z));
            }
            if (quaderLocation.Y - 1 >= 0)
            {
                adjacencyList.Add(new QuaderLocation(quaderLocation.X, quaderLocation.Y - 1, quaderLocation.Z));
            }
            if (quaderLocation.Z - 1 >= 0)
            {
                adjacencyList.Add(new QuaderLocation(quaderLocation.X, quaderLocation.Y, quaderLocation.Z - 1));
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
                        if (labyrinth.LabyrinthArray[i, j, k].Value == (int)quaderType)
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
                        _outputService.ConsoleOutupt(labyrinth.LabyrinthArray[i, j, k].Type.ToString());
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