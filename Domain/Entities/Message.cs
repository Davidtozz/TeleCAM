namespace Domain.Entities;

#pragma warning disable CS8618

public class Message
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public DateTime SentAt { get; set; }
    public Guid SenderId { get; set; }
    public Guid ReceiverId { get; set; }
    public virtual User Sender { get; set; }
    public virtual User Receiver { get; set; }
}