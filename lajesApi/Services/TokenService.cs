public class TokenService 
{
    public void SetTokensCookie(string token, string refreshToken, HttpContext context)
    {
        context.Response.Cookies.Append("userToken", token, new CookieOptions
        {
            Expires = DateTimeOffset.UtcNow.AddMinutes(15),
            Secure = true,
            HttpOnly = true,
            SameSite = SameSiteMode.Strict
        });

        context.Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
        {
            Expires = DateTimeOffset.UtcNow.AddMinutes(30),
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict
        });
    }

}
