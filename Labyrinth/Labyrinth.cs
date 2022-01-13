using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Labyrinth
{
    public class Labyrinth
    {
        private readonly int _l;
        private readonly int _r;
        private readonly int _c;
        public IQuader[,,] LabyrinthArray {get;}
        
        public Labyrinth(int l, int r, int c)
        {
            _l = l;
            _r = r;
            _c = c;
            LabyrinthArray = new IQuader[l, r, c]; 
        }

        public void CreateLabyrinth()
        {
            for (int i = 0; i < _l; i++)
            {
                Console.WriteLine();
                for (int j = 0; j < _r; j++)
                {
                    string? inputString = Console.ReadLine();

                    for (int k = 0; k < _c; k++)
                    {
                        if (inputString != null && inputString.Length == _c)
                        {
                            LabyrinthArray[i, j, k] = new Quader(inputString[k], new QuaderLocation(i, j, k));
                        }
                        else
                        {
                            throw new FormatException($"Wrong Number of Quaders in a row '{inputString}'");
                        }
                    }
                }
            }
        }

        public void BreadthFirstSearch()
        {
            var quadersQueue = new Queue<IQuader>();

            var startQuader = FindQuader(QuaderTypes.Start) ?? throw new FormatException("Not Found 'S' Quader");

            quadersQueue.Enqueue(startQuader);

            while (quadersQueue.Count > 0)
            {
                var currentQuader = quadersQueue.Dequeue();
                var adjacencyList = CreateAdjacencyList(currentQuader);

                foreach (var quaderlocation in adjacencyList)
                {
                    var nextQuader = LabyrinthArray[quaderlocation.X, quaderlocation.Y, quaderlocation.Z];
                    if (nextQuader.Value == 0)
                    {
                        nextQuader.Value = currentQuader.Value + 1;
                        quadersQueue.Enqueue(nextQuader);
                    }
                }
            }
        }

        public int FindShortestPath()
        {
            var exitQuader = FindQuader(QuaderTypes.Exit) ?? throw new FormatException("Not Found 'E' Quader");

            var adjacencyList = CreateAdjacencyList(exitQuader);

            int minTime = Int32.MaxValue;

            foreach (var quaderLocation in adjacencyList)
            {
                int time = LabyrinthArray[quaderLocation.X, quaderLocation.Y, quaderLocation.Z].Value;
                if (time > 0 && time < minTime)
                {
                    minTime = time;
                }
            }

            return minTime;
        }

        private List<QuaderLocation> CreateAdjacencyList(IQuader quader)
        {
            QuaderLocation quaderLocation = quader.Location;

            var adjacencyList = new List<QuaderLocation>();

            if (quaderLocation.X + 1 < LabyrinthArray.GetLength(0))
            {
                adjacencyList.Add(new QuaderLocation(quaderLocation.X + 1, quaderLocation.Y, quaderLocation.Z));
            }
            if (quaderLocation.Y + 1 < LabyrinthArray.GetLength(1))
            {
                adjacencyList.Add(new QuaderLocation(quaderLocation.X, quaderLocation.Y + 1, quaderLocation.Z));
            }
            if (quaderLocation.Z + 1 < LabyrinthArray.GetLength(2))
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
        
        public IQuader? FindQuader(QuaderTypes quaderType)
        {
            for (int i = 0; i < _l; i++)
            {
                for (int j = 0; j < _r; j++)
                {
                    for (int k = 0; k < _c; k++)
                    {
                        if (LabyrinthArray[i, j, k].Value == (int)quaderType)
                        {
                            return LabyrinthArray[i, j, k];
                        }
                    }
                }
            }
            return null;
        }

        public void PrintLabyrinth()
        {
            for (int i = 0; i < _l; i++)
            {
                Console.WriteLine();
                for (int j = 0; j < _r; j++)
                {
                    for (int k = 0; k < _c; k++)
                    {
                        Console.Write(LabyrinthArray[i, j, k].Value);
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}