using Microsoft.AspNetCore.Mvc;

public static class UserController
{

    public static void AddUsersEndpoints(this WebApplication app)
    {
        var userEndpoints = app.MapGroup("users");

        //GET ALL
        userEndpoints.MapGet("", async ([FromServices] UsersRepository usersRepository) =>
            await usersRepository.GetUsers());

        

        //POST
        userEndpoints.MapPost("", async (User request, [FromServices] UsersRepository usersRepository) =>
        {

            await usersRepository.Add(request);
            return Results.Ok();
        });

        
    }


}