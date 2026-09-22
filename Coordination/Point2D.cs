using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUIUtility.Coordination;

namespace TUIUtility.Coordination
{
    public struct Point2D(Point point)
    {
        public Point Point = new(point.Dimensions);

        public static Point2D Origin => Point.Origin(2);

        public static implicit operator Point2D(Point point)
        {
            point.Dimensions = (Coordinate?[])point.Dimensions.Clone();
            return new()
            {
                Point = point
            };
        }

        public static implicit operator Point2D((Coordinate x, Coordinate y) point) =>
            new()
            {
                Point = new Point([point.x, point.y])
            };

        public static implicit operator (Coordinate x, Coordinate y)(Point2D point)
        {
            return (point.X, point.Y);
        }

        public readonly Coordinate X
        {
            get => Point.Dimensions[0]!.Value;
        }

        public readonly Coordinate Y
        {
            get => Point.Dimensions[1]!.Value;
        }
    }
}
