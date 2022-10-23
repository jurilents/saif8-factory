using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Utilities.Logging
{
    public static class Logger
    {
        public static string LogFilePath { private get; set; } =
            Path.Combine(Application.persistentDataPath, $"{DateTime.Now}");
        
        public static bool UseLogFile { private get; set; }
        public static bool UseUnityConsole { private get; set; }
        public static bool UseGameConsole { private get; set; }
        
        public static GameObject GameConsole { private get; set; }
        public static IGameLogger GameLogMessage { private get; set; }
        public static IGameLogger GameInfoMessage { private get; set; }
        public static IGameLogger GameWarnMessage { private get; set; }
        public static IGameLogger GameErrorMessage { private get; set; }
        
        private static readonly Queue<LogMessage> _messagesCache = new Queue<LogMessage>();

        public static void Log(string msg) => ApplyLog(msg, LogType.Log, GameLogMessage);
        public static void Info(string msg) => ApplyLog(msg, LogType.Info, GameInfoMessage);
        public static void Warn(string msg) => ApplyLog(msg, LogType.Warn, GameWarnMessage);
        public static void Error(string msg) => ApplyLog(msg, LogType.Error, GameErrorMessage);


        private static void ApplyLog(string message, LogType logType, IGameLogger gameMessage)
        {
            var messageInstance = new LogMessage(message, logType);
            _messagesCache.Enqueue(new LogMessage(message, logType));
            if (UseLogFile && (_messagesCache.Count > 16 || logType is LogType.Error)) WriteCacheToFile();
            if (UseGameConsole) gameMessage.AddMessage(messageInstance, GameConsole);

#if UNITY_EDITOR
            if (UseUnityConsole) Debug.Log(message);
#endif
        }

        private static void WriteCacheToFile()
        {
            Task.Run(async () =>
            {
                // Write cache data to buffer
                var _builder = new StringBuilder();
                foreach (LogMessage message in _messagesCache)
                    _builder.Append(message.ToFileFormatString());

                // Reset cache
                _messagesCache.Clear();

                // Append text to file
                using var stream = new StreamWriter(LogFilePath, true, Encoding.UTF8);
                await stream.WriteAsync(_builder.ToString());
            });
        }
    }
}