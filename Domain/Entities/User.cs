using System.Text.Json.Serialization;

namespace Domain.Entities;

#pragma warning disable CS8618

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Email { get; set; }
    [JsonIgnore]    
    public string PasswordHash { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public virtual ICollection<Contact> Contacts { get; set; }
}