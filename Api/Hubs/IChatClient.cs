namespace Api;

public interface IChatClient
{
    public Task Connect();
    public Task SendMessage(string message, string? senderId = null, string? receiverId = null);
    public Task Disconnect();
}