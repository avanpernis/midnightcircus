using System;

namespace TokenAssist
{
    public enum MessageType
    {
        Info,
        Warning,
        Error,
        Success
    }

    public class MessageEventArgs : EventArgs
    {
        public MessageEventArgs(MessageType type, string message)
        {
            Type = type;
            Message = message;
        }

        public MessageType Type { get; set; }
        public string Message { get; set; }
    }

    public static class MessageSystem
    {
        public static event EventHandler<MessageEventArgs> OnMessage;

        public static void Info(string message)
        {
            Message(new MessageEventArgs(MessageType.Info, message));
        }

        public static void Warning(string message)
        {
            Message(new MessageEventArgs(MessageType.Warning, "WARNING: " + message));
        }

        public static void Error(string message)
        {
            Message(new MessageEventArgs(MessageType.Error, "ERROR: " + message));
        }

        public static void Success(string message)
        {
            Message(new MessageEventArgs(MessageType.Success, "SUCCESS: " + message));
        }

        private static void Message(MessageEventArgs args)
        {
            if (OnMessage != null)
            {
                OnMessage(null, args);
            }
        }
    }
}
