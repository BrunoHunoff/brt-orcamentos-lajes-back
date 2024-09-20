using Microsoft.EntityFrameworkCore;

public class LajeRepository {
    private readonly AppDbContext dbContext;

    public LajeRepository(AppDbContext AppdbContext) {
        dbContext = AppdbContext;
    }

    public async Task<bool> LajeExists(string name) {
        return await dbContext.Lajes.AnyAsync(Laje => Laje.Name == name);
    }

    
    //CREATE
    public async Task AddLajeAsync(Laje laje) {
        await dbContext.Lajes.AddAsync(laje);
        await dbContext.SaveChangesAsync();
    }

    //READ ALL
    public async Task<List<Laje>> GetAllLajes() => 
        await dbContext.Lajes.ToListAsync();

    //READ ID
    public async Task<Laje> GetLajeById(Guid id) => 
        await dbContext.Lajes.SingleOrDefaultAsync(laje => laje.Id == id);

    
    //UPDATE


    //DELETE
}
