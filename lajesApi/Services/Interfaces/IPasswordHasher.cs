public interface IPasswordHasher
{
    string Hash(string password);
    bool Verify(string passwordHashm, string inputPassword);
}