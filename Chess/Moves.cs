namespace Chess
{
    class Moves
    {
        private FigureMoving fm;

        private Board board;

        public Moves(Board board)
        {
            this.board = board;
        }

        public bool CanMove(FigureMoving fm)
        {
            this.fm = fm;
            return 
                CanMoveFrom() &&
                CanMoveTo() &&
                CanFigureMove();
        }

        private bool CanMoveFrom()
        {
            return this.fm.From.OnBoard() &&
                   this.fm.Figure.GetColor() == this.board.MoveColor;
        }

        private bool CanMoveTo()
        {
            return this.fm.To.OnBoard() &&
                this.fm.From != this.fm.To &&
                this.board.GetFigureAt(this.fm.To).GetColor() != this.board.MoveColor;
        }

        private bool CanFigureMove()
        {
            switch (fm.Figure)
            {
                case Figure.WhiteKing:
                case Figure.BlackKing:
                    return CanKingMove();

                case Figure.WhiteQueen:
                case Figure.BlackQueen:
                    return CanStraigthMove();

                case Figure.WhiteRook:
                case Figure.BlackRook:
                    return false;

                case Figure.WhiteBishop:
                case Figure.BlackBishop:
                    return false;

                case Figure.WhiteKnight:
                case Figure.BlackKnight:
                    return CanKnightMove();

                case Figure.WhitePawn:
                case Figure.BlackPawn:
                    return false;

                default:
                    return false;
            }
        }

        private bool CanStraigthMove()
        {
            Square at = fm.From;
            do
            {
                at = new Square(at.x + this.fm.SignX, at.y + this.fm.SignY);
                if (at == this.fm.To)
                    return true;
            }
            while (at.OnBoard() &&
                   this.board.GetFigureAt(at) == Figure.None);

            return false;
        }

        private bool CanKingMove()
        {
            if (this.fm.AbsDeltaX <= 1 && this.fm.AbsDeltaY <= 1)
                return true;
            return false;
        }

        private bool CanKnightMove()
        {
            if (this.fm.AbsDeltaX <= 1 && this.fm.AbsDeltaY <= 2) return true;
            if (this.fm.AbsDeltaX <= 2 && this.fm.AbsDeltaY <= 1) return true;
            return false;
        }
    }
}
