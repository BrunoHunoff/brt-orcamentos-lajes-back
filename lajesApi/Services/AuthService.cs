using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

public class AuthService
{
    private readonly IConfiguration _configuration;
    private readonly RefreshTokensRepository _refreshTokensRepository;
    private readonly UsersRepository _usersRepository;
    private readonly PasswordHasher _passwordHasher;

    public AuthService(
        UsersRepository usersRepository,
        IConfiguration configuration,
        RefreshTokensRepository refreshTokensRepository,
        PasswordHasher passwordHasher
    )
    {
        _configuration = configuration;
        _refreshTokensRepository = refreshTokensRepository;
        _usersRepository = usersRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<(string token, string refreshToken)> LoginAsync(string email, string password)
    {
        Console.WriteLine("HEREEEE");

        var user = await _usersRepository.GetUserByEmail(email);

        if (user == null || !_passwordHasher.Verify(user.Password,password))
        {
            return (string.Empty, string.Empty);
        }

        var token = GenerateJwtToken(user);
        var refreshToken = GenerateRefreshToken(user.Id);

        await _refreshTokensRepository.AddRefreshTokenAsync(refreshToken);

        return (token, refreshToken.Token);
    }

    public async Task<(string token, string refreshToken)> Refresh(string userRefreshToken)
    {
        var dbRefreshToken = await _refreshTokensRepository.GetRefreshTokenByToken(userRefreshToken);

        if (dbRefreshToken is null || dbRefreshToken.ExpirationDate < DateTime.Now) return (string.Empty, string.Empty);

        User user = await _usersRepository.GetUserByIdAsync(dbRefreshToken.UserId);

        var token = GenerateJwtToken(user);
        var refreshToken = GenerateRefreshToken(user.Id);

        await _refreshTokensRepository.AddRefreshTokenAsync(refreshToken);

        return (token, refreshToken.Token);
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private RefreshToken GenerateRefreshToken(int userId)
    {
        RefreshToken refreshToken = new RefreshToken();

        refreshToken.Token = Guid.NewGuid().ToString("N");
        refreshToken.UserId = userId;
        refreshToken.CreationDate = DateTime.Now;
        refreshToken.ExpirationDate = DateTime.Now.AddDays(1);

        return refreshToken;
    }
}
