namespace Chess
{
    using System.Collections.Generic;
    using System.Text;

    class Board
    {
        public string Fen { get; private set; }

        private Figure[,] figures;

        public Color MoveColor { get; private set; }

        public int MoveNumber { get; private set; }

        public Board(string fen)
        {
            Fen = fen;
            figures = new Figure[8,8];
            Init();
        }

        private void Init()
        {
            // "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1"
            //  0                                           1 2    3 4 5
            // 0 - расположение фигур
            // 1 - кто ходит (белые/черные)
            // 2 - (не реализов.) флаги ракировки
            // 3 - (не реализов.) битое поле
            // 4 - (не реализов.) кол. ходов для правила 50-ти ходов
            // 5 - номер хода сейчас
            string[] parts = Fen.Split();
            if (parts.Length!=6)
            {
                return;
            }

            InitFigures(parts[0]);
            MoveColor = parts[1] == "b" ? Color.Black : Color.White;
            MoveNumber = int.Parse(parts[5]);
        }

        private void InitFigures(string data)
        {
            for (int j = 8; j >= 2; j--)
            {
                data = data.Replace(j.ToString(), (j - 1).ToString() + "1");
            }

            data = data.Replace("1", ".");
            string[] lines = data.Split('/');

            for (int y = 7; y >= 0; y--)
            {
                for (int x = 0; x < 8; x++)
                {
                    figures[x, y] = lines[7-y][x] == '.' ? Figure.None : (Figure)lines[7 - y][x];
                }
            }
        }

        public IEnumerable<FigureOnSquare> YieldFigures()
        {
            foreach (Square square in Square.YieldSquares())
            {
                if(GetFigureAt(square).GetColor() == MoveColor)
                    yield return new FigureOnSquare(GetFigureAt(square), square);
            }
        }

        public Figure GetFigureAt(Square square)
        {
            if (square.OnBoard())
            {
                return figures[square.x, square.y];
            }

            return Figure.None;
        }

        void SetFigureAt(Square square, Figure figure)
        {
            if (square.OnBoard())
            {
                figures[square.x, square.y] = figure;
            }
        }

        public Board Move(FigureMoving fm)
        {
            Board next = new Board(Fen);
            next.SetFigureAt(fm.From, Figure.None);
            next.SetFigureAt(fm.To, fm.Promotion == Figure.None ? fm.Figure : fm.Promotion);
            if (MoveColor == Color.Black)
            {
                next.MoveNumber++;
            }

            next.MoveColor = MoveColor.FlipColor();
            next.GenerateFen();
            return next;
        }

        private void GenerateFen()
        {
            Fen = FenFigures() + " " + (MoveColor == Color.White ? "w" : "b") + " - - 0 " + MoveNumber.ToString();
        }

        private string FenFigures()
        {
            StringBuilder sb = new StringBuilder();
            for (int y = 7; y >= 0; y--)
            {
                for (int x = 0; x < 8; x++)
                {
                    sb.Append(this.figures[x, y] == Figure.None ? '1' : (char)this.figures[x, y]);
                }

                if (y > 0)
                {
                    sb.Append('/');
                }
            }

            string eight = "11111111";
            for (int j = 8; j >= 2; j--)
            {
                sb.Replace(eight.Substring(0, j), j.ToString());
            }
            return sb.ToString();
        }

        private bool CanEatKing()
        {
            Square badKing = FindBadKing();
            Moves moves = new Moves(this);
            foreach (FigureOnSquare fs in this.YieldFigures())
            {
                FigureMoving fm = new FigureMoving(fs, badKing);
                if (moves.CanMove(fm))
                    return true;
            }

            return false;
        }

        private Square FindBadKing()
        {
            Figure badKing = MoveColor == Color.Black ? Figure.WhiteKing : Figure.WhiteKing;
            foreach (Square square in Square.YieldSquares())
            {
                if (GetFigureAt(square) == badKing)
                {
                    return square;
                }
            }
            return Square.none;
        }

        public bool IsCheck()
        {
            Board after = new Board(Fen);
            after.MoveColor = MoveColor.FlipColor();
            return after.CanEatKing();
        }

        public bool IsCheckAfterMove(FigureMoving fm)
        {
            Board after = Move(fm);
            return after.CanEatKing();
        }
    }
}
