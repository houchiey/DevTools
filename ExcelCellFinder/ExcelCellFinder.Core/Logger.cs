namespace ExcelCellFinder.Core
{
    public class Logger
    {
        public Action<string> WriteDebug { set; private get; }
        public Action<string, Exception> WriteError { set; private get; }

        public Logger(Action<string> writeDebug, Action<string, Exception> writeError)
        {
            WriteDebug = writeDebug;
            WriteError = writeError;
        }

        internal void LogDebug(string message)
        {
            WriteDebug(message);
        }

        internal void LogError(string message, Exception exception)
        {
            WriteError(message, exception);
        }
    }
}
