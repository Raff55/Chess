using ChessLib.Figures;
namespace ChessLib;

public class Knight : Figure, IFigure
{
    public Knight()
    {
        Coords = new Coordinate(0, 0);
        Color = 'W';
    }

    public Knight(Coordinate coord, char color)
    {
        Coords = coord;
        Color = color;
    }

    public Knight(Knight obj)
    {
        Coords = obj.Coords;
        Color = obj.Color;
    }

    public bool MoveTo(Coordinate destination, List<Coordinate> coordinates)
    {
        if ((Math.Abs(Coords.X - destination.X) == 2 && Math.Abs(Coords.Y - destination.Y) == 1) || (Math.Abs(Coords.X - destination.X) == 1 && Math.Abs(Coords.Y - destination.Y) == 2))
        {
            Coords = destination;
            return true;
        }
        else
        {
            return false;
        }
    }

    public char[,] WrongPlane(char[,] arr, Coordinate coord)
    {
        arr[coord.X, coord.Y] = 'X';
        return arr;
    }

    public char[,] Step(char[,] board)
    {
        for (int i = 1; i < 9; i++)
        {
            for (int j = 1; j < 9; j++)
            {
                if (i == Coords.X && j == Coords.Y)
                {
                    board[i, j] = 'N';
                }
                else if ((i + 2 == Coords.X || i - 2 == Coords.X) && (!figuresB.Contains(board[i, j]) && !figuresW.Contains(board[i, j])))
                {
                    if ((j + 1 == Coords.Y || j - 1 == Coords.Y) && (!figuresB.Contains(board[i, j]) && !figuresW.Contains(board[i, j])))
                    {
                        board[i, j] = '$';
                    }
                }
                else if ((i + 1 == Coords.X || i - 1 == Coords.X) && (!figuresB.Contains(board[i, j]) && !figuresW.Contains(board[i, j])))
                {
                    if ((j + 2 == Coords.Y || j - 2 == Coords.Y) && (!figuresB.Contains(board[i, j]) && !figuresW.Contains(board[i, j])))
                    {
                        board[i, j] = '$';
                    }
                }
            }
        }
        return board;
    }

    public char[,] Step(char[,] board, ref List<Coordinate> AvailableSteps)
    {
        bool isValid = true;
        for (int i = 1; i < 9; i++)
        {
            for (int j = 1; j < 9; j++)
            {
                if (i == Coords.X && j == Coords.Y)
                {
                    if (Color == 'W')
                        board[i, j] = 'N';
                    else board[i, j] = 'n';
                }
                else if ((i + 2 == Coords.X || i - 2 == Coords.X))
                {
                    if ((j + 1 == Coords.Y || j - 1 == Coords.Y))
                    {
                        CheckPlace(i, j, ref board, ref AvailableSteps, ref isValid);
                    }
                }
                else if ((i + 1 == Coords.X || i - 1 == Coords.X))
                {
                    if ((j + 2 == Coords.Y || j - 2 == Coords.Y))
                    {
                        CheckPlace(i, j, ref board, ref AvailableSteps, ref isValid);
                    }
                }
            }
        }
        return board;
    }

    public string StepsCount(Coordinate destinationCoordinate)
    {
        List<Coordinate> currentCoord = new List<Coordinate>();
        currentCoord.Add(Coords);

        int[,] stepscount = new int[9, 9];
        for (int i = 1; i < 9; i++)
        {
            for (int j = 1; j < 9; j++)
            {
                stepscount[i, j] = -1;
            }
        }
        stepscount[Coords.X, Coords.Y] = 0;
        while (currentCoord.Count > 0)
        {
            List<Coordinate> coordinate1 = CalculateSteps(Coords);
            currentCoord.AddRange(coordinate1);

            foreach (var c in currentCoord)
            {
                if (stepscount[c.X, c.Y] == -1)
                {
                    stepscount[c.X, c.Y] = stepscount[Coords.X, Coords.Y] + 1;
                }
                if (c.Y == destinationCoordinate.Y && c.X == destinationCoordinate.X)
                {

                    return $"Minimum steps count is {stepscount[destinationCoordinate.X, destinationCoordinate.Y]}";
                }
            }
            Coords = currentCoord[0];
            currentCoord.RemoveAt(0);
        }
        return " ";
    }

    public static List<Coordinate> CalculateSteps(Coordinate coords)
    {
        List<Coordinate> coordinate2 = new List<Coordinate>();
        for (int i = 1; i < 9; i++)
        {
            for (int j = 1; j < 9; j++)
            {
                if (i + 2 == coords.X || i - 2 == coords.X)
                {
                    if (j + 1 == coords.Y || j - 1 == coords.Y)
                    {
                        Coordinate coordinate = new Coordinate();
                        coordinate.X = i;
                        coordinate.Y = j;
                        coordinate2.Add(coordinate);
                    }
                }
                else if (i + 1 == coords.X || i - 1 == coords.X)
                {
                    if (j + 2 == coords.Y || j - 2 == coords.Y)
                    {
                        Coordinate coordinate = new Coordinate();
                        coordinate.X = i;
                        coordinate.Y = j;
                        coordinate2.Add(coordinate);
                    }
                }
            }
        }
        return coordinate2;
    }
}