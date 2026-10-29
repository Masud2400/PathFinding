public class PathFinding
{
    public PathNode CreateNode(
        (int y, int x) position,
        PathNode? parent, 
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

    public double CalculateHeuristic((int y, int x) posOne, (int y, int x) posTwo)
    {
        return Math.Sqrt(
            Math.Pow((posTwo.y - posOne.y), 2) + Math.Pow((posTwo.x - posOne.x), 2)
        );
    }

    public List<(int, int)> GetValidNeighbours(Grid2D grid, (int y, int x) position)
    {
        int y = position.y;
        int x = position.x;

        int rows = grid.cells.GetLength(0);
        int cols = grid.cells.GetLength(1);

        var possibleMoves = new (int Y, int X)[]
        {
            (y + 1, x),     (y - 1, x),     // Right, Left
            (y,     x + 1), (y,     x - 1), // Up, Down
            (y + 1, x + 1), (y - 1, x - 1), // Diagonal moves
            (y + 1, x - 1), (y - 1, x + 1)
        };

        var neighbours = new List <(int y, int x)>();

        foreach(var (Y, X) in possibleMoves)
        {
            int ny = Y;
            int nx = X;

            if (
                0 <= ny && ny < rows && 
                0 <= nx && nx < cols && 
                grid.GetValue(ny, nx) == 0
            )
                neighbours.Add((ny, nx));
        }

        return neighbours;
    }

    public List<(int y, int x)> ReconstructPath(PathNode goalNode)
    {
        List<(int y, int x)> path = [];
        PathNode? current = goalNode;

        while(current != null)
        {
            path.Add(current.position);
            current = current.parent;
        }

        path.Reverse();

        return path;
    }

    public List<(int y, int x)> FindPath(
        Grid2D grid, (int y, int x) start, (int y, int x) goal
    )
    {
        PathNode startNode = CreateNode(
            position : start, 
            g : 0, 
            h : CalculateHeuristic(start, goal),
            parent : null
        );

        var openList = new PriorityQueue<(int y, int x), double>([(start, startNode.f)]);
        var openDict = new Dictionary<(int y, int x), PathNode>() { {start, startNode} };
        var closedSet = new HashSet<(int y, int x)>();

        while(openList.Count > 0)
        {
            (int y, int x) currentPos = openList.Dequeue();
            PathNode currentNode = openDict[currentPos];

            if(currentPos == goal)
                return ReconstructPath(currentNode);

            closedSet.Add(currentPos);

            foreach(var neighbourPos in GetValidNeighbours(grid, currentPos))
            {
                if(closedSet.Contains(neighbourPos))
                    continue;

                double tentativeG = currentNode.g + 
                    CalculateHeuristic(currentPos, neighbourPos);
                
                if(!openDict.ContainsKey(neighbourPos))
                {
                    PathNode neighbour = CreateNode(
                        position : neighbourPos,
                        g : tentativeG,
                        h : CalculateHeuristic(neighbourPos, goal),
                        parent : currentNode
                    );
                    openList.Enqueue(neighbourPos, neighbour.f);
                    openDict[neighbourPos] = neighbour;
                }
                else if(tentativeG < openDict[neighbourPos].g)
                {
                    PathNode neighbour = openDict[neighbourPos];
                    neighbour.g = tentativeG;
                    neighbour.f = tentativeG + neighbour.h;
                    neighbour.parent = currentNode;
                }
            }
        }

        return [];
    }
}