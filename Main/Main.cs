using ChessLib;
using ChessLib.Logic;

namespace Start1;

public class Start
{
    public string letters = "ABCDEFGH";
    public string figuresW = "RNBKQP";
    public string figuresB = "rnbkqp";
    /// <summary>
    /// The method prompts the user to enter the location, type, and color of a chess piece they want to place on the board, 
    /// places the piece on the board, prompts the user to move the piece if desired,  and prints the updated chessboard to 
    /// the console.
    /// </summary>
    public Start()
    {
        var ch = new Chess();
        List<Coordinate> invalidPlaces = new List<Coordinate>();
        List<Coordinate> CoordsW = new List<Coordinate>();
        List<Coordinate> CoordsB = new List<Coordinate>();
        List<Coordinate> AvailableStepsW = new List<Coordinate>();
        List<Coordinate> AvailableStepsB = new List<Coordinate>();
        List<Coordinate> AvailableStepsWK = new List<Coordinate>();
        List<Coordinate> AvailableStepsBK = new List<Coordinate>();
        List<bool> ShaxsCount = new List<bool>();

        char[,] board = ch.CreateChessboard();
        PrintChessboard(board);
        #region Inputs
        //White Queen
        Coordinate wq = GetWhiteQueen(ref board, ref invalidPlaces);
        CoordsW.Add(wq);

        //White Rook1
        Coordinate wr1 = GetWhiteRook(ref board, ref invalidPlaces);
        CoordsW.Add(wr1);

        //White Rook2
        Coordinate wr2 = GetWhiteRook(ref board, ref invalidPlaces);
        CoordsW.Add(wr2);

        //White King
        Coordinate wk = GetWhiteKing(ref board, ref invalidPlaces, AvailableStepsB);
        CoordsW.Add(wk);

        //Placing Figures 
        ch.PlacingFigures(board, CoordsW, ref AvailableStepsW, ref AvailableStepsWK, AvailableStepsB);

        //Black King
        Coordinate bk = GetBlackKing(ref board, ref invalidPlaces, AvailableStepsW, AvailableStepsWK);
        CoordsB.Add(bk);

        ch.PlacingFigures(board, CoordsB, ref AvailableStepsB, ref AvailableStepsBK, AvailableStepsW);
        #endregion
        //Move
        while (true)
        {
            ch.PlacingFigures(board, CoordsW, ref AvailableStepsW, ref AvailableStepsWK, AvailableStepsB);
            ch.PlacingFigures(board, CoordsB, ref AvailableStepsB, ref AvailableStepsBK, AvailableStepsW);
            AvailableStepsBK = ch.CheckAvailableStepsKing(ref board, CoordsW, ref AvailableStepsW, AvailableStepsBK, AvailableStepsWK, bk);
            Coordinate coord1 = bk;
            Console.Clear();
            PrintChessboard(board);
            string isValidMove = " ";
            char[,]? virtualBoard = board.Clone() as char[,];
            bool isMove = true;
            char figure = board[coord1.X, coord1.Y];
            if (figuresW.Contains(figure)) { virtualBoard = ch.FigurePlacement(virtualBoard, coord1, figure, 'W', ref AvailableStepsW); }
            else { virtualBoard = ch.FigurePlacement(virtualBoard, coord1, figure, 'B', ref AvailableStepsB); }
            if(AvailableStepsBK.Count == 0)
            {
                if(King.Shakh(coord1, AvailableStepsW))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Mat");
                    Console.WriteLine("You Lose!");
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Pat");
                    Console.WriteLine("You Lose!");
                    break;
                }
            }
            Coordinate coord2 = GetPlace2();
            if (figure == 'K')
            {
                char f = board[coord2.X, coord2.Y];
                board[coord2.X, coord2.Y] = 'K';
                int index = CoordsW.IndexOf(coord1);
                CoordsW[index] = coord2;
                ch.PlacingFigures(board, CoordsW, ref AvailableStepsW, ref AvailableStepsWK, AvailableStepsB);
                if (King.isValidKingPlace(coord2, AvailableStepsBK))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("W Tagavorner@ chen karox linel irar koxq");
                    index = CoordsW.IndexOf(coord2);
                    CoordsW[index] = coord1;
                    board[coord2.X, coord2.Y] = f;
                    continue;
                }
                else if (King.isValidKingPlace(bk, AvailableStepsWK))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("W Tagavorner@ chen karox linel irar koxq");
                    index = CoordsW.IndexOf(coord2);
                    CoordsW[index] = coord1;
                    board[coord2.X, coord2.Y] = f;
                    continue;
                }
                else if (King.Shakh(coord2, AvailableStepsB))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("W Shax");
                    index = CoordsW.IndexOf(coord2);
                    CoordsW[index] = coord1;
                    board[coord2.X, coord2.Y] = f;
                    continue;
                }
                else
                {
                    board = virtualBoard;
                }
            }
            else if (figure == 'k')
            {
                //string res1 = "";
                //bool isValid = false;
                //bool isShakh = false;
                //if(King.Solution(coord2, AvailableStepsBK, AvailableStepsW, AvailableStepsWK, ref res1, ref isValid, ref isShakh))
                //{
                //ch.ClearBoard(ref board, coord1, coord2, 'k');
                //int index = CoordsB.IndexOf(coord1);
                //CoordsB[index] = coord2;
                //}
                char f = board[coord2.X, coord2.Y];
                board[coord2.X, coord2.Y] = 'k';
                int index = CoordsB.IndexOf(coord1);
                CoordsB[index] = coord2;
                ch.PlacingFigures(board, CoordsB, ref AvailableStepsB, ref AvailableStepsBK, AvailableStepsW);
                if (King.isValidKingPlace(coord2, AvailableStepsWK))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Tagavorner@ chen karox linel irar koxq");
                    index = CoordsB.IndexOf(coord2);
                    CoordsB[index] = coord1;
                    board[coord2.X, coord2.Y] = f;
                    continue;
                }
                else if (King.isValidKingPlace(wk, AvailableStepsBK))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Tagavorner@ chen karox linel irar koxq");
                    index = CoordsB.IndexOf(coord2);
                    CoordsB[index] = coord1;
                    board[coord2.X, coord2.Y] = f;
                    continue;
                }
                else if (King.Shakh(coord2, AvailableStepsW))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("B Shax");
                    index = CoordsB.IndexOf(coord2);
                    CoordsB[index] = coord1;
                    board[coord2.X, coord2.Y] = f;
                    continue;
                }
                else
                {
                    board = virtualBoard;
                }
            }
            else
            {
                board = virtualBoard;
            }
            if (figuresW.Contains(figure))
            {
                board = ch.FigureMove(board, coord1, coord2, figure, 'W', ref isValidMove, ref isMove);
            }
            else if (figuresB.Contains(figure))
            {
                board = ch.FigureMove(board, coord1, coord2, figure, 'B', ref isValidMove, ref isMove);
            }
            if (isMove)
            {
                if (figuresW.Contains(board[coord1.X, coord1.Y]))
                {
                    if (board[coord1.X, coord1.Y] == 'K')
                    {
                        wk = coord2;
                    }
                    Console.Clear();
                    ch.ClearBoard(ref board, coord1, coord2, figure);
                }
                if (figuresB.Contains(board[coord1.X, coord1.Y]))
                {
                    if (board[coord1.X, coord1.Y] == 'k')
                    {
                        bk = coord2;
                    }
                    Console.Clear();
                    ch.ClearBoard(ref board, coord1, coord2, figure);
                }
            }
            else
            {
                ch.ClearBoard(ref board);
                Console.Clear();
                PrintChessboard(board);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(isValidMove);
                Console.ResetColor();
            }
            AvailableStepsBK = ch.CheckAvailableStepsKing(ref board, CoordsW, ref AvailableStepsW, AvailableStepsBK, AvailableStepsWK, coord2);
            var log = new log1(board, CoordsW, CoordsB);
            virtualBoard = log.Steps(out CoordsW, ref ShaxsCount);
            string res = " ";
            //ch.PlacingFigures(board, CoordsW, ref AvailableStepsW, ref AvailableStepsWK, AvailableStepsB);


            if (ch.CheckKingPlace(ref board, virtualBoard, figure, coord1, coord2, ref bk, AvailableStepsBK, AvailableStepsWK, AvailableStepsW, ref CoordsW, ref CoordsB, ref res, ref ShaxsCount))
            {
                if (figuresB.Contains(figure)) { board = ch.FigurePlacement(board, bk, figure, 'B', ref AvailableStepsB); }
                Console.Clear();
                PrintChessboard(board);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Black King : " + res);
            }
            else if (ch.CheckKingPlace(ref board, virtualBoard, figure, coord1, coord2, ref wk, AvailableStepsWK, AvailableStepsBK, AvailableStepsB, ref CoordsB, ref CoordsW, ref res, ref ShaxsCount))
            {
                if (figuresB.Contains(figure)) { board = ch.FigurePlacement(board, bk, figure, 'B', ref AvailableStepsB); }
                Console.Clear(); 
                PrintChessboard(board);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("White King : " + res);
            }
            else
            {
                if (figuresB.Contains(figure)) { board = ch.FigurePlacement(board, bk, figure, 'B', ref AvailableStepsB); }
                Console.Clear();
                PrintChessboard(board);
            }
        }
    }

    /// <summary>
    ///  Method takes a two-dimensional char array as input and prints it to the console with different colors and styles based on the value of each element.
    /// </summary>
    /// <param name="array">Method takes a two-dimensional char array as input</param>
    public void PrintChessboard(char[,] array)
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (array[i, j] == '#')
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.Write($" {array[i, j]} ");
                    Console.ResetColor();
                }
                else if (array[i, j] == '*')
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write($" {array[i, j]} ");
                    Console.ResetColor();
                }
                else if (i == 0 || j == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.Write($" {array[i, j]} ");
                    Console.ResetColor();
                }
                else if (array[i, j] == '!')
                {
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write($" {array[i, j]} ");
                    Console.ResetColor();
                }
                else if (array[i, j] == '$')
                {
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write($" {array[i, j]} ");
                    Console.ResetColor();
                }
                else
                {
                    if (figuresB.Contains(array[i, j]))
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write($" {char.ToUpper(array[i, j])} ");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write($" {array[i, j]} ");
                        Console.ResetColor();
                    }
                }
            }
            Console.WriteLine();
        }
    }

    public Coordinate GetPlace(char[,] board)
    {
        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Enter Initial Place On Board : ");
            string? place1 = Console.ReadLine();
            if (String.IsNullOrEmpty(place1) || place1.Length != 2)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong input value, write correctly value");
            }
            else if (letters.Contains(char.ToUpper(place1[0])) && (int.Parse(place1[1].ToString()) <= 8 && int.Parse(place1[1].ToString()) >= 1))
            {
                Coordinate coords = new Coordinate(char.ToUpper(place1[0]), place1[1]);
                if (board[coords.X, coords.Y] != '*' && board[coords.X, coords.Y] != '#')
                {
                    return coords;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Empty Place");
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong input value, write correctly value");
            }
        }
    }

    public Coordinate GetPlace2()
    {
        string Place2 = "";
        bool isPlace2 = false;
        while (!isPlace2)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Enter Destination Place On Board : ");
            string? place2 = Console.ReadLine();
            if (String.IsNullOrEmpty(place2) || place2.Length != 2)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong input value, write correctly value");
            }
            else if (letters.Contains(char.ToUpper(place2[0])) && (int.Parse(place2[1].ToString()) <= 8 && int.Parse(place2[1].ToString()) >= 1))
            {
                char a = char.ToUpper(place2[0]);
                Place2 += a.ToString() + place2[1];
                isPlace2 = true;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong input value, write correctly value");
            }
        }
        Coordinate coords = new Coordinate(Place2[0], Place2[1]);
        return coords;
    }

    public Coordinate GetWhiteKNight(ref char[,] board, ref List<Coordinate> invalidPlaces)
    {
        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Enter White Knight Place : ");
            string? place = Console.ReadLine();
            if (String.IsNullOrEmpty(place) || place.Length != 2)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong input value, write correctly value");
            }
            else if (letters.Contains(char.ToUpper(place[0])) && (int.Parse(place[1].ToString()) <= 8 && int.Parse(place[1].ToString()) >= 1))
            {
                var coords = new Coordinate(char.ToUpper(place[0]), place[1]);
                if (invalidPlaces.Contains(coords))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("There is already a piece in the field you specified.Please enter another field.");
                }
                else
                {
                    invalidPlaces.Add(coords);
                    board[coords.X, coords.Y] = 'N';
                    Console.Clear();
                    PrintChessboard(board);
                    return coords;
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong input value, write correctly value");
            }
        }
    }

    public Coordinate GetWhiteRook(ref char[,] board, ref List<Coordinate> invalidPlaces)
    {
        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Enter White Rook Place : ");
            string? place = Console.ReadLine();
            if (String.IsNullOrEmpty(place) || place.Length != 2)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong input value, write correctly value");
            }
            else if (letters.Contains(char.ToUpper(place[0])) && (int.Parse(place[1].ToString()) <= 8 && int.Parse(place[1].ToString()) >= 1))
            {
                var coords = new Coordinate(char.ToUpper(place[0]), place[1]);
                if (invalidPlaces.Contains(coords))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("There is already a piece in the field you specified.Please enter another field.");
                }
                else
                {
                    invalidPlaces.Add(coords);
                    board[coords.X, coords.Y] = 'R';
                    Console.Clear();
                    PrintChessboard(board);
                    return coords;
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong input value, write correctly value");
            }
        }
    }
    
    public Coordinate GetWhiteQueen(ref char[,] board, ref List<Coordinate> invalidPlaces)
    {
        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Enter White Queen Place : ");
            string? place = Console.ReadLine();
            if (String.IsNullOrEmpty(place) || place.Length != 2)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong input value, write correctly value");
            }
            else if (letters.Contains(char.ToUpper(place[0])) && (int.Parse(place[1].ToString()) <= 8 && int.Parse(place[1].ToString()) >= 1))
            {
                var coords = new Coordinate(char.ToUpper(place[0]), place[1]);
                if (invalidPlaces.Contains(coords))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("There is already a piece in the field you specified.Please enter another field.");
                }
                else
                {
                    invalidPlaces.Add(coords);
                    board[coords.X, coords.Y] = 'Q';
                    Console.Clear();
                    PrintChessboard(board);
                    return coords;
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong input value, write correctly value");
            }
        }
    }

    public Coordinate GetWhiteKing(ref char[,] board, ref List<Coordinate> invalidPlaces, List<Coordinate> AvailableStepsOtherColor)
    {
        var ch = new Chess();
        List<Coordinate> AvailableSteps = new List<Coordinate>();
        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Enter White King Place : ");
            string? place = Console.ReadLine();
            if (String.IsNullOrEmpty(place) || place.Length != 2)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong input value, write correctly value");
            }
            else if (letters.Contains(char.ToUpper(place[0])) && (int.Parse(place[1].ToString()) <= 8 && int.Parse(place[1].ToString()) >= 1))
            {
                var coords = new Coordinate(char.ToUpper(place[0]), place[1]);
                if (invalidPlaces.Contains(coords))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("There is already a piece in the field you specified.Please enter another field.");
                }
                else
                {
                    invalidPlaces.Add(coords);
                    board = ch.FigurePlacement(board, coords, 'K', 'W', ref AvailableSteps);
                    ch.ClearBoard(ref board);
                    Console.Clear();
                    PrintChessboard(board);
                    return coords;
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong input value, write correctly value");
            }
        }
    }

    public Coordinate GetBlackKing(ref char[,] board, ref List<Coordinate> invalidPlaces, List<Coordinate> AvailableStepsW, List<Coordinate> AvailableStepsWK)
    {
        var ch = new Chess();
        List<Coordinate> AvailableSteps = new List<Coordinate>();
        string res = " ";
        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Enter Black King Place : ");
            string? place = Console.ReadLine();
            if (String.IsNullOrEmpty(place) || place.Length != 2)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong input value, write correctly value");
            }
            else if (letters.Contains(char.ToUpper(place[0])) && (int.TryParse(place[1].ToString(), out int num) && num <= 8 && num >= 1))
            {
                var coords = new Coordinate(char.ToUpper(place[0]), place[1]);
                char[,]? ExperimentalBoard = board.Clone() as char[,];
                if (ExperimentalBoard != null)
                {
                    ExperimentalBoard = ch.FigurePlacement(ExperimentalBoard, coords, 'k', 'B', ref AvailableSteps);
                    bool isValidPlace = false;
                    bool isShakh = false;
                    if (invalidPlaces.Contains(coords))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("There is already a piece in the field you specified.Please enter another field.");
                    }
                    else if (King.Solution(coords, AvailableSteps, AvailableStepsW, AvailableStepsWK, ref res, ref isValidPlace, ref isShakh))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(res);
                    }
                    else
                    {
                        invalidPlaces.Add(coords);
                        board = ch.FigurePlacement(board, coords, 'k', 'B', ref AvailableSteps);
                        ch.ClearBoard(ref board);
                        Console.Clear();
                        PrintChessboard(board);
                        return coords;
                    }
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong input value, write correctly value");
            }
        }
    }
}