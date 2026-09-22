using System;
using System.Collections.Generic;
using System.Text;
using TUIUtility.Coordination;

namespace TUIUtility.Coordination
{
    public struct ClosedPoint(int dimensionsSize)
    {
        public Point Point { get; set; }

        public readonly Coordinate?[] MinimumDimensions { get => Point.Origin(dimensionsSize).Dimensions; }
        public required Coordinate?[] MaximumDimensions { get; set; }

    }
}
