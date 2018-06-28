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
            this.Figure = fs.Figure;
            this.From = fs.Square;
            this.To = to;
            this.Promotion = promotion;
        }

        public FigureMoving(string move) //Pe2e4
        {
            this.Figure = (Figure) move[0];
            this.From = new Square(move.Substring(1, 2));
            this.To = new Square(move.Substring(3, 2));
            this.Promotion = (move.Length == 6) ? (Figure) move[5] : Figure.None;
        }
    }
}
