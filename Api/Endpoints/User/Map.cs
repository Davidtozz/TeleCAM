namespace Api.Endpoints.User;

public partial class UserEndpoint : IEndpoint
{
    public void Map(WebApplication app)
    {
        var group = app.MapGroup("/user");

        group.MapGet("/{id}", GetUser);
        group.MapPost("/", CreateUser);
        group.MapDelete("/{id}", DeleteUser);
    }
}