using System;

namespace Utilities.Logging
{
    public readonly struct LogMessage
    {
        private static char[] _logTypeSymbols = {'-', 'ℹ', '⚠', '⛔'};

        internal readonly string Text;
        internal readonly LogType Type;
        internal readonly string TimeString;

        internal LogMessage(string text, LogType type)
        {
            Text = text;
            Type = type;
            TimeString = DateTime.Now.ToString("yy-MM-dd hh:mm:ss");
        }

        public string ToFileFormatString() =>
            $"{_logTypeSymbols[(int) Type]} \t{TimeString} \t {Text}\n";
    }
}