namespace Domain.Entities;

#pragma warning disable CS8618

public class User
{
    //todo: change to Guid
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    
    public string PasswordHash { get; set; }
    public virtual ICollection<Contact> Contacts { get; set; }
}