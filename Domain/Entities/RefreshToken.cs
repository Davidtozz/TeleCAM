namespace Domain.Entities;

#pragma warning disable CS8618

public class RefreshToken
{
    public Guid Id { get; set; }
    public string Token { get; set; }
    public DateTime ExpiresOnUtc { get; set; }
    public User User { get; set; }
}