namespace ChessLib.Figures;

interface IFigure
{
    bool MoveTo(Coordinate destination, List<Coordinate> coordinates);
    char[,] WrongPlane(char[,] arr, Coordinate coord);
    char[,] Step(char[,] board);
}