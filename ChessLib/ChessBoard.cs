namespace ChessLib;
public class Chess
{
    public string letters = "ABCDEFGH";
    public string figuresW = "RNBKQP";
    public string figuresB = "rnbkqp";
    /// <summary>
    /// This method generates a representation of a chessboard as a 2D array of characters, 
    /// with letters and numbers for coordinates, and alternating black and white squares.
    /// </summary>
    /// <returns>The method returns a 2D array of characters representing the chessboard.</returns>
    public char[,] CreateChessboard()
    {
        char[,] arr = new char[9, 9];
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (i == 0 && j == 0)
                {
                    arr[i, j] = ' ';
                }
                else if (i == 0 && j != 0)
                {
                    arr[i, j] = (letters[j - 1]);
                }
                else if (j == 0 && i != 0)
                {
                    arr[i, j] = (char.Parse(i.ToString()));
                }
                else if ((i % 2 == 0 && j % 2 == 0) || (i % 2 == 1 && j % 2 == 1))
                {
                    arr[i, j] = '#';
                }
                else
                {
                    arr[i, j] = '*';
                }
            }
        }
        return arr;
    }

    public char[,] FigurePlacement(char[,] board, Coordinate coord, char figure, char color, ref List<Coordinate> AvailableSteps)
    {
        if (coord.Y < 9 && coord.X < 9)
        {
            figure = char.ToUpper(figure);
            bool isEnum = Enum.TryParse<ChessFigures1>(figure.ToString(), out ChessFigures1 fig);
            if (isEnum)
            {
                switch (fig)
                {
                    case ChessFigures1.B:
                        var b = new Bishop { Coords = coord, Color = color };
                        board = b.Step(board, ref AvailableSteps);
                        return board;
                    case ChessFigures1.R:
                        var r = new Rook { Coords = coord, Color = color };
                        board = r.Step(board, ref AvailableSteps);
                        return board;
                    case ChessFigures1.N:
                        var n = new Knight { Coords = coord, Color = color };
                        board = n.Step(board, ref AvailableSteps);
                        return board;
                    case ChessFigures1.Q:
                        var q = new Queen { Coords = coord, Color = color };
                        board = q.Step(board, ref AvailableSteps);
                        return board;
                    case ChessFigures1.K:
                        var k = new King { Coords = coord, Color = color };
                        board = k.Step(board, ref AvailableSteps);
                        return board;
                    case ChessFigures1.P:
                        var p = new Pawn { Coords = coord, Color = color };
                        board = p.Step(board, ref AvailableSteps);
                        return board;
                    default:
                        return board;
                }
            }
        }
        return board;
    }

    public char[,] FigureMove(char[,] board, Coordinate IC, Coordinate DC, char figure, char color, ref string isValidMove, ref bool isMove)//ref List<Coordinate> AvailableSteps,
    {
        List<Coordinate> AvailableSteps = new List<Coordinate>();
        if (IC.X < 9 && IC.Y < 9)
        {
            bool isEnum = Enum.TryParse<ChessFigures1>(figure.ToString().ToUpper(), out ChessFigures1 fig);
            if (isEnum)
            {
                switch (fig)
                {
                    case ChessFigures1.B:
                        var b1 = new Bishop { Coords = IC, Color = color };
                        board = b1.Step(board, ref AvailableSteps);
                        if (b1.MoveTo(DC, AvailableSteps))
                        {
                            isValidMove = $"The Bishop can move from {letters[IC.Y - 1]}{IC.X} to {letters[DC.Y - 1]}{DC.X}";
                            board = b1.Step(board, ref AvailableSteps);
                            isMove = true;
                            return board;
                        }
                        else
                        {
                            isValidMove = $"The Bishop can't move from {letters[IC.Y - 1]}{IC.X} to {letters[DC.Y - 1]} {DC.X}";
                            board = b1.Step(board);
                            isMove = false;
                            return board;
                        }
                    case ChessFigures1.R:
                        var r1 = new Rook { Coords = IC, Color = color };
                        board = r1.Step(board, ref AvailableSteps);
                        if (r1.MoveTo(DC, AvailableSteps))
                        {
                            isValidMove = $"The Rook can move from {letters[IC.Y - 1]}{IC.X} to {letters[DC.Y - 1]}{DC.X}";
                            AvailableSteps.Clear();
                            board = r1.Step(board, ref AvailableSteps);
                            isMove = true;
                            return board;
                        }
                        else
                        {
                            isValidMove = $"The Rook can't move from {letters[IC.Y - 1]}{IC.X} to {letters[DC.Y - 1]}{DC.X}";
                            board = r1.Step(board);
                            isMove = false;
                            return board;
                        }
                    case ChessFigures1.N:
                        var n1 = new Knight { Coords = IC, Color = color };
                        board = n1.Step(board, ref AvailableSteps);
                        if (n1.MoveTo(DC, AvailableSteps))
                        {
                            isValidMove = $"The Knight can move from {letters[IC.Y - 1]}{IC.X} to {letters[DC.Y - 1]}{DC.X}";
                            AvailableSteps.Clear();
                            board = n1.Step(board, ref AvailableSteps);
                            isMove = true;
                            return board;
                        }
                        else
                        {
                            isValidMove = $"The Knight can't move from {letters[IC.Y - 1]}{IC.X} to {letters[DC.Y - 1]}{DC.X}";
                            board = n1.Step(board);
                            isMove = false;
                            return board;
                        }
                    case ChessFigures1.Q:
                        var q1 = new Queen { Coords = IC, Color = color };
                        board = q1.Step(board, ref AvailableSteps);
                        if (q1.MoveTo(DC, AvailableSteps))
                        {
                            isValidMove = $"The Queen can move from {letters[IC.Y - 1]}{IC.X} to {letters[DC.Y - 1]}{DC.X}";
                            AvailableSteps.Clear();
                            board = q1.Step(board, ref AvailableSteps);
                            isMove = true;
                            return board;
                        }
                        else
                        {
                            isValidMove = $"The Queen can't move from {letters[IC.Y - 1]}{IC.X} to {letters[DC.Y - 1]}{DC.X}";
                            board = q1.Step(board);
                            isMove = false;
                            return board;
                        }
                    case ChessFigures1.K:
                        var k1 = new King { Coords = IC, Color = color };
                        board = k1.Step(board, ref AvailableSteps);
                        if (k1.MoveTo(DC, AvailableSteps))
                        {
                            isValidMove = $"The King can move from {letters[IC.Y - 1]}{IC.X} to {letters[DC.Y - 1]}{DC.X}";
                            AvailableSteps.Clear();
                            board = k1.Step(board, ref AvailableSteps);
                            isMove = true;
                            return board;
                        }
                        else
                        {
                            isValidMove = $"The King can't move from {letters[IC.Y - 1]}{IC.X} to {letters[DC.Y - 1]}{DC.X}";
                            board = k1.Step(board);
                            isMove = false;
                            return board;
                        }
                    case ChessFigures1.P:
                        var p1 = new Pawn { Coords = IC, Color = color };
                        board = p1.Step(board, ref AvailableSteps);
                        if (p1.MoveTo(DC, AvailableSteps))
                        {
                            isValidMove = $"The Pawn can move from {letters[IC.Y - 1]}{IC.X} to {letters[DC.Y - 1]}{DC.X}";
                            board = p1.Step(board, ref AvailableSteps);
                            return board;
                        }
                        else
                        {
                            isValidMove = $"The Pawn can't move from {letters[IC.Y - 1]}{IC.X} to {letters[DC.Y - 1]}{DC.X}";
                            board = p1.Step(board);
                            return board;
                        }
                    default:
                        return board;
                }
            }
        }
        return board;
    }

    public List<Coordinate> ValidMoves(char[,] board)
    {
        List<Coordinate> validMoves = new List<Coordinate>();
        string symbols = "></\\|-$";
        for (int i = 1; i < 9; i++)
        {
            for (int j = 1; j < 9; j++)
            {
                if (symbols.Contains(board[i, j]))
                {
                    Coordinate coordinate = new Coordinate(i, j);
                    validMoves.Add(coordinate);
                }
            }
        }
        return validMoves;
    }

    public void ClearBoard(ref char[,] board, Coordinate c1, Coordinate c2, char fig)
    {
        char[] chars = { '-', '/', '\\', '|', '<', '>', '$' };
        string figuresW = "RBNKQ";
        string figuresB = "rbnkq";
        for (int i = 1; i < 9; i++)
        {
            for (int j = 1; j < 9; j++)
            {
                if (chars.Contains(board[i, j]) || (!figuresB.Contains(board[i, j]) && !figuresW.Contains(board[i, j])) || (i == c1.X && j == c1.Y))
                {
                    if ((i % 2 == 0 && j % 2 == 0) || (i % 2 == 1 && j % 2 == 1))
                    {
                        board[i, j] = '#';
                    }
                    else
                    {
                        board[i, j] = '*';
                    }
                }
            }
        }
        board[c2.X, c2.Y] = fig;
    }

    public void ClearBoard(ref char[,] board)
    {
        char[] chars = { '-', '/', '\\', '|', '<', '>', '$', 'X' };
        char[,] arr = board;
        string figuresW = "RBNKQ";
        string figuresB = "rbnkq";
        for (int i = 1; i < 9; i++)
        {
            for (int j = 1; j < 9; j++)
            {
                if (chars.Contains(arr[i, j]) && !figuresB.Contains(arr[i, j]) && !figuresW.Contains(arr[i, j]))
                {
                    if ((i % 2 == 0 && j % 2 == 0) || (i % 2 == 1 && j % 2 == 1))
                    {
                        arr[i, j] = '#';
                    }
                    else
                    {
                        arr[i, j] = '*';
                    }
                }
            }
        }
    }

    public void PlacingFigures(char[,] board, List<Coordinate> Coords, ref List<Coordinate> AvailableSteps, ref List<Coordinate> AvailableStepsK, List<Coordinate> AvailableStepsOtherColor)
    {
        AvailableSteps.Clear();
        //Coordinate = 
        foreach (var coord in Coords)
        {
            char figure = board[coord.X, coord.Y];
            if (figuresW.Contains(figure))
            {
                if (figure == 'K')
                {
                    AvailableStepsK.Clear();
                    board = FigurePlacement(board, coord, figure, 'W', ref AvailableStepsK);
                    //List<Coordinate> points = new List<Coordinate>();
                    //for (int i = 0; i < AvailableStepsK.Count; i++)
                    //{
                    //    if (!AvailableStepsOtherColor.Contains(AvailableStepsK[i]))
                    //    {
                    //        points.Add(AvailableStepsK[i]);
                    //        continue;
                    //    }
                    //}
                    //AvailableStepsK = points;
                }
                //else if (figure == '*' || figure == '#' || figuresB.Contains(figure))
                //{
                //    Coords.Remove(coord);
                //}
                else
                {
                    board = FigurePlacement(board, coord, figure, 'W', ref AvailableSteps);
                }
                ClearBoard(ref board);
            }
            else
            {
                if (figure == 'k')
                {
                    AvailableStepsK.Clear();
                    board = FigurePlacement(board, coord, figure, 'B', ref AvailableStepsK);
                    //    List<Coordinate> points = new List<Coordinate>();
                    //    for (int i = 0; i < AvailableStepsK.Count; i++)
                    //    {
                    //        if (!AvailableStepsOtherColor.Contains(AvailableStepsK[i]))
                    //        {
                    //            points.Add(AvailableStepsK[i]);
                    //        }
                    //    }
                    //    AvailableStepsK = points;
                    //}
                    //else if (figure == '*' || figure == '#' || figuresW.Contains(figure))
                    //{
                    //    Coords.Remove(coord);
                }
                else
                {
                    board = FigurePlacement(board, coord, figure, 'B', ref AvailableSteps);
                }
                ClearBoard(ref board);
            }
        }
    }

    public bool CheckKingPlace(ref char[,] board, char[,] chessBoard, char figure, Coordinate Initial, Coordinate Destination, ref Coordinate KingCoord, List<Coordinate> AvailableStepsKing, List<Coordinate> AvailableStepsOtherKing, List<Coordinate> otherColorSteps, ref List<Coordinate> CoordsColor, ref List<Coordinate> CoordsOtherColor, ref string res, ref List<bool> shakhCount)
    {
        bool isInvalidPlace = false;
        bool isShakh = false;
        if (King.Solution(KingCoord, AvailableStepsKing, otherColorSteps, AvailableStepsOtherKing, ref res, ref isInvalidPlace, ref isShakh))
        {
            if (isInvalidPlace)
            {
                CoordsColor.Remove(Destination);
                CoordsColor.Add(Initial);
                ClearBoard(ref board, Destination, Initial, figure);
                return true;
            }
            else if (isShakh)
            {
                shakhCount.Add(true);
                board = chessBoard;
                ClearBoard(ref board, Initial, Destination, figure);
                return true;
            }
            else
            {
                if (figure == 'k' && figuresW.Contains(board[Destination.X, Destination.Y]))
                {
                    CoordsOtherColor.Remove(Destination);
                }
                else if (figure == 'K' && figuresB.Contains(board[Destination.X, Destination.Y]))
                {
                    CoordsOtherColor.Remove(Destination);
                }
                board = chessBoard;
                ClearBoard(ref board, Initial, Destination, figure);
                return true;
            }
        }
        else
        {
            board = chessBoard;
            ClearBoard(ref board, Initial, Destination, figure);
        }
        return false;
    }

    public List<Coordinate> CheckAvailableStepsKing(ref char[,] board, List<Coordinate> coordsOtherColor, ref List<Coordinate> availableStepsOtherColor, List<Coordinate> availableStepsK, List<Coordinate> availableStepsOtherK, Coordinate coordsBK)
    {
        if ((coordsBK.X % 2 == 0 && coordsBK.Y % 2 == 0) || (coordsBK.X % 2 == 1 && coordsBK.Y % 2 == 1))
        {
            board[coordsBK.X, coordsBK.Y] = '#';
        }
        else
        {
            board[coordsBK.X, coordsBK.Y] = '*';
        }
        ClearBoard(ref board);
        List<Coordinate> valid = new List<Coordinate>();
        List<Coordinate> availableStepsOthers = availableStepsOtherColor;
        foreach (var coords in coordsOtherColor)
        {
            availableStepsOthers.Add(coords);
        }
        PlacingFigures(board, coordsOtherColor, ref availableStepsOtherColor, ref availableStepsOtherK, new List<Coordinate>());
        foreach (var step in availableStepsK)
        {
            if (!King.isValidKingPlace(step, availableStepsOtherK) && !King.Shakh(step, availableStepsOthers) && !figuresW.Contains(board[step.X, step.Y]))
                valid.Add(step);
        }
        board[coordsBK.X, coordsBK.Y] = 'k';
        ClearBoard(ref board);
        foreach (var w in valid)
        {
            if (!figuresB.Contains(board[w.X, w.Y]) && !figuresW.Contains(board[w.X, w.Y]))
                board[w.X, w.Y] = '$';
        }
        return valid;
    }
}