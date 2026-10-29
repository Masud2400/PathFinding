using ScottPlot;

public class Program
{
    public static void Main()
    {
        PathFinding pathFinding = new();
        MakeGrid makeGrid = new();

        Grid2D grid = makeGrid.Generate();

        (int y, int x) startPos = (y : 5, x : 7);
        (int y, int x) goalPos = (y : 15, x : 18);

        List<(int y, int x)> path = pathFinding.FindPath(grid, startPos, goalPos);

        Plot visual = new();

        int[] ys = [.. path.Select(p => p.y)];
        int[] xs = [.. path.Select(p => p.x)];

        visual.Add.Scatter(xs, ys);
        visual.SavePng("path.png", 600, 500);

        Console.WriteLine("Chart successfully saved to path.png!");
    }
}