using System.Collections.Generic;
using LabyrinthTask.Domain;

namespace LabyrinthTask.Services
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