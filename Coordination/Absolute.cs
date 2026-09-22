namespace TUIUtility.Coordination
{
    public struct Absolute(int value) : ICoordinate
    {
        public int Value { get; set; } = value;

        public static implicit operator Absolute(int value)
        {
            return new() { Value = value };
        }

        public static explicit operator int(Absolute absolete)
        {
            return absolete.Value;
        }
    }
}
