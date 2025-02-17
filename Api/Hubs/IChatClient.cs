namespace Api.Hubs;

public interface IChatClient
{
    Task ReceiveMessage(string message, string sender);
    Task MessageSent(string message);
    Task UserConnected(string connectionId, string username);
    Task UserConnected(string username);
    Task UserDisconnected(string connectionId);
    Task Typing(string connectionId);
}