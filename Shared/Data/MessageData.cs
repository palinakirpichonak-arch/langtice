namespace Shared.Data
{
    public enum MessageType
    {
        Expiration
    }

    public class MessageData
    {
        public string MessageSubject { get; set; } = string.Empty;
        public string MessageBody { get; set; } = string.Empty;
    }

    public static class EmailSubjectTemplates
    {
        public static readonly Dictionary<MessageType, MessageData> Templates = new()
        {
            {
                MessageType.Expiration, new MessageData()
                {
                    MessageSubject = "Flashcard expiration",
                    MessageBody = "Your flashcard will expire in {0} minutes"
                }
            }
        };
    }
}
