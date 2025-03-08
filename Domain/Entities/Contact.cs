namespace Domain.Entities;

#pragma warning disable CS8618

public class Contact
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Status { get; set; }
    public Guid UserId { get; set; }
    public virtual User User { get; set; }
    public ICollection<Message> Messages { get; set; }
}