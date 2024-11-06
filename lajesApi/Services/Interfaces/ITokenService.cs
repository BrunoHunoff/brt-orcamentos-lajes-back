public interface ITokenService
{
    Task<string> GenerateToken(LoginDto loginDto);
}