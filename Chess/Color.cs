namespace Chess
{
    enum Color
    {
        None,
        White,
        Black
    }

    static class ColorMehtods
    {
        public static Color FlipColor(this Color color)
        {
            if (color == Color.Black) return Color.White;
            if (color == Color.White) return Color.Black;
            return Color.None;
        }
    }
}
