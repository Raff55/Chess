using ChessLib.Figures;
namespace ChessLib;

public class Bishop : Figure, IFigure
{
    public Bishop()
    {
        Coords = new Coordinate(0, 0);
        Color = 'W';
    }

    public Bishop(Coordinate coord, char color)
    {
        Coords = coord;
        Color = color;
    }

    public bool MoveTo(Coordinate destination, List<Coordinate> coordinates)
    {
        if(Math.Abs(destination.X - Coords.X) == Math.Abs(destination.Y - Coords.Y))
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
        bool upRight = true;
        bool upLeft = true;
        bool downRight = true;
        bool downLeft = true;
        for (int i = 1;i < 9;i++)
        {
            for(int j = 1;j < 9;j++)
            {
                if (i == Coords.X && j == Coords.Y)
                {
                    if (Color == 'W')
                        board[i, j] = 'B';
                    else board[i, j] = 'b';
                }
                else if (i < Coords.X && j > Coords.Y && i + j == Coords.X + Coords.Y && upRight)
                {
                    if (figuresW.Contains(board[i, j]) || figuresB.Contains(board[i, j]))
                    {
                        upRight = false;
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
            }
        }
        return board;
    }

    public char[,] Step(char[,] board, ref List<Coordinate> AvailableSteps)
    {
        bool upRight = true;
        bool upLeft = true;
        bool downRight = true;
        bool downLeft = true;
        for (int i = 1; i < 9; i++)
        {
            for (int j = 1; j < 9; j++)
            {
                if (i == Coords.X && j == Coords.Y)
                {
                    if (Color == 'W')
                        board[i, j] = 'B';
                    else board[i, j] = 'b';
                }
                else if (i < Coords.X && j > Coords.Y && i + j == Coords.X + Coords.Y && upRight)
                {
                    CheckPlace(i, j, ref board, ref AvailableSteps, ref upRight);
                }
                else if (i < Coords.X && j < Coords.Y && i - j == Coords.X - Coords.Y && upLeft)
                {
                    CheckPlace(i, j, ref board, ref AvailableSteps, ref upLeft);
                }
                else if (i > Coords.X && j > Coords.Y && (i - j == Coords.X - Coords.Y) && downRight)
                {
                    CheckPlace(i, j, ref board, ref AvailableSteps, ref downRight);
                }
                else if (i > Coords.X && j < Coords.Y && i + j == Coords.X + Coords.Y && downLeft)
                {
                    CheckPlace(i, j, ref board, ref AvailableSteps, ref downLeft);
                }
            }
        }
        return board;
    }
}