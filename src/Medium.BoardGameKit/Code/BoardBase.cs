using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Medium.BoardGameKit.Code
{
    public abstract class BoardBase
    {
        private int xOffset = 30;
        private int yOffset = 5;
        private bool flipBoard = false;
        protected Font font = new Font("Arial", 12f);

        private bool highlight = false;
        private Point highlightSquare = new Point(-1, -1);

        public Form Owner { get; private set; }
        public Dimension Dimensions { get; private set; }
        public int XOffset { get { return xOffset; } set { xOffset = value; } }
        public int YOffset { get { return yOffset; } set { yOffset = value; } }
        public Size SquareSize { get; protected set; }
        public bool FlipBaord
        {
            get { return flipBoard; }
            set
            {
                if (value != flipBoard)
                {
                    flipBoard = value;
                    Owner.Refresh();
                }
            }
        }

        public BoardBase(ref Form owner, int x, int y, int width, int height)
        {
            // Validate Dimensions
            if (width >= owner.Width)
                width = owner.Width - XOffset;
            if (height >= owner.Height)
                height = owner.Height - YOffset;

            Owner = owner;
            Dimensions = Dimension.Get(x, y, width, height);
            SquareSize = new Size(Dimensions.Width / Dimensions.XElement,
                                        Dimensions.Height / Dimensions.YElement);

            // Wire Events
            Owner.Paint += new PaintEventHandler(Owner_Paint);
            Owner.MouseClick += new MouseEventHandler(Owner_MouseClick);
        }

        public BoardBase(ref Form owner, Dimension dimension)
                   : this(ref owner, dimension.XElement, dimension.YElement, dimension.Width, dimension.Height)
        {
        }

        private void Owner_Paint(object sender, PaintEventArgs e)
        {
            Draw(e.Graphics);
        }

        private void Owner_MouseClick(object sender, MouseEventArgs e)
        {
            int x = (e.X - XOffset) / SquareSize.Width;
            int y = (e.Y - YOffset) / SquareSize.Height;

            InputUpdate(e, Move.Get(x, y));

            if (x >= 0 && y >= 0 && !highlight)
            {
                highlight = true;
                highlightSquare = new Point(x, y);

                Owner.Refresh();
            }
        }

        public virtual void Draw(Graphics graphics)
        {
            graphics.CompositingMode = CompositingMode.SourceOver;
            graphics.CompositingQuality = CompositingQuality.HighQuality;

            bool flip = flipBoard;

            // Outer Borders
            graphics.DrawRectangle(Pens.Black,
                    new Rectangle(XOffset - 2, YOffset - 2,
     Dimensions.Width, Dimensions.Height));

            graphics.FillRectangle(Brushes.SaddleBrown,
                   new Rectangle(XOffset - 2, YOffset - 2,
     Dimensions.Width, Dimensions.Height));

            // Board
            for (int y = 0; y < Dimensions.YElement; y++)
            {
                for (int x = 0; x < Dimensions.XElement; x++)
                {
                    var rect = new Rectangle(x * SquareSize.Width + XOffset,
                        y * SquareSize.Height + YOffset,
                        SquareSize.Width,
                        SquareSize.Height);

                    if (flip)
                        graphics.FillRectangle(Brushes.SandyBrown, rect);
                    else
                        graphics.FillRectangle(Brushes.White, rect);

                    graphics.DrawRectangle(Pens.Black, rect);
                    //graphics.DrawString($"{x},{y}", font, Brushes.Black, new PointF(rect.X, rect.Y));

                    flip = !flip;
                }

                flip = !flip;
            }

            // Add Highlights
            if (highlight && highlightSquare.X >= 0)
            {
                graphics.FillRectangle(new SolidBrush(Color.FromArgb(100, 255, 255, 0)),
                      highlightSquare.X * SquareSize.Width + XOffset + 1,
                      highlightSquare.Y * SquareSize.Height + YOffset + 1,
                      SquareSize.Width - 1,
                      SquareSize.Height - 1);

                highlight = false;
            }
        }

        public virtual void InputUpdate(MouseEventArgs e, Move move)
        {
        }



    }
}
