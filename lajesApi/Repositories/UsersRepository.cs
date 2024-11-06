using Microsoft.EntityFrameworkCore;

public class UsersRepository{
    private readonly AppDbContext dbContext;

    public UsersRepository(AppDbContext AppdbContext) {
        dbContext = AppdbContext;
    }

    public async Task<User?> GetUserById(int id) => await dbContext.users.SingleOrDefaultAsync(user => user.Id == id);

    public async Task Add(User user) {
        await dbContext.users.AddAsync(user);
        await dbContext.SaveChangesAsync();
    }

    public async Task<List<User>> GetUsers() => await dbContext.users.ToListAsync();
    
}