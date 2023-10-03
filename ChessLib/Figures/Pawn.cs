using ChessLib.Figures;
namespace ChessLib;

public class Pawn : Figure, IFigure
{
    public Pawn()
    {
        Coords = new Coordinate(0, 0);
        Color = 'W';
    }

    public Pawn(Coordinate coord, char color)
    {
        Coords = coord;
        Color = color;
    }

    public Pawn(Pawn obj)
    {
        Coords = obj.Coords;
        Color = obj.Color;
    }

    public bool MoveTo(Coordinate destination, List<Coordinate> coordinates)
    {
        if(Color == 'W')
        {
            if(destination.X - Coords.X == 1 && destination.Y == Coords.Y)
            {
                Coords = destination;
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            if (Coords.X - destination.X == 1 && destination.Y == Coords.Y)
            {
                Coords = destination;
                return true;
            }
            else
            {
                return false;
            }
        }
        
    }

    public char[,] WrongPlane(char[,] arr, Coordinate coord)
    {
        arr[coord.X, coord.Y] = 'X';
        return arr;
    }

    public char[,] Step(char[,] board)
    {
        if(Color == 'W')
        {
            for(int i = 1;i < 9;i++)
            {
                for(int j = 1;j < 9;j++)
                {
                    if(i == Coords.X && j == Coords.Y )
                    {
                        board[i, j] = 'P';
                    }
                    else if(i == Coords.X - 1 && j == Coords.Y && (!figuresB.Contains(board[i, j]) && !figuresW.Contains(board[i, j])))
                    {
                        board[i, j] = '$';
                    }
                }
            }
            return board;
        }
        else
        {
            for(int i = 1;i < 9;i++)
            {
                for(int j = 1;j < 9;j++)
                {
                    if(i == Coords.X && j == Coords.Y)
                    {
                        board[i, j] = 'P';
                    }
                    else if(i == Coords.X + 1 && j == Coords.Y && (!figuresB.Contains(board[i, j]) && !figuresW.Contains(board[i, j])))
                    {
                        board[i, j] = '$';
                    }
                }
            }
            return board;
        }
    }

    public char[,] Step(char[,] board, ref List<Coordinate> AvailableSteps)
    {
        bool isValid = true;
        if (Color == 'W')
        {
            for (int i = 8; i > 0; i--)
            {
                for (int j = 8; j > 0; j--)
                {
                    if (i == Coords.X && j == Coords.Y)
                    {
                        board[i, j] = 'P';
                    }
                    else if (i == Coords.X - 1 && j == Coords.Y)
                    {
                        CheckPlace(i, j, ref board, ref AvailableSteps, ref isValid);
                    }
                }
            }
            return board;
        }
        else
        {
            for (int i = 1; i < 9; i++)
            {
                for (int j = 1; j < 9; j++)
                {
                    if (i == Coords.X && j == Coords.Y)
                    {
                        board[i, j] = 'p';
                    }
                    else if (i == Coords.X + 1 && j == Coords.Y)
                    {
                        CheckPlace(i, j, ref board, ref AvailableSteps, ref isValid);
                    }
                }
            }
            return board;
        }
    }

}