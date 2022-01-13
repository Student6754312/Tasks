namespace Labyrinth
{
    public interface IQuader
    {
        char Type { get; }
        int Value { get; set; }
        QuaderLocation Location { get; }
    }
}