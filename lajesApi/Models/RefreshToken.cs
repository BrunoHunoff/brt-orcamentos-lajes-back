public class RefreshToken
{
    public int id { get; init; }
    public string Token { get; set; }
    public DateTime ExpirationDate { get; set; }
    public DateTime CreationDate { get; set; }
    public int UserId { get; set; }

}
