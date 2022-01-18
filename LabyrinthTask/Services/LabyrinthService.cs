using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IOServices.Interfaces;
using IOServices.ServiceFactory;
using LabyrinthTask.Domain;

namespace LabyrinthTask.Services
{
    public class LabyrinthService : ILabyrinthService
    {
        private readonly IInputService _inputService;
        private readonly IOutputService _outputService;

        public LabyrinthService(IInputServiceFactory inputSelectionFactory, IOutputService outputService)
        {
            _inputService = inputSelectionFactory.GetService();
            _outputService = outputService;
        }

        public void CreateLabyrinth(ILabyrinth labyrinth)
        {
            for (int i = 0; i < labyrinth.L; i++)
            {
                _outputService.Output("\n");

                for (int j = 0; j < labyrinth.R; j++)
                {
                    string? inputString = _inputService.Input();

                    if (inputString == null || inputString.Length != labyrinth.C)
                    {
                        throw new FormatException($"Wrong Number of Quaders in a row(L = {i + 1}, R = {j + 1}) - '{inputString}'");
                    }

                    for (int k = 0; k < labyrinth.C; k++)
                    {
                        labyrinth.LabyrinthArray[i, j, k] = new Quader(inputString[k], new QuaderLocation(i, j, k));
                    }
                }
            }
        }

        public bool BreadthFirstSearch(ILabyrinth labyrinth, out List<IQuader> shortestPathList)
        {
            shortestPathList = new List<IQuader>();

            var quadersQueue = new Queue<IQuader>();

            var startQuader = FindQuader(QuaderTypes.Start, labyrinth) ?? throw new FormatException("Not Found 'S' Quader");

            quadersQueue.Enqueue(startQuader);

            while (quadersQueue.Count > 0)
            {
                var currentQuader = quadersQueue.Dequeue();

                var adjacencyList = CreateAdjacencyList(currentQuader, labyrinth);

                foreach (var quader in adjacencyList)
                {
                    if (quader.Type == QuaderTypes.Exit)
                    {
                        shortestPathList = FindShortestPath(quader, shortestPathList, labyrinth);
                        return true;
                    }

                    if (quader.Type == QuaderTypes.Air)
                    {
                        quader.Value = currentQuader.Value + 1;
                        quader.Type = QuaderTypes.Visited;
                        quadersQueue.Enqueue(quader);
                    }
                }
            }
            return false;
        }

        public List<IQuader> CreateAdjacencyList(IQuader quader, ILabyrinth labyrinth)
        {
            var adjacencyList = new List<IQuader>();

            int x = quader.Location.X;
            int y = quader.Location.Y;
            int z = quader.Location.Z;

            if (x + 1 < labyrinth.L)
            {
                adjacencyList.Add(labyrinth.LabyrinthArray[x + 1, y, z]);
            }
            if (y + 1 < labyrinth.R)
            {
                adjacencyList.Add(labyrinth.LabyrinthArray[x, y + 1, z]);
            }
            if (z + 1 < labyrinth.C)
            {
                adjacencyList.Add(labyrinth.LabyrinthArray[x, y, z + 1]);
            }
            if (quader.Location.X - 1 >= 0)
            {
                adjacencyList.Add(labyrinth.LabyrinthArray[x - 1, y, z]);
            }
            if (quader.Location.Y - 1 >= 0)
            {
                adjacencyList.Add(labyrinth.LabyrinthArray[x, y - 1, z]);
            }
            if (quader.Location.Z - 1 >= 0)
            {
                adjacencyList.Add(labyrinth.LabyrinthArray[x, y, z - 1]);
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
                _outputService.Output("");

                for (int j = 0; j < labyrinth.R; j++)
                {
                    StringBuilder row = new StringBuilder();
                    for (int k = 0; k < labyrinth.C; k++)
                    {
                        row.Append(labyrinth.LabyrinthArray[i, j, k].View);
                    }
                    _outputService.Output(row.ToString());
                }
            }
        }

        private List<IQuader> FindShortestPath(IQuader endQuader, List<IQuader> shortestPathList, ILabyrinth labyrinth)
        {
            shortestPathList.Add(endQuader);

            if (endQuader.Type == QuaderTypes.Start)
            {
                return shortestPathList;
            }

            var adjacencyList = CreateAdjacencyList(endQuader, labyrinth);

            IQuader goalQuader = adjacencyList.Where(q => q.Value > 0).OrderBy(q => q.Value).First();

            return FindShortestPath(goalQuader, shortestPathList, labyrinth);

        }
    }
}