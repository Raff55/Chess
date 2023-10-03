using ChessLib.Figures;
namespace ChessLib;

public class Rook : Figure, IFigure
{
    public Rook()
    {
        Coords = new Coordinate(0, 0);
        Color = 'W';
    }

    public Rook(Coordinate coord, char color)
    {
        Coords = coord;
        Color = color;
    }

    public Rook(Rook obj)
    {
        Coords = obj.Coords;
        Color = obj.Color;
    }

    public bool MoveTo(Coordinate destination, List<Coordinate> coordinates)
    {
        if (coordinates.Contains(destination))
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
        bool up = true;
        bool down = true;
        bool left = true;
        bool right = true;
        for (int i = 1; i < 9; i++)
        {
            for (int j = 1; j < 9; j++)
            {
                if (i == Coords.X && j == Coords.Y)
                {
                    board[i, j] = 'R';
                }
                else if (i == Coords.X && j > Coords.Y && down)
                {
                    if (figuresW.Contains(board[i, j]) || figuresB.Contains(board[i, j]))
                    {
                        down = false;
                    }
                    else board[i, j] = '$';
                }
                else if (i > Coords.X && j == Coords.Y && right)
                {
                    if (figuresW.Contains(board[i, j]) || figuresB.Contains(board[i, j]))
                    {
                        right = false;
                    }
                    else board[i, j] = '$';
                }
            }
        }
        for (int i = 8; i > 0; i--)
        {
            for (int j = 8; j > 0; j--)
            {
                if (i == Coords.X && j < Coords.Y && up)
                {
                    if (figuresW.Contains(board[i, j]) || figuresB.Contains(board[i, j]))
                    {
                        up = false;
                    }
                    else board[i, j] = '$';
                }
                else if (i < Coords.X && j == Coords.Y && left)
                {
                    if (figuresW.Contains(board[i, j]) || figuresB.Contains(board[i, j]))
                    {
                        left = false;
                    }
                    else board[i, j] = '$';
                }
            }
        }
        return board;
    }

    public char[,] Step(char[,] board, ref List<Coordinate> AvailableSteps)
    {
        bool up = true;
        bool down = true;
        bool left = true;
        bool right = true;
        for (int i = 1; i < 9; i++)
        {
            for (int j = 1; j < 9; j++)
            {
                if (i == Coords.X && j == Coords.Y)
                {
                    if (Color == 'W')
                        board[i, j] = 'R';
                    else board[i, j] = 'r';
                }
                else if (i == Coords.X && j > Coords.Y)
                {
                    CheckPlace(i, j, ref board, ref AvailableSteps, ref down);
                }
                else if (i > Coords.X && j == Coords.Y)
                {
                    CheckPlace(i, j, ref board, ref AvailableSteps, ref right);
                }
            }
        }
        for (int i = 8; i > 0; i--)
        {
            for (int j = 8; j > 0; j--)
            {
                if (i == Coords.X && j < Coords.Y && up)
                {
                    CheckPlace(i, j, ref board, ref AvailableSteps, ref up);
                }
                else if (i < Coords.X && j == Coords.Y && left)
                {
                    CheckPlace(i, j, ref board, ref AvailableSteps, ref left);
                }
            }
        }
        return board;
    }
}