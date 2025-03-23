namespace Api.Services;

using Api.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using System.Linq;

public sealed class MessageService(TelecamContext context) : IMessageService
{
    private readonly TelecamContext _context = context;

    public async Task<ICollection<Message>> GetMessageHistoryAsync(Guid user1Id, Guid user2Id)
    {
        return await _context.Messages
            .Where(m => m.Sender.Id == user1Id)
            .Where(m => m.Receiver.Id == user2Id)
            .Include(m => m.Sender)
            .Include(m => m.Receiver)
            .Select(m => new Message
            {
                Content = m.Content,
                SentAt = m.SentAt,
            })
            .ToArrayAsync();
    }
    public async Task<Message> SendMessageAsync(User sender, User recipient, string content)
    {
        var message = new Message
        {
            Id = Guid.NewGuid(),
            Sender = sender,
            Receiver = recipient,
            Content = content,
            SentAt = DateTime.UtcNow,
        };

        await _context.Messages.AddAsync(message);
        await _context.SaveChangesAsync();

        return message;
    }
}

public interface IMessageService
{
    Task<Message> SendMessageAsync(User sender, User recipient, string message);
    Task<ICollection<Message>> GetMessageHistoryAsync(Guid user1Id, Guid user2Id);
}