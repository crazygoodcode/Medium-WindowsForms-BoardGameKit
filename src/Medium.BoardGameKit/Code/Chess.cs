using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Drawing;
using System.Windows.Forms;

namespace Medium.BoardGameKit.Code
{
    public sealed class Chess : BoardBase
    {
        private string[] letters = { "a", "b", "c", "d", "e", "f", "g", "h" };
        private string[] numbers = { "8", "7", "6", "5", "4", "3", "2", "1" };
        private bool pieceSelectedFlag = false;
        private Piece pieceSelected = null;
        public List<Piece> Pieces { get; set; }
        private Image pieces = Image.FromFile($"Content\\gfx\\pieces.png");
        public Chess(Form owner) : base(ref owner, 8, 8, 500, 500)
        {
            NewGame();
        }
        public void NewGame()
        {
            Pieces = new List<Piece>(64);
            for (int x = 0; x < 8; x++)
            {
                Pieces.Add(new Pawn(x, 1));
                Pieces.Add(new Pawn(x, 6, false));
            }
        }
        public override void InputUpdate(MouseEventArgs e, Move move)
        {
            if (pieceSelectedFlag)
                if (pieceSelected.ValidMoves().Contains(move))
                {
                    pieceSelected.Previous.Push(Move.Get(pieceSelected.Row, pieceSelected.Column));

                    pieceSelected.Row = move.Row;
                    pieceSelected.Column = move.Column;
                    pieceSelectedFlag = false;
                }

            pieceSelected = Pieces.Where(p => p.Row == move.Row && p.Column ==
            move.Column).FirstOrDefault();
            pieceSelectedFlag = pieceSelected != null;

        }
        public override void Draw(Graphics graphics)
        {
            base.Draw(graphics);

            // Vertial Numbers
            for (int y = 0; y < Dimensions.YElement; y++)
            {
                SizeF numberSize = graphics.MeasureString(numbers[y], font);

                graphics.DrawString(numbers[y],
                          font,
                          Brushes.DarkBlue,
                          new PointF(YOffset, ((y * SquareSize.Height + YOffset) + (SquareSize.Height / 2)) - (numberSize.Height / 2)));
            }

            // Bottom Letters
            for (int x = 0; x < Dimensions.XElement; x++)
            {
                SizeF letterSize = graphics.MeasureString(letters[x], font);

                graphics.DrawString(letters[x],
                    font,
                    Brushes.DarkBlue,
                    new PointF(((x * SquareSize.Width + XOffset) + (SquareSize.Width / 2)) - (letterSize.Width / 2), SquareSize.Height * Dimensions.YElement + 10));
            }

            // Pieces
            foreach (var piece in Pieces)
            {
                if (piece.GetType() == typeof(Pawn))
                {
                    graphics.DrawImage(pieces,
                        new Rectangle(piece.Row * SquareSize.Width + XOffset + 4,
                                      piece.Column * SquareSize.Height + YOffset + 4,
                                      SquareSize.Width - 8,
                                      SquareSize.Height - 8),
                                      piece.IsBlack ? Pawn.ImageB : Pawn.ImageW,
                                      GraphicsUnit.Pixel);
                }
            }

            // Piece Moves
            if (pieceSelectedFlag)
            {
                foreach (var p in pieceSelected.ValidMoves())
                {
                    graphics.FillRectangle(new SolidBrush(Color.FromArgb(120, 255, 255, 0)),
                    p.Row * SquareSize.Width + XOffset + 1,
                    p.Column * SquareSize.Height + YOffset + 1,
                    SquareSize.Width - 1,
                    SquareSize.Height - 1);
                }
            }
        }

    }
}
