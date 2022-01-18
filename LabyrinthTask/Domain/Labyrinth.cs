namespace LabyrinthTask.Domain
{
    public class Labyrinth : ILabyrinth
    {
        public int L { get;}
        public int R { get;}
        public int C { get; }
        public IQuader[,,] LabyrinthArray {get;}
        
        public Labyrinth(int l, int r, int c)
        {
            L = l;
            R = r;
            C = c;
            
            LabyrinthArray = new IQuader[L, R, C]; 
        }
    }
}