using System.Reflection.Metadata;
using System.Text.Json.Serialization;

namespace Domain.Entities;

#pragma warning disable CS8618

public class Message
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public string Content { get; set; }
    /* public ICollection<Blob>? Attachments { get; set; } */
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public DateTime SentAt { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public virtual User Sender { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public virtual User Receiver { get; set; }
}