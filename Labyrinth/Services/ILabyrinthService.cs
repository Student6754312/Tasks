using System.Collections.Generic;
using Labyrinth.Domain;

namespace Labyrinth.Services
{
    public interface ILabyrinthService
    {
        void CreateLabyrinth(ILabyrinth labyrinth);
        bool BreadthFirstSearch(ILabyrinth labyrinth, out int time);
        List<QuaderLocation> CreateAdjacencyList(IQuader quader, ILabyrinth labyrinth);
        IQuader? FindQuader(QuaderTypes quaderType, ILabyrinth labyrinth);
        void PrintLabyrinth(ILabyrinth labyrinth);
        int FindShortestPath(ILabyrinth labyrinth);
    }
}