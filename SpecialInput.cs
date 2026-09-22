namespace TUIUtility
{
    public class SpecialInput
    {
        public static void DisplayInputOnBuffer(TerminalBuffer buffer, SpecialInput input, ref int oldInputLength)
        {
            buffer.CursorPosition = (0, 0);
            buffer.Write(input.Input);
            if (oldInputLength > input.Input.Length)
            {
                buffer.Write(" ");
                buffer.CursorPosition = (buffer.CursorPosition.X - 1, buffer.CursorPosition.Y);
            }
            
            oldInputLength = input.Input.Length;
        }

        public Predicate<char> AcceptedCharacters { get; set; } = c => char.IsLetterOrDigit(c) || char.IsPunctuation(c) || char.IsSymbol(c) || char.IsWhiteSpace(c);

        public class InputActions
        {
            public Predicate<ConsoleKey> EscapeKeys { get; set; } = key => key == ConsoleKey.Enter;
            public Predicate<ConsoleKey> BackspaceKeys { get; set; } = key => key == ConsoleKey.Backspace;
        }

        public InputActions inputActions = new();

        public string Input { get; set; } = string.Empty;

        public int CursorPosition { get; set; } = 0;

        public EventHandler? OnInputChange;

        public string Receive()
        {
            while (true)
            {
                var cki = Console.ReadKey(true);

                if (cki.Key == ConsoleKey.Enter)
                    break;

                char chr = cki.KeyChar;

                if (AcceptedCharacters(chr))
                {
                    Input = Input.Insert(CursorPosition, chr.ToString());
                    CursorPosition++;
                    OnInputChange?.Invoke(null, EventArgs.Empty);
                }

                if (inputActions.BackspaceKeys(cki.Key) && CursorPosition != 0)
                {
                    Input = Input.Remove(CursorPosition - 1);
                    CursorPosition--;
                    OnInputChange?.Invoke(null, EventArgs.Empty);
                }

            }

            return Input;
        }
    }
}
