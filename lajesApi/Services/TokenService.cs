using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

public class TokenService : ITokenService 
{
    private readonly IConfiguration _configuration;
    private readonly UsersRepository _usersRepository;
    private readonly PasswordHasher _passwordHasher;

    public TokenService(IConfiguration configuration, UsersRepository usersRepository, PasswordHasher passwordHasher)
    {
        _configuration = configuration;
        _usersRepository = usersRepository;
        _passwordHasher = passwordHasher;
    }
    public async Task<string> GenerateToken(LoginDto loginDto)
    {
        var userDataBase = await _usersRepository.GetUserByEmail(loginDto.Email);

        if (userDataBase == null) return String.Empty;

        if (loginDto.Email != userDataBase.Email || !_passwordHasher.Verify(userDataBase.Password, loginDto.Password)) return String.Empty;

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