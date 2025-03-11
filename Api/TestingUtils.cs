using Api.Data;
using Domain.Entities;

public static class TestingUtils 
{
    private static readonly Random _random = new();
    private static readonly string[] MessageTemplates =
    [
        "Hey, how are you?",
        "I'm good, thanks for asking!",
        "What are you working on today?",
        "Just finished that project we discussed.",
        "Can we meet later?",
        "Sure, what time works for you?",
        "Around 3pm?",
        "That works for me!",
        "Did you see the latest changes?",
        "Yes, they look great!",
        "I'm not sure about that approach.",
        "Let's discuss it further tomorrow.",
        "Don't forget about the deadline!",
        "Thanks for the reminder.",
        "Do you have time for a quick call?",
        "Sorry, I'm in a meeting right now.",
        "Let me know when you're free.",
        "I'll be available after 2pm.",
        "Have you tried the new feature?",
        "Not yet, is it working well?"
    ];


    public static async Task<List<Message>> GenerateRandomMessagesAsync(
        TelecamContext context,
        (Guid user1Id,
        Guid user2Id) chat,
        int count)
        {

            var messages = new List<Message>();

            var user1 = await context.Users.FindAsync(chat.user1Id);
            var user2 = await context.Users.FindAsync(chat.user2Id);

            if (user1 is null || user2 is null)
            {
                throw new ArgumentException("One or both users do not exist.");
            }

            var baseTime = DateTime.UtcNow.AddDays(-7);

            for(int i = 0; i < count; i++) {
                // Determine sender and receiver (alternate to create a conversation)
                var sender = i % 2 == 0 ? user1 : user2;
                var receiver = i % 2 == 0 ? user2 : user1;
                
                // Create message with random content
                var message = new Message
                {
                    Id = Guid.NewGuid(),
                    Content = MessageTemplates[_random.Next(MessageTemplates.Length)],
                    Sender = sender,
                    Receiver = receiver,
                    SentAt = baseTime.AddMinutes(i * 15 + _random.Next(10)) // Progressive timestamps with some randomness
                };
                
                messages.Add(message);
            }

            await context.Messages.AddRangeAsync(messages);
            await context.SaveChangesAsync();

            return messages;
        }
    
}
