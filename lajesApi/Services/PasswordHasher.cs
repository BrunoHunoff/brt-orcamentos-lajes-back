public class PasswordHasher : IPasswordHasher
{
    public string Hash(string password)
    {
        string passwordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(password, 11);

        return passwordHash;
    }

    public bool Verify(string hashedPassword, string password)
    {
        return BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
    }

}