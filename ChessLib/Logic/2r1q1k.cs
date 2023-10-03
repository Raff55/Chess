namespace ChessLib.Logic;

public class log1
{
    public char[,] Board;
    Coordinate wk, wq, wr1, wr2, bk;
    List<Coordinate> AvStQ = new List<Coordinate>();
    List<Coordinate> AvStR1 = new List<Coordinate>();
    List<Coordinate> AvStR2 = new List<Coordinate>();
    List<Coordinate> AvailableStepsWK = new List<Coordinate>();
    List<Coordinate> AvStBK = new List<Coordinate>();
    List<Coordinate> CoordW = new List<Coordinate>();

    public log1(char[,] board, Coordinate wk, Coordinate wq, Coordinate wr1, Coordinate wr2, Coordinate bk)
    {
        this.Board = board;
        this.wk = wk;
        this.wq = wq;
        this.wr1 = wr1;
        this.wr2 = wr2;
        this.bk = bk;
    }

    public log1(char[,] board, List<Coordinate> coordsw, List<Coordinate> coordsb)
    {
        Board = board;
        CoordW = coordsw;
        bk = coordsb[0];
        if (coordsw.Count == 4)
        {
            wq = coordsw[0];
            wr1 = coordsw[1];
            wr2 = coordsw[2];
            wk = coordsw[3];
        }
        if (coordsw.Count == 3)
        {

        }
    }

    public char[,] Steps(out List<Coordinate> coordW, ref List<bool> ShakhCount)
    {
        Chess ch = new Chess();
        List<Coordinate> AvailableStepsW = new List<Coordinate>();

        List<Coordinate> AvailableStepsHorizontalQ = new List<Coordinate>();
        List<Coordinate> AvailableStepsVerticalQ = new List<Coordinate>();

        List<Coordinate> AvailableStepsHorizontalR1 = new List<Coordinate>();
        List<Coordinate> AvailableStepsVerticalR1 = new List<Coordinate>();

        List<Coordinate> AvailableStepsHorizontalR2 = new List<Coordinate>();
        List<Coordinate> AvailableStepsVerticalR2 = new List<Coordinate>();

        List<Coordinate> AvailableStepsBK = new List<Coordinate>();
        Board = ch.FigurePlacement(Board, bk, 'k', 'b', ref AvailableStepsBK);

        List<Coordinate> figures = new List<Coordinate>();
        List<List<Coordinate>> figuresAvailableSteps = new List<List<Coordinate>>();
        #region
        ch.FigurePlacement(Board, wq, 'Q', 'W', ref AvStQ);
        AvailableStepsW.AddRange(AvStQ);
        ch.FigurePlacement(Board, wr1, 'R', 'W', ref AvStR1);
        AvailableStepsW.AddRange(AvStR1);
        ch.FigurePlacement(Board, wr2, 'R', 'W', ref AvStR2);
        AvailableStepsW.AddRange(AvStR2);
        ch.FigurePlacement(Board, wk, 'K', 'W', ref AvailableStepsWK);
        ch.FigurePlacement(Board, bk, 'K', 'B', ref AvStBK);
        int count = 0;
        List<Coordinate> SecureShakh = new List<Coordinate>();
        foreach (var step in AvailableStepsBK)
        {
            if (AvStQ.Contains(step))
            {
                count++;
            }
            if (AvStR1.Contains(step))
            {
                count++;
            }
            if (AvStR2.Contains(step))
            {
                count++;
            }
            if (AvailableStepsWK.Contains(step))
            {
                count++;
            }
            if (count > 1)
            {
                SecureShakh.Add(step);
            }
            count = 0;
        }
        bool q1 = false;
        bool wr11 = false;
        bool wr21 = false;
        #endregion
        if (ShakhCount.Count % 2 == 0)
        {
            for (int i = 0; i < AvStBK.Count; i++)
            {
                for (int j = 0; j < AvStQ.Count; j++)
                {
                    if (AvStQ[j].X == bk.X && AvStQ[j].Y != AvStBK[i].Y)
                    {
                        if (AvailableStepsBK.Contains(AvStQ[j]) && SecureShakh.Contains(AvStQ[j]))
                        {
                            AvailableStepsVerticalQ.Add(AvStQ[j]);
                            q1 = true;
                        }
                        else if(!AvailableStepsBK.Contains(AvStQ[j]))
                        {
                            AvailableStepsVerticalQ.Add(AvStQ[j]);
                            q1 = true;
                        }
                    }
                    else if (AvStQ[j].Y == bk.Y && AvStQ[j].X != AvStBK[i].X)
                    {
                        if (AvailableStepsBK.Contains(AvStQ[j]) && SecureShakh.Contains(AvStQ[j]))
                        {
                            AvailableStepsHorizontalQ.Add(AvStQ[j]);
                            q1 = true;
                        }
                        else if (!AvailableStepsBK.Contains(AvStQ[j]))
                        {
                            AvailableStepsHorizontalQ.Add(AvStQ[j]);
                            q1 = true;
                        }
                    }
                }

                for (int j = 0; j < AvStR1.Count; j++)
                {

                    if (AvStR1[j].X == bk.X && AvStR1[j].Y != AvStBK[i].Y)
                    {
                        if (AvailableStepsBK.Contains(AvStR1[j]) && SecureShakh.Contains(AvStR1[j]))
                        {
                            AvailableStepsVerticalR1.Add(AvStR1[j]);
                            wr11 = true;
                        }
                        else if (!AvailableStepsBK.Contains(AvStR1[j]))
                        {
                            AvailableStepsVerticalR1.Add(AvStR1[j]);
                            wr11 = true;
                        }
                    }
                    else if (AvStR1[j].Y == bk.Y && AvStR1[j].X != AvStBK[i].X)
                    {
                        if (AvailableStepsBK.Contains(AvStR1[j]) && SecureShakh.Contains(AvStR1[j]))
                        {
                            AvailableStepsHorizontalR1.Add(AvStR1[j]);
                            wr11 = true;
                        }
                        else if (!AvailableStepsBK.Contains(AvStR1[j]))
                        {
                            AvailableStepsHorizontalR1.Add(AvStR1[j]);
                            wr11 = true;
                        }
                    }
                }

                for (int j = 0; j < AvStR2.Count; j++)
                {
                    if (AvStR2[j].X == bk.X && AvStR2[j].Y != AvStBK[i].Y)
                    {
                        if (AvailableStepsBK.Contains(AvStR2[j]) && SecureShakh.Contains(AvStR2[j]))
                        {
                            AvailableStepsVerticalR2.Add(AvStR2[j]);
                            wr21 = true;
                        }
                        else if (!AvailableStepsBK.Contains(AvStR2[j]))
                        {
                            AvailableStepsVerticalR2.Add(AvStR2[j]);
                            wr21 = true;
                        }
                    }
                    else if (AvStR2[j].Y == bk.Y && AvStR2[j].X != AvStBK[i].X)
                    {
                        if (AvailableStepsBK.Contains(AvStR2[j]) && SecureShakh.Contains(AvStR2[j]))
                        {
                            AvailableStepsHorizontalR2.Add(AvStR2[j]);
                            wr21 = true;
                        }
                        else if (!AvailableStepsBK.Contains(AvStR2[j]))
                        {
                            AvailableStepsHorizontalR2.Add(AvStR2[j]);
                            wr21 = true;
                        }
                    }
                }
            }
        }
        else if (ShakhCount.Count % 2 != 0)
        {
            for (int i = 0; i < AvStBK.Count; i++)
            {
                for (int j = 0; j < AvStQ.Count; j++)
                {

                    if ((AvStQ[j].X == bk.X - 1 || AvStQ[j].X == bk.X + 1) && AvStQ[j].X != AvStBK[i].X && AvStQ[j].Y != AvStBK[i].Y)
                    {
                        AvailableStepsHorizontalQ.Add(AvStQ[j]);
                        q1 = true;
                    }
                    else if ((AvStQ[j].Y == bk.Y - 1 || AvStQ[j].Y == bk.Y + 1) && AvStQ[j].X != AvStBK[i].X && AvStQ[j].Y != AvStBK[i].Y)
                    {
                        AvailableStepsVerticalQ.Add(AvStQ[j]);
                        q1 = true;
                    }
                }

                for (int j = 0; j < AvStR1.Count; j++)
                {
                    if ((AvStR1[j].X == bk.X - 1 || AvStR1[j].X == bk.X + 1) && AvStR1[j].X != AvStBK[i].X && AvStR1[j].Y != AvStBK[i].Y)
                    {
                        AvailableStepsHorizontalR1.Add(AvStR1[j]);
                        wr11 = true;
                    }
                    else if ((AvStR1[j].Y == bk.Y - 1 || AvStR1[j].Y == bk.Y + 1) && AvStR1[j].X != AvStBK[i].X && AvStR1[j].Y != AvStBK[i].Y)
                    {
                        AvailableStepsVerticalR1.Add(AvStR1[j]);
                        wr11 = true;
                    }
                }

                for (int j = 0; j < AvStR2.Count; j++)
                {
                    if ((AvStR2[j].X == bk.X - 1 || AvStR2[j].X == bk.X + 1) && AvStR2[j].X != AvStBK[i].X && AvStR2[j].Y != AvStBK[i].Y)
                    {
                        AvailableStepsHorizontalR2.Add(AvStR2[j]);
                        wr21 = true;
                    }
                    else if ((AvStR2[j].Y == bk.Y - 1 || AvStR2[j].Y == bk.Y + 1) && AvStR2[j].X != AvStBK[i].X && AvStR2[j].Y != AvStBK[i].Y)
                    {
                        AvailableStepsVerticalR2.Add(AvStR2[j]);
                        wr21 = true;
                    }
                }
            }
            ShakhCount.Add(true);
        }
        if (q1)
        {
            if (AvailableStepsHorizontalQ.Count != 0)
            {
                figuresAvailableSteps.Add(AvailableStepsHorizontalQ);
                figures.Add(wq);
            }
            else
            {
                figuresAvailableSteps.Add(AvailableStepsVerticalQ);
                figures.Add(wq);
            }
        }
        if (wr11)
        {
            if (AvailableStepsHorizontalR1.Count != 0)
            {
                figuresAvailableSteps.Add(AvailableStepsHorizontalR1);
                figures.Add(wr1);
            }
            else
            {
                figuresAvailableSteps.Add(AvailableStepsVerticalR1);
                figures.Add(wr1);
            }
        }
        if (wr21)
        {
            if (AvailableStepsHorizontalR2.Count != 0)
            {
                figuresAvailableSteps.Add(AvailableStepsHorizontalR2);
                figures.Add(wr2);
            }
            else
            {
                figuresAvailableSteps.Add(AvailableStepsVerticalR2);
                figures.Add(wr2);
            }
        }
        if (figures.Count == 0)
        {
            figures.Add(wk);
            figuresAvailableSteps.Add(AvailableStepsWK);
        }
        Random rnd = new Random();
        int randIndex = rnd.Next(figures.Count);
        List<Coordinate> random = figuresAvailableSteps[randIndex];
        Coordinate init = figures[randIndex];
        Coordinate dest = random[0];
        ch.ClearBoard(ref Board, init, dest, Board[init.X, init.Y]);
        int index = CoordW.IndexOf(init);
        CoordW[index] = dest;
        coordW = CoordW;
        return Board;
    }
}