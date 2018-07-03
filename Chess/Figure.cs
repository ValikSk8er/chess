namespace Chess
{
    enum Figure
    {
        None,
        WhiteKing = 'K',
        WhiteQueen = 'Q',
        WhiteRook = 'R',    // ладья, тура
        WhiteBishop = 'B',  // слон, офицер
        WhiteKnight = 'N',  // конь
        WhitePawn = 'P',    // пешка

        BlackKing = 'k',
        BlackQueen = 'q',
        BlackRook = 'r',
        BlackBishop = 'b',
        BlackKnight = 'n',
        BlackPawn = 'p'
    }

    static class FigureMethods
    {
        public static Color GetColor(this Figure figure)
        {
            if (figure == Figure.None)
                return Color.None;
            return (figure == Figure.WhiteKing || figure == Figure.WhiteQueen || figure == Figure.WhiteRook
                    || figure == Figure.WhiteBishop || figure == Figure.WhiteKnight || figure == Figure.WhitePawn)
                       ? Color.White
                       : Color.Black;
        }
    }
}
