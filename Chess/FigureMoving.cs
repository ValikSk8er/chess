namespace Chess
{
    class FigureMoving
    {
        public Figure Figure { get; private set; }

        public Square From { get; private set; }

        public Square To { get; private set; }

        public Figure Promotion { get; private set; }

        public FigureMoving(FigureOnSquare fs, Square to, Figure promotion = Figure.None)
        {
            Figure = fs.Figure;
            From = fs.Square;
            To = to;
            Promotion = promotion;
        }

        public FigureMoving(string move) // Pe2e4
        {
            Figure = (Figure) move[0];
            From = new Square(move.Substring(1, 2));
            To = new Square(move.Substring(3, 2));
            Promotion = (move.Length == 6) ? (Figure) move[5] : Figure.None;
        }
    }
}
