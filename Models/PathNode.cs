public class PathNode
{
    public (int y, int x) position;
    public double g;
    public double h;
    public double f;
    public PathNode? parent;
}