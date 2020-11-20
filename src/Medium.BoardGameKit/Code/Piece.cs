using System.Collections.Generic;

namespace Medium.BoardGameKit.Code
{
    public abstract class Piece
    {
        public int Row { get; internal set; }
        public int Column { get; internal set; }
        public bool IsBlack { get; protected set; }
        public Stack<Move> Previous { get; protected set; }
        public Piece(int row, int column, bool isBlack = true)
        {
            Row = row;
            Column = column;
            IsBlack = isBlack;
            Previous = new Stack<Move>(1);
        }

        public abstract List<Move> ValidMoves();
    }
}
