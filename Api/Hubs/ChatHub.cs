using Microsoft.AspNetCore.SignalR;
using SignalRSwaggerGen.Attributes;

namespace Api;

[SignalRHub]
public class ChatHub : Hub<IChatClient>
{
       
}