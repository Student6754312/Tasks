namespace LabyrinthTask.Domain
{
    public interface ILabyrinth
    {
        int L { get; }
        int R { get; }
        int C { get; }
        IQuader[,,] LabyrinthArray { get; }
    }
}