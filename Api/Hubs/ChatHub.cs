using System.Collections.Concurrent;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.SignalR;
using SignalRSwaggerGen.Attributes;

namespace Api.Hubs;

[SignalRHub]
public class ChatHub : Hub<IChatClient>
{
    public static readonly ConcurrentDictionary<string, string> Connections = new();
    private readonly IUserService _userService;

    public ChatHub(IUserService userService)
    {
        _userService = userService;
    }

    public override async Task OnConnectedAsync()
    {
        if (Connections.TryGetValue(Context.ConnectionId, out var username))
        {
            var contacts = await _userService.GetContactsAsync(username);
            foreach (var contact in contacts)
            {
                await Clients.User(contact.Name).UserConnected(username);
            }
        }
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        if (Connections.TryRemove(Context.ConnectionId, out var username))
        {
            var contacts = await _userService.GetContactsAsync(username);
            foreach (var contact in contacts)
            {
                await Clients.User(contact.Name).UserDisconnected(username);
            }
        }
        await base.OnDisconnectedAsync(exception);
    }

    public async Task SetUsername(string username)
    {
        Connections[Context.ConnectionId] = username;
        var contacts = await _userService.GetContactsAsync(username);
        foreach (var contact in contacts)
        {
            await Clients.User(contact.Name).UserConnected(username);
        }
    }

    public async Task AddContact(string contactName)
    {
        if (Connections.TryGetValue(Context.ConnectionId, out var username))
        {
            var contact = new Contact()
            {
                Name = contactName,
                Status = "offline"
            };
            await _userService.AddContactAsync(username, contact);
        }

    }
    public async Task SendMessage(string message, string receiverUsername)
    {
        if (Connections.TryGetValue(Context.ConnectionId, out var senderUsername))
        {
            await Clients.User(receiverUsername).ReceiveMessage(message, sender: senderUsername);
        }
    }
    
    public async Task Typing(string receiverUsername) {
        if (Connections.TryGetValue(Context.ConnectionId, out var senderUsername)) {
            await Clients.Client(receiverUsername).Typing(senderUsername);
        }
    }

}