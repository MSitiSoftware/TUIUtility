using System;
using System.Collections.Generic;
using System.Text;

namespace TUIUtility.Coordination
{
    public struct Coordinate
    {
        public required ICoordinate Object { get; set; }

        public static implicit operator Coordinate(int absolete) =>
            new() { Object = new Absolute(absolete) };

        public static implicit operator Coordinate(Percent percent) =>
            new() { Object = percent };

        public static implicit operator int(Coordinate coordinate) =>
            coordinate.Object.Value;

    }
}
