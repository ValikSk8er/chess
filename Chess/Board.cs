using System;

namespace Chess
{
    class Board
    {
        public string Fen { get; private set; }
        private Figure[,] figures;
        public Color MoveColor { get; private set; }
        public int MoveNumber { get; private set; }

        public Board(string fen)
        {
            this.Fen = fen;
            figures = new Figure[8,8];
            Init();
        }

        private void Init()
        {
            SetFigureAt(new Square("a1"), Figure.WhiteKing);
            SetFigureAt(new Square("h8"), Figure.BlackKing);
            MoveColor = Color.White;
        }

        public Figure GetFigureAt(Square square)
        {
            if (square.OnBoard())
                return figures[square.x, square.y];
            return Figure.None;
        }

        void SetFigureAt(Square square, Figure figure)
        {
            if (square.OnBoard())
                figures[square.x, square.y] = figure;
        }

        public Board Move(FigureMoving fm)
        {
            Board next = new Board(Fen);
            next.SetFigureAt(fm.From, Figure.None);
            next.SetFigureAt(fm.To, fm.Promotion == Figure.None ? fm.Figure : fm.Promotion);
            if (MoveColor == Color.Black)
                next.MoveNumber++;
            next.MoveColor = MoveColor.FlipColor();
            return next;
        }
    }
}
