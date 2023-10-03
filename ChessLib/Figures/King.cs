using ChessLib.Figures;
namespace ChessLib;

public class King : Figure, IFigure
{
    public King()
    {
        Coords = new Coordinate(0, 0);
        Color = 'W';
    }

    public King(Coordinate coord, char color)
    {
        Coords = coord;
        Color = color;
    }

    public King(King obj)
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
    public bool MoveTo(Coordinate destination)
    {
        if ((Math.Abs(Coords.X - destination.X) <= 1) && (Math.Abs(Coords.Y - destination.Y) <= 1))
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
                    if (Color == 'W')
                        board[i, j] = 'K';
                    else board[i, j] = 'k';
                }
                else if (((i == Coords.X && j == Coords.Y - 1) || (i == Coords.X && j == Coords.Y + 1) ||
                    (i == Coords.X - 1 && j == Coords.Y) || (i == Coords.X + 1 && j == Coords.Y) ||
                    (i == Coords.X + 1 && j == Coords.Y - 1) || (i == Coords.X - 1 && j == Coords.Y + 1) ||
                    (i == Coords.X - 1 && j == Coords.Y - 1) || (i == Coords.X + 1 && j == Coords.Y + 1)))

                {
                    if (!figuresB.Contains(board[i, j]) && !figuresW.Contains(board[i, j]))
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
                        board[i, j] = 'K';
                    else board[i, j] = 'k';
                }
                else if (i == Coords.X + 1 && j == Coords.Y && down)
                {
                    CheckPlace(i, j, ref board, ref AvailableSteps, ref down);
                }
                else if (i == Coords.X + 1 && j == Coords.Y + 1 && downRight)
                {
                    CheckPlace(i, j, ref board, ref AvailableSteps, ref downRight);
                }
                else if (i == Coords.X + 1 && j == Coords.Y - 1 && downLeft)
                {
                    CheckPlace(i, j, ref board, ref AvailableSteps, ref down);
                }
                else if (i == Coords.X && j == Coords.Y + 1 && right)
                {
                    CheckPlace(i, j, ref board, ref AvailableSteps, ref right);
                }
            }
        }
        for (int i = 8; i > 0; i--)
        {
            for (int j = 8; j > 0; j--)
            {
                if (i == Coords.X - 1 && j == Coords.Y && up)
                {
                    CheckPlace(i, j, ref board, ref AvailableSteps, ref up);
                }
                else if (i == Coords.X && j == Coords.Y - 1 && left)
                {
                    CheckPlace(i, j, ref board, ref AvailableSteps, ref left);
                }
                else if (i == Coords.X - 1 && j == Coords.Y - 1 && upLeft)
                {
                    CheckPlace(i, j, ref board, ref AvailableSteps, ref upLeft);
                }
                else if (i == Coords.X - 1 && j == Coords.Y + 1 && upRight)
                {
                    CheckPlace(i, j, ref board, ref AvailableSteps, ref upRight);
                }
            }
        }
        return board;
    }

    public char[,] Step(char[,] board, ref List<Coordinate> AvailableSteps, List<Coordinate> OtherColorAvailableSteps)
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
                        board[i, j] = 'K';
                    else board[i, j] = 'k';
                }
                else if (i == Coords.X + 1 && j == Coords.Y && down)
                {
                    CheckPlace(i, j, ref board, ref AvailableSteps, ref down);
                }
                else if (i == Coords.X + 1 && j == Coords.Y + 1 && downRight)
                {
                    CheckPlace(i, j, ref board, ref AvailableSteps, ref downRight);
                }
                else if (i == Coords.X + 1 && j == Coords.Y - 1 && downLeft)
                {
                    CheckPlace(i, j, ref board, ref AvailableSteps, ref down);
                }
                else if (i == Coords.X && j == Coords.Y + 1 && right)
                {
                    CheckPlace(i, j, ref board, ref AvailableSteps, ref right);
                }
            }
        }
        for (int i = 8; i > 0; i--)
        {
            for (int j = 8; j > 0; j--)
            {
                if (i == Coords.X - 1 && j == Coords.Y && up)
                {
                    CheckPlace(i, j, ref board, ref AvailableSteps, ref up);
                }
                else if (i == Coords.X && j == Coords.Y - 1 && left)
                {
                    CheckPlace(i, j, ref board, ref AvailableSteps, ref left);
                }
                else if (i == Coords.X - 1 && j == Coords.Y - 1 && upLeft)
                {
                    CheckPlace(i, j, ref board, ref AvailableSteps, ref upLeft);
                }
                else if (i == Coords.X - 1 && j == Coords.Y + 1 && upRight)
                {
                    CheckPlace(i, j, ref board, ref AvailableSteps, ref upRight);
                }
            }
        }
        if(OtherColorAvailableSteps.Count > 0)
        {
            List<Coordinate> coords = new List<Coordinate>();
            bool isContinue = false;
            for (int i = 0; i < AvailableSteps.Count; i++)
            {
                for (int j = 0; j < OtherColorAvailableSteps.Count; j++)
                {
                    if (AvailableSteps[i].X == OtherColorAvailableSteps[j].X && AvailableSteps[i].Y == OtherColorAvailableSteps[j].Y)
                    {
                        board[AvailableSteps[i].X, AvailableSteps[i].Y] = '!';
                        isContinue = false;
                        break;
                    }
                    else
                    {
                        isContinue = true;
                    }
                }
                if (isContinue)
                {
                    coords.Add(AvailableSteps[i]);
                    isContinue = false;
                }
            }
            AvailableSteps = coords;
        }
        return board;
    }

    public static bool Solution(Coordinate KingCoord, List<Coordinate> KingSteps, List<Coordinate> otherColorSteps, List<Coordinate> AvailableStepsOtherKing, ref string result, ref bool isValidPlacement, ref bool isShakh)
    {
        bool isPat = Pat(KingSteps, otherColorSteps);
        isShakh = Shakh(KingCoord, otherColorSteps);
        isValidPlacement = isValidKingPlace(KingCoord, AvailableStepsOtherKing);
        if (!isValidPlacement)
        {
            if (isShakh)
            {
                if (isPat)
                {
                    result = "Mat";
                    return true;
                }
                result = "Shax";
                return true;
            }
            else if (isPat)
            {
                result = "Pat";
                return true;
            }
            return false;
        }
        result = "Tagavornery chen karox irar koxq linel";
        return true;
    }

    public static bool Pat(List<Coordinate> KSteps, List<Coordinate> otherColorSteps)
    {
        int count = 0;
        foreach (Coordinate c in KSteps)
        {
            if (!otherColorSteps.Contains(c))
            {
                count++;
            }
        }
        if (count > 0)
        {
            return false;
        }
        else return true;

    }

    public static bool Shakh(Coordinate KingCoords, List<Coordinate> otherColorSteps)
    {
        if (otherColorSteps.Contains(KingCoords))
        {
            return true;
        }
        return false;
    }

    public static bool isValidKingPlace(Coordinate KingCoord, List<Coordinate> otherColorKing)
    {
        if (otherColorKing.Contains(KingCoord))
        {
            return true;
        }
        return false;
    }
}