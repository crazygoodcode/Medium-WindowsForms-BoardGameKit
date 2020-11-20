using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Medium.BoardGameKit.Code
{
    public sealed class Pawn : Piece
    {
        public static Rectangle ImageW = new Rectangle(386, 88, 53, 49);
        public static Rectangle ImageB = new Rectangle(386, 23, 53, 49);
     
        public Pawn(int row, int column, bool isBlack = true)
            : base(row, column, isBlack)
        {
        }

        public override List<Move> ValidMoves()
        {
            List<Move> moves = new List<Move>(1);
            if (Previous.Count < 1)
                moves.Add(Move.Get(Row, IsBlack ? Column + 2 : Column - 2));

            moves.Add(Move.Get(Row, IsBlack ? Column + 1 : Column - 1));

            return moves;
        }
     
    }
}
