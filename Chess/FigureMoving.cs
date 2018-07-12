namespace Chess
{
    using System;

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

        public int DeltaX { get { return To.x - From.x;} }

        public int DeltaY { get { return To.y - From.y; } }

        public int AbsDeltaX { get { return Math.Abs(DeltaX); } }

        public int AbsDeltaY { get { return Math.Abs(DeltaY); } }

        public int SignX { get { return Math.Sign(DeltaX); } }

        public int SignY { get { return Math.Sign(DeltaY); } }

        public override string ToString()
        {
            string text = (char)Figure + From.Name + To.Name;
            if (Promotion != Figure.None)
                text += (char)Promotion;
            return text;
        }
    }
}
