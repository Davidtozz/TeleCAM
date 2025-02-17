namespace Api.Models;

#pragma warning disable CS8618

public class Message
{
    public int Id { get; set; }
    public string Content { get; set; }
    public DateTime SentAt { get; set; }
    public int SenderId { get; set; }
    public int ReceiverId { get; set; }
    public virtual User Sender { get; set; }
    public virtual User Receiver { get; set; }
}