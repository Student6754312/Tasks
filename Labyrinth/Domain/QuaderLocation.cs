namespace Labyrinth.Domain
{
    public struct QuaderLocation
    {
        public int X { get;}
        public int Y { get;}
        public int Z { get;}

        public QuaderLocation(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}