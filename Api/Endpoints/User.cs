using Microsoft.AspNetCore.Http.HttpResults;

namespace Api.Endpoints;

public class User : IEndpoint
{
    public void Map(WebApplication app)
    {
        app.MapGroup("/user")
            .MapGet("/hello", SayHello);
    }
    
    private IResult SayHello()
    {
        return Results.Ok("Hello World");
    }
}