namespace ChessLib.Figures;

public class Figure
{
    public Coordinate Coords { get; set; }
    public char Color { get; init; }
    public string figuresW = "RNBKQP";
    public string figuresB = "rnbkqp";

    public void CheckPlace(int i, int j, ref char[,] board, ref List<Coordinate> AvailableSteps, ref bool isValid)
    {
        if (!figuresB.Contains(board[i, j]) && !figuresW.Contains(board[i, j]) && isValid)
        {
            board[i, j] = '$';
            AvailableSteps.Add(new Coordinate(i, j));
        }
        else if (((figuresB.Contains(board[i, j]) && Color == 'W') || (figuresW.Contains(board[i, j]) && Color == 'B')) && isValid)
        {
            AvailableSteps.Add(new Coordinate(i, j));
            isValid = false;
        }
        else if (((figuresB.Contains(board[i, j]) && Color == 'B') || (figuresW.Contains(board[i, j]) && Color == 'W')) && isValid)
        {
            isValid = false;
        }
    }
}