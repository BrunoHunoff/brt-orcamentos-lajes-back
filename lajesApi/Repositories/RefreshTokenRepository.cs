using Microsoft.EntityFrameworkCore;

public class RefreshTokensRepository {
    private readonly AppDbContext dbContext;

    public RefreshTokensRepository(AppDbContext AppdbContext) {
        dbContext = AppdbContext;
    }    
    //CREATE
    public async Task AddRefreshTokenAsync(RefreshToken refreshToken) {

        var existingTokens = await dbContext.refreshTokens
        .Where(rt => rt.UserId == refreshToken.UserId)
        .ToListAsync();

        foreach (var existingToken in existingTokens) {
            dbContext.refreshTokens.Remove(existingToken);
        }

        await dbContext.refreshTokens.AddAsync(refreshToken);
        await dbContext.SaveChangesAsync();
    }

    //READ ID
    public async Task<RefreshToken> GetRefreshTokenByToken(string token) {
        return await dbContext.refreshTokens.SingleOrDefaultAsync(refreshToken => refreshToken.Token == token);
    }
    

    //DELETE
    public async Task DeleterefreshToken(string token) {
        var refreshToken = await GetRefreshTokenByToken(token);

        if (refreshToken is null) throw new KeyNotFoundException("Token not found");

        dbContext.refreshTokens.Remove(refreshToken);

        await dbContext.SaveChangesAsync();
    }
}
