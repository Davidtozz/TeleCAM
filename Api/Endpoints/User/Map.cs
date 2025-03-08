using Microsoft.AspNetCore.Authorization;

namespace Api.Endpoints.User;


public partial class UserEndpoint : IEndpoint
{
    public void Map(WebApplication app)
    {
        var group = app.MapGroup("/user");

        group.MapGet("/{id}", GetUser);
        group.MapPost("/register", Register);
        group.MapPost("/login",  Login);
        group.MapDelete("/{id}", DeleteUser);
        group.MapPost("/refresh", Refresh);
    }
}