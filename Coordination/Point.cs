namespace TUIUtility.Coordination
{
    public struct Point(Coordinate?[] dimensions)
    {
        public Coordinate?[] Dimensions = (Coordinate?[])dimensions.Clone();

        public static Point Origin(int dimensionsSize)
        {
            return new Point()
            {
                Dimensions = [.. Enumerable.Repeat<Coordinate?>(0, dimensionsSize)]
            };
        }

        public static explicit operator string(Point point)
        {
            return string.Join(", ", point.Dimensions.Select(coord => coord?.Object.Value ?? 0));
        }

    }
}