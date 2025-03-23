using System.Text.Json.Serialization;

namespace Domain.Entities;

#pragma warning disable CS8618

public class Contact
{
    
    public Guid Id { get; set; }
    public string Name { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public virtual User User { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public virtual User? TargetUser { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public virtual ICollection<Message> Messages { get; set; } = [];
}