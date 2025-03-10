using System.Collections.Concurrent;
using Domain.Entities;
using Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SignalRSwaggerGen.Attributes;

namespace Api.Hubs;

[Authorize("user")]
public class ChatHub : Hub<IChatClient>
{
    private readonly IUserService<User> _userService;
    private readonly IContactService _contactService;

    public ChatHub(IUserService<User> userService, IContactService contactService)
    {
        _userService = userService;
        _contactService = contactService;
    }

    public override async Task OnConnectedAsync()
    {
        
        string username = Context.User!.Identity!.Name!;
        var contacts = await _contactService.GetContactsByUsernameAsync(username);
        foreach (var contact in contacts)
        {
            await Clients.User(contact.Name).UserConnected(username);
        }
        
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        
        string username = Context.User!.Identity!.Name!;
        var contacts = await _contactService.GetContactsByUsernameAsync(username);
        foreach (var contact in contacts)
        {
            await Clients.User(contact.Name).UserDisconnected(username);
        }
        
        await base.OnDisconnectedAsync(exception);
    }


    public async Task SendMessage(string message, string receiverUsername)
    {
        /* if (Connections.TryGetValue(Context.ConnectionId, out var senderUsername))
        {
            await Clients.User(receiverUsername).ReceiveMessage(message, sender: senderUsername);
        } */
    }
    
    public async Task Typing(string receiverUsername) {
        /* if (Connections.TryGetValue(Context.ConnectionId, out var senderUsername)) {
            await Clients.Client(receiverUsername).Typing(senderUsername);
        } */
    }
}


public interface IChatClient
{
    Task ReceiveMessage(string message, string sender);
    Task MessageSent(string message);
    Task UserConnected(string connectionId, string username);
    Task UserConnected(string username);
    Task UserDisconnected(string connectionId);
    Task Typing(string connectionId);
}