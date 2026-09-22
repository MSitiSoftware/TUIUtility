using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using TUIUtility.Coordination;

namespace TUIUtility
{
    /// <summary>
    /// The Utility for general actions that can be done on the 
    /// Terminal Interface.
    /// </summary>
    public static class TerminalUtility
    {
        public static TerminalBuffer RootBuffer => new(Point2D.Origin, BufferSize);

        public static TerminalBuffer WindowedBuffer => new(RootBuffer, Point2D.Origin, WindowSize);

        public static Point2D BufferSize => (Console.BufferWidth, Console.BufferHeight);

        public static Point2D WindowSize => (Console.WindowWidth, Console.WindowHeight);

        public static Point2D CursorPosition
        {
            get => Console.GetCursorPosition();
            set => Console.SetCursorPosition(value.X, value.Y);
        }
    }
}
