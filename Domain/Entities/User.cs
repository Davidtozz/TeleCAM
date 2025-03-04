using System.Text.Json.Serialization;

namespace Domain.Entities;

#pragma warning disable CS8618

public class User
{
    [JsonIgnore]
    public int Id { get; set; }
    public string Username { get; set; }
    [JsonIgnore]
    public virtual ICollection<Contact> Contacts { get; set; }
}