namespace Medium.BoardGameKit.Code
{
    public struct Dimension
    {
        public int XElement;
        public int YElement;
        public int Width;
        public int Height;

        public static Dimension Get(int x, int y, int width, int height)
        {
            return new Dimension()
            {
                XElement = x,
                YElement = y,
                Width = width,
                Height = height
            };
        }
    }

}
