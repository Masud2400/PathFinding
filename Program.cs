public class Grid2D(int width, int height)
{
    public int[,] cells = new int[width, height];

    public void SetValue(int x, int y, int value)
    {
        cells[x, y] = value;
    }

    public int GetValue(int x, int y)
    {
        return cells[x, y];
    }
}

public class PathNode
{
    public required int[] position;
    public double g;
    public double h;
    public double f;
    public required Dictionary<int, int>? parent;
}

public class PathFinding
{
    public PathNode CreateNode(
        int[] position,
        Dictionary<int, int>? parent = null, 
        double g = float.PositiveInfinity,
        double h = 0
    )
    {
        return new PathNode
        { 
            position = position, 
            g = g,
            h = h,
            f = g + h,
            parent = parent
        };
    }

    public double CalculateHeuristic(int[] posOne, int[] posTwo)
    {
        return Math.Sqrt(
            Math.Pow((posTwo[0] - posOne[0]), 2) + Math.Pow((posTwo[1] - posOne[1]), 2)
        );
    }

    public List<(int X, int Y)> GetValidNeighbours(Grid2D grid, int[] position)
    {
        int x = position[0];
        int y = position[1];

        int rows = grid.cells.GetLength(0);
        int cols = grid.cells.GetLength(1);

        var possibleMoves = new (int X, int Y)[]
        {
            (x + 1, y),     (x - 1, y),     // Right, Left
            (x,     y + 1), (x,     y - 1), // Up, Down
            (x + 1, y + 1), (x - 1, y - 1), // Diagonal moves
            (x + 1, y - 1), (x - 1, y + 1)
        };

        var neighbours = new List <(int X, int Y)>();

        foreach(var (X, Y) in possibleMoves)
        {
            int nx = X;
            int ny = Y;

            if (
                0 <= nx && nx < rows && 
                0 <= ny && ny < cols && 
                grid.GetValue(nx, ny) == 0
            )
                neighbours.Add((X : nx, Y : ny));
        }

        return neighbours;
    }

    public void ReconstructPath()
    {
        
    }

    public void FindPath(Grid2D grid, int[] start, int[] goal)
    {
        PathNode startNode = CreateNode(
            position : start, 
            g : 0, 
            h : CalculateHeuristic(start, goal)
        );
    }
}

public class Program
{
    public static void Main()
    {

    }
}