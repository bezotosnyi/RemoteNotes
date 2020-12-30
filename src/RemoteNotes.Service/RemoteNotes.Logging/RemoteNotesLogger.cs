using System;
using log4net;
using log4net.Appender;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using RemoteNotes.Logging.Contract;

namespace RemoteNotes.Logging
{
    public class RemoteNotesLogger<T> : IRemoteNotesLogger<T>
    {
        private readonly ILog _logger;

        public RemoteNotesLogger()
        {
            _logger = LogManager.GetLogger(typeof(T).FullName);
        }

        static RemoteNotesLogger()
        {
            InitLogger();
        }

        public void Debug(object message) => _logger.Debug(message);
        public void Debug(object message, Exception exception) => _logger.Debug(message, exception);

        public void Error(object message) => _logger.Error(message);
        public void Error(object message, Exception exception) => _logger.Error(message, exception);

        public void Fatal(object message) => _logger.Fatal(message);
        public void Fatal(object message, Exception exception) => _logger.Fatal(message, exception);

        public void Info(object message) => _logger.Info(message);
        public void Info(object message, Exception exception) => _logger.Info(message, exception);

        public void Warn(object message) => _logger.Warn(message);
        public void Warn(object message, Exception exception) => _logger.Warn(message, exception);

        private static void InitLogger()
        {
            var hierarchy = (Hierarchy)LogManager.GetRepository();

            var patternLayout = new PatternLayout
            {
                ConversionPattern = "%date [%thread] %-5level %logger - %message%newline"
            };

            patternLayout.ActivateOptions();

            var consoleAppender = new ConsoleAppender { Layout = patternLayout };
            consoleAppender.ActivateOptions();

            var rollingFileAppender = new RollingFileAppender
            {
                File = @"Logs\RemoteNotes.log",
                AppendToFile = true,
                Layout = patternLayout,
                MaxSizeRollBackups = 10,
                MaximumFileSize = "5MB",
                RollingStyle = RollingFileAppender.RollingMode.Size
            };

            rollingFileAppender.ActivateOptions();

            hierarchy.Root.AddAppender(rollingFileAppender);
            hierarchy.Root.AddAppender(consoleAppender);
            hierarchy.Configured = true;
        }
    }
}