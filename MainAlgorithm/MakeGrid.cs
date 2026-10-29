public class MakeGrid
{
    public Grid2D Generate()
    {
        Grid2D grid = new(20, 20);

        for(int i = 0; i < 20; i++)
        {
            for(int j = 0; j < 20; j++)
            {
                if(i < 15 && j == 10)
                    grid.SetValue(i, j, 1);
                else
                    grid.SetValue(i, j, 0);
            }
        }

        return grid;
    }
}