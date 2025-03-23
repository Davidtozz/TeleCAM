namespace Api.Endpoints.User; 

using Api.Endpoints;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;

public partial class UserEndpoint {
    
    /// <summary>
    /// Deletes a user
    /// </summary>
    /// <param name="id"></param>
    /// <param name="userService"></param>
    /// <returns></returns>
    public async Task<IResult> DeleteUser([FromRoute] Guid id, IUserService userService)
    {
        var user = await userService.GetUserByIdAsync(id);
        if (user is null)
        {
            return Results.NotFound();
        }
        await userService.DeleteUserAsync(user.Id);
        return Results.Ok("User deleted");
    }
}