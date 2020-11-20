namespace Medium.BoardGameKit.Code
{
    public struct Move
    {
        public int Row;
        public int Column;

        public static Move Get(int row, int column)
        {
            return new Move() { Row = row, Column = column };
        }
    }

}
