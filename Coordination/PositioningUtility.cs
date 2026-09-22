using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUIUtility.Coordination;

namespace TUIUtility.Coordination
{
    public class PositioningUtility
    {
        public static Point2D SecondDimensionize(Point point)
        {
            return (point.Dimensions[0] ?? 0, point.Dimensions[1] ?? 0);
        }
    }
}
