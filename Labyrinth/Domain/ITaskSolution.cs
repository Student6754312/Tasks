using System.Collections.Generic;

namespace Labyrinth.Domain
{
    public interface ITaskSolution
    {
        void Input(List<ILabyrinth> labyrinthList);
        void Output(List<ILabyrinth> labyrinthList);
    }
}