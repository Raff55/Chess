using ChessLib.Figures;
namespace ChessLib;

public class Queen : Figure, IFigure
{
    public Queen()
    {
        Coords = new Coordinate(0, 0);
        Color = 'W';
    }

    public Queen(Coordinate coord, char color)
    {
        Coords = coord;
        Color = color;
    }

    public Queen(Queen obj)
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
        bool upRight = true;
        bool upLeft = true;
        bool down = true;
        bool downRight = true;
        bool downLeft = true;
        bool left = true;
        bool right = true;

        for (int i = 1; i < 9; i++)
        {
            for (int j = 1; j < 9; j++)
            {
                if (i == Coords.X && j == Coords.Y)
                {
                    if (Color == 'W')
                        board[i, j] = 'Q';
                    else board[i, j] = 'q';
                }
                else if (i == Coords.X && j > Coords.Y && down)
                {
                    if (figuresW.Contains(board[i, j]) || figuresB.Contains(board[i, j]))
                    {
                        down = false;
                    }
                    else board[i, j] = '$';
                }
                else if (i > Coords.X && j > Coords.Y && (i - j == Coords.X - Coords.Y) && downRight)
                {
                    if (figuresW.Contains(board[i, j]) || figuresB.Contains(board[i, j]))
                    {
                        downRight = false;
                    }
                    else board[i, j] = '$';
                }
                else if (i > Coords.X && j < Coords.Y && i + j == Coords.X + Coords.Y && downLeft)
                {
                    if (figuresW.Contains(board[i, j]) || figuresB.Contains(board[i, j]))
                    {
                        downLeft = false;
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
                else if (i < Coords.X && j < Coords.Y && i - j == Coords.X - Coords.Y && upLeft)
                {
                    if (figuresW.Contains(board[i, j]) || figuresB.Contains(board[i, j]))
                    {
                        upLeft = false;
                    }
                    else board[i, j] = '$';
                }
                else if (i < Coords.X && j > Coords.Y && i + j == Coords.X + Coords.Y && upRight)
                {
                    if (figuresW.Contains(board[i, j]) || figuresB.Contains(board[i, j]))
                    {
                        upRight = false;
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
        bool upRight = true;
        bool upLeft = true;
        bool down = true;
        bool downRight = true;
        bool downLeft = true;
        bool left = true;
        bool right = true;
        for (int i = 1; i < 9; i++)
        {
            for (int j = 1; j < 9; j++)
            {
                if (i == Coords.X && j == Coords.Y)
                {
                    if (Color == 'W')
                        board[i, j] = 'Q';
                    else board[i, j] = 'q';
                }
                else if (i == Coords.X && j > Coords.Y && down)
                {
                    CheckPlace(i, j, ref board, ref AvailableSteps, ref down); 
                }
                else if (i > Coords.X && j > Coords.Y && (i - j == Coords.X - Coords.Y) && downRight)
                {
                    CheckPlace(i, j, ref board, ref AvailableSteps, ref downRight);
                }
                else if (i > Coords.X && j < Coords.Y && i + j == Coords.X + Coords.Y && downLeft)
                {
                    CheckPlace(i, j, ref board, ref AvailableSteps, ref downLeft);
                }
                else if (i > Coords.X && j == Coords.Y && right)
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
                else if (i < Coords.X && j < Coords.Y && i - j == Coords.X - Coords.Y && upLeft)
                {
                    CheckPlace(i, j, ref board, ref AvailableSteps, ref upLeft);
                }
                else if (i < Coords.X && j > Coords.Y && i + j == Coords.X + Coords.Y && upRight)
                {
                    CheckPlace(i, j, ref board, ref AvailableSteps, ref upRight);
                }
            }
        }
        return board;
    }
}