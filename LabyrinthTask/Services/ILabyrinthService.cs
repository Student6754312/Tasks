using System.Collections.Generic;
using Labyrinth.Domain;

namespace Labyrinth.Services
{
    public interface ILabyrinthService
    {
        void CreateLabyrinth(ILabyrinth labyrinth);
        bool BreadthFirstSearch(ILabyrinth labyrinth, out List<IQuader>shortestPathList);
        List<IQuader> CreateAdjacencyList(IQuader quader, ILabyrinth labyrinth);
        IQuader? FindQuader(QuaderTypes quaderType, ILabyrinth labyrinth);
        void PrintLabyrinth(ILabyrinth labyrinth);
    }
}