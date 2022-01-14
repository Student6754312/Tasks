using System.Collections.Generic;
using Labyrinth.Domain;

namespace Labyrinth.Services
{
    public interface ILabyrinthService
    {
        void CreateLabyrinth(ILabyrinth labyrinth);
        void BreadthFirstSearch(ILabyrinth labyrint);
        List<QuaderLocation> CreateAdjacencyList(IQuader quader, ILabyrinth labyrint);
        IQuader? FindQuader(QuaderTypes quaderType, ILabyrinth labyrint);
        void PrintLabyrinth(ILabyrinth labyrinth);
        int FindShortestPath(ILabyrinth labyrinth);
    }
}