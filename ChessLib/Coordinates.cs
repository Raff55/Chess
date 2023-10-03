namespace ChessLib;
public struct Coordinate
{
    public int X { get; set; }
    public int Y { get; set; }
    string letts = "ABCDEFGH";
    
    public Coordinate(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Coordinate(char let, char num)
    {
        X = int.Parse(num.ToString());
        Y = int.Parse(letts.IndexOf(let).ToString()) + 1;
    }
}