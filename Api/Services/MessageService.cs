namespace Api.Services;

using Api.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

public sealed class MessageService(TelecamContext context) : IMessageService
{
    private readonly TelecamContext _context = context;

    public async Task<Message> SendMessageAsync(User sender, Contact recipient, string content)
    {
        var message = new Message
        {
            Id = Guid.NewGuid(),
            Sender = sender,
            Receiver = recipient.User,
            Content = content,
            SentAt = DateTime.UtcNow
        };

        await _context.Messages.AddAsync(message);
        await _context.SaveChangesAsync();

        return message;
    }
}

public interface IMessageService
{
    Task<Message> SendMessageAsync(User sender, Contact recipient, string message);
}