using Microsoft.AspNetCore.Identity;

public class User : IdentityUser{
    public int Id { get; init; }
    public string Name { get; set;}
    public string Email { get; set;}
    public string Password { get; set;}
}