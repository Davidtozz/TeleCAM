public sealed record SignupForm
{
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}