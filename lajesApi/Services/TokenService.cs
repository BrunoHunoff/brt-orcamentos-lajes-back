using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

public class TokenService : ITokenService 
{
    private readonly IConfiguration _configuration;
    private readonly UsersRepository _usersRepository;

    public TokenService(IConfiguration configuration, UsersRepository usersRepository)
    {
        _configuration = configuration;
        _usersRepository = usersRepository;
    }
    public async Task<string> GenerateToken(User user)
    {
        var userDataBase = await _usersRepository.GetUserById(user.Id);


        if (user.Name != userDataBase.Name || user.Password != userDataBase.Password) return String.Empty;

        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? string.Empty));
        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];

        var sigingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var tokenOptions = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: sigingCredentials
        );

        var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        return token;
    }
}