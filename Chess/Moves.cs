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
                    return (fm.SignX == 0 || this.fm.SignY == 0)
                           && this.CanStraigthMove();

                case Figure.WhiteBishop:
                case Figure.BlackBishop:
                    return (fm.SignX != 0 && fm.SignY != 0)
                           && this.CanStraigthMove();

                case Figure.WhiteKnight:
                case Figure.BlackKnight:
                    return CanKnightMove();

                case Figure.WhitePawn:
                case Figure.BlackPawn:
                    return CanPawnMove();

                default:
                    return false;
            }
        }

        private bool CanPawnMove()
        {
            if (fm.From.y < 1 || this.fm.From.y > 6)
                return false;
            int stepY = this.fm.Figure.GetColor() == Color.White ? 1 : -1;
            return CanPownGo(stepY) || CanPawnJump(stepY) || CanPawnEat(stepY);
        }

        private bool CanPownGo(int stepY)
        {
            if (this.board.GetFigureAt(this.fm.To) == Figure.None)
            {
                if (this.fm.DeltaX == 0)
                {
                    if (this.fm.DeltaY == stepY)
                        return true;
                }
            }

            return false;
        }

        private bool CanPawnJump(int stepY)
        {
            if (this.board.GetFigureAt(this.fm.To) == Figure.None)
            {
                if (this.fm.DeltaX == 0)
                {
                    if (this.fm.DeltaY == 2 * stepY)
                    {
                        if (this.fm.From.y == 1 || this.fm.From.y == 6)
                        {
                            if (this.board.GetFigureAt(new Square(this.fm.From.x, this.fm.From.y + stepY))
                                == Figure.None)
                                return true;
                        }
                    }
                }
            }
            return false;
        }

        private bool CanPawnEat(int stepY)
        {
            if (this.board.GetFigureAt(this.fm.To) != Figure.None)
            {
                if (this.fm.AbsDeltaX == 1)
                {
                    if (this.fm.DeltaY == stepY)
                    {
                        return true;
                    }
                }
            }

            return false;
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
            if (this.fm.AbsDeltaX <= 1 && this.fm.AbsDeltaY <= 2)
                return true;
            if (this.fm.AbsDeltaX <= 2 && this.fm.AbsDeltaY <= 1)
                return true;
            return false;
        }
    }
}
