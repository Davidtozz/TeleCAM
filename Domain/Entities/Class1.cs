namespace Domain;

public class User
{
    public Guid Id { get; init; }
    public string Username { get; init; }
    public string Password { get; init; }
    public OnlineStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
}

