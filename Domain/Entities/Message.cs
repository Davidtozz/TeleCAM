using System.Reflection.Metadata;

namespace Domain.Entities;

#pragma warning disable CS8618

public class Message
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    /* public ICollection<Blob>? Attachments { get; set; } */
    public DateTime SentAt { get; set; }
    public virtual User Sender { get; set; }
    public virtual User Receiver { get; set; }
}