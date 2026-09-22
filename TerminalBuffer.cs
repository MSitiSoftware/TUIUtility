using TUIUtility.Coordination;

namespace TUIUtility
{
    public class TerminalBuffer(TerminalBuffer? parent, Point2D position, Point2D size)
    {
        public TerminalBuffer(Point2D position, Point2D size) : this(null, position, size) { }

        public TerminalBuffer? Parent { get; set; } = parent;
        public Point2D Position { get; set; } = position;
        public Point2D Size { get; set; } = size;
        private Point2D ExternalSize { get => Parent?.Size ?? TerminalUtility.BufferSize; }

        public Point2D Externalize(Point2D internalPosition)
        {

            Point2D externalPosition = new(Position.Point);

            for (int i = 0; i < internalPosition.Point.Dimensions.Length; i++)
            {
                Coordinate internalCoordinate = internalPosition.Point.Dimensions[i]!.Value;
                Coordinate externalCoordinate = externalPosition.Point.Dimensions[i]!.Value;
                externalPosition.Point.Dimensions[i] = (externalCoordinate.Object is Percent percent ? (ExternalSize.Point.Dimensions[i] * percent.Value / 100 + percent.AbsoluteOffset) : externalCoordinate) + (internalCoordinate.Object is Percent percent2 ? (Size.Point.Dimensions[i] * percent2.Value / 100 + percent2.AbsoluteOffset) : internalCoordinate);
            }

            return Parent?.Externalize(externalPosition) ?? externalPosition;

        }

        public Point2D Internalize(Point2D externalPosition)
        {

            externalPosition = Parent?.Internalize(externalPosition) ??
                new(externalPosition.Point);

            for (int i = 0; i < externalPosition.Point.Dimensions.Length; i++)
            {
                Coordinate internalCoordinate = Position.Point.Dimensions[i]!.Value;
                externalPosition.Point.Dimensions[i] -= (internalCoordinate.Object is Percent percent ? (ExternalSize.Point.Dimensions[i] * percent.Value / 100 + percent.AbsoluteOffset) : internalCoordinate);
            }

            return externalPosition;

        }

        //private Point2D cursorPosition = PositioningUtility.OriginPoint;

        public Point2D CursorPosition
        {
            get
            {
                return Internalize(TerminalUtility.CursorPosition);
            }
            set
            {
                TerminalUtility.CursorPosition = Externalize(value);
            }
        }

        public bool CursorInBuffer
        {
            get
            {
                var cursorPositionDimensions = CursorPosition.Point.Dimensions;
                var sizeDimensions = Size.Point.Dimensions;

                for (int i = 0; i < cursorPositionDimensions.Length; i++)
                {
                    if (cursorPositionDimensions[i] < 0 || cursorPositionDimensions[i] > sizeDimensions[i])
                    {
                        return false;
                    }
                }

                return true;

            }
        }

        public void Write(string content)
        {
            if (!CursorInBuffer)
                CursorPosition = Point2D.Origin;

            int spaceSize = CursorPosition.X;

            // The whole length of the row to be added into the buffer.
            int totalRow = spaceSize + content.Length;

            int rowSize = Size.X;

            // The amount of times to iterate a new row to the buffer.
            int partingFactor = (int)Math.Ceiling(totalRow / (double)rowSize);

            for (int i = 0; i < partingFactor; i++)
            {
                if (rowSize <= content.Length)
                {
                    Console.Write(content[..rowSize]);

                    content = content[rowSize..];

                    if (CursorPosition.Y == Size.Y) return;

                    CursorPosition = (0, CursorPosition.Y + 1);
                }
                else
                {
                    Console.Write(content);
                }

            }

        }

    }
}
