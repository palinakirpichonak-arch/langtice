namespace Shared.Data
{
    public enum MessageNotificationType
    {
        ExpirationNotification,
        StreakNotification
    }

    public class MessageData
    {
        public string MessageSubject { get; set; } = string.Empty;
        public string MessageBody { get; set; } = string.Empty;
    }

    public static class EmailSubjectTemplates
    {
        public static readonly Dictionary<MessageNotificationType, MessageData> Templates = new()
        {
            {
                MessageNotificationType.ExpirationNotification, new MessageData()
                {
                    MessageSubject = "Flashcard expiration",
                    MessageBody = "Your flashcard will expire at {0}"
                }
            },
            {
                MessageNotificationType.StreakNotification, new MessageData()
                {
                    MessageSubject = "Don't loose you're daily streak!",
                    MessageBody = "Complete a lesson not to lose you daily streak"
                }
            }
        };
    }
}
