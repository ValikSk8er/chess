namespace Chess
{
    using System.Collections.Generic;

    public class Chess
    {
        public string fen { get; private set; }

        private Board board;

        private Moves moves;

        private List<FigureMoving> allMoves;

        public Chess(string fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1")
        {
            this.fen = fen;
            board = new Board(fen);
            this.moves = new Moves(board);
        }

        private Chess(Board board)
        {
            this.board = board;
            this.fen = board.Fen;
            this.moves = new Moves(board);
        }

        public Chess Move(string move) // Pe2e4 Pe7e8Q
        {
            FigureMoving fm = new FigureMoving(move);
            if (!this.moves.CanMove(fm))
            {
                return this;
            }
            if (this.board.IsCheckAfterMove(fm))
            {
                return this;
            }
            Board nextBoard = board.Move(fm);
            Chess nextChess = new Chess(nextBoard);
            return nextChess;
        }

        public char GetFigureAt(int x, int y)
        {
            Square square = new Square(x, y);
            Figure f = board.GetFigureAt(square);
            return f == Figure.None ? '.' : (char) f;
        }

        void FindAllMoves()
        {
            this.allMoves = new List<FigureMoving>();
            foreach (FigureOnSquare fs in this.board.YieldFigures())
            {
                foreach (Square to in Square.YieldSquares())
                {
                    FigureMoving fm = new FigureMoving(fs, to);
                    if (this.moves.CanMove(fm))
                    {
                        if (!this.board.IsCheckAfterMove(fm))
                        {
                            this.allMoves.Add(fm);
                        }
                    }
                }
            }
        }

        public List<string> GetAllMoves()
        {
            this.FindAllMoves();
            List<string> list = new List<string>();
            foreach (FigureMoving fm in this.allMoves)
            {
                list.Add(fm.ToString());
            }

            return list;
        }

        public bool IsCheck()
        {
            return this.board.IsCheck();
        }
    }
}
