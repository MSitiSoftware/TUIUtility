namespace TUIUtility.Coordination
{
    public struct Percent(int value) : ICoordinate
    {
        public int Value { get; set; } = value;

        public int AbsoluteOffset { get; set; } = 0;

        public static implicit operator Percent(int value) =>
            new()
            {
                Value = value
            };

        public static implicit operator Percent((int value, int offset) percentage) =>
            new()
            {
                Value = percentage.value,
                AbsoluteOffset = percentage.offset
            };

        public static explicit operator int(Percent percent)
        {
            return percent.Value;
        }
    }
}
