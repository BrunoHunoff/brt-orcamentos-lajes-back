public static class AuthEndpoints
{
    public static void AddAuthEndpoints(this WebApplication app)
    {
        app.MapPost("/login", async (LoginDto loginDto, TokenService tokenService) =>
        {
            var token = await tokenService.GenerateToken(loginDto);

            if (string.IsNullOrEmpty(token))
            {
                return Results.Unauthorized();
            }

            return Results.Ok(new { Token = token });
        });
    }
}
