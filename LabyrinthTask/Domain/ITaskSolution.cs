using System.Collections.Generic;

namespace LabyrinthTask.Domain
{
    public interface ITaskSolution
    {
        void Input(List<ILabyrinth> labyrinthList);
        void Output(List<ILabyrinth> labyrinthList);
    }
}