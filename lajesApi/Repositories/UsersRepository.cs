using Microsoft.EntityFrameworkCore;

public class UsersRepository{
    private readonly AppDbContext dbContext;
    private readonly PasswordHasher passwordHasher;

    public UsersRepository(AppDbContext AppdbContext, PasswordHasher PasswordHasher) {
        dbContext = AppdbContext;
        passwordHasher = PasswordHasher;
    }

    public async Task<User?> GetUserByEmail(string email) => await dbContext.users.SingleOrDefaultAsync(user => user.Email == email);

    public async Task<User?> GetUserByIdAsync(int id) => await dbContext.users.SingleOrDefaultAsync(user => user.Id == id);

    public async Task Add(User user) {

        user.Password = passwordHasher.Hash(user.Password);

        await dbContext.users.AddAsync(user);
        await dbContext.SaveChangesAsync();
    }

    public async Task<List<User>> GetUsers() => await dbContext.users.ToListAsync();
    
}