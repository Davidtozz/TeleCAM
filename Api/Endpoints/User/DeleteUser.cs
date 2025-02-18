namespace Api.Endpoints.User; 

using Api.Endpoints;
using Api.Services;
using Microsoft.AspNetCore.Mvc;


public partial class UserEndpoint {
        private async Task<IResult> DeleteUser([FromRoute] int id, IUserService userService)
    {
        var user = await userService.GetUserByIdAsync(id);
        if (user is null)
        {
            return Results.NotFound();
        }
        await userService.DeleteUserAsync(user.Id);
        return Results.Ok();
    }
}