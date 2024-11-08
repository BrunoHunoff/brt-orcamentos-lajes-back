using Microsoft.AspNetCore.Authorization;

public static class AuthEndpoints
{
    [Authorize]
    public static void AddAuthEndpoints(this WebApplication app)
    {
        // Endpoint para login
        app.MapPost(
            "/login",
            async (
                LoginDto loginDto,
                TokenService tokenservice,
                AuthService authService,
                HttpContext context
            ) =>
            {
                var (token, refreshToken) = await authService.LoginAsync(
                    loginDto.Email,
                    loginDto.Password
                );

                if (string.IsNullOrEmpty(token))
                {
                    return Results.Unauthorized();
                }

                tokenservice.SetTokensCookie(token, refreshToken, context);

                return Results.Ok("Token: " + token + "RefreshToken: " + refreshToken);
            }
        );

        app.MapPost(
            "/refresh",
            async (
                string refreshToken,
                AuthService authService,
                RefreshTokensRepository refreshTokensRepo,
                TokenService tokenService,
                IConfiguration configuration,
                HttpContext context
            ) =>
            {
                var (newToken, newRefreshToken) = await authService.Refresh(refreshToken);

                if (string.IsNullOrEmpty(newToken))
                {
                    return Results.Unauthorized();
                }

                tokenService.SetTokensCookie(newToken, newRefreshToken, context);

                return Results.Ok("Token: " + newToken + "RefreshToken: " + newRefreshToken);
            }
        );
    }
}
