namespace Labyrinth.Domain
{
    public interface IQuader
    {
        char View { get; }
        public QuaderTypes Type { get; set; }
        int Value { get; set; }
        QuaderLocation Location { get; }
    }
}