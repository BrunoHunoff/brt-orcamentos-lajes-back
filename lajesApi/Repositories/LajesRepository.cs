using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

public class slabRepository {
    private readonly AppDbContext dbContext;

    public slabRepository(AppDbContext AppdbContext) {
        dbContext = AppdbContext;
    }

    private async Task<bool> slabExists(string name) {
        return await dbContext.slabs.AnyAsync(slab => slab.Name == name);
    }

    
    //CREATE
    public async Task AddslabAsync(Slab slab) {

        if (await slabExists(slab.Name)) throw new Exception("name already in use");

        await dbContext.slabs.AddAsync(slab);
        await dbContext.SaveChangesAsync();
    }

    //READ ALL
    public async Task<List<Slab>> GetAllslabs() => 
        await dbContext.slabs.ToListAsync();

    //READ ID
    public async Task<Slab> GetslabById(Guid id) {
        return await dbContext.slabs.SingleOrDefaultAsync(slab => slab.Id == id);
    }
    
    //UPDATE
    public async Task<Slab?> Updateslab(Guid id, Slab novaslab){

        var slab = await GetslabById(id);

        if (slab is null) throw new KeyNotFoundException("Id not found");

        slab.UpdateSlab(novaslab.Name, novaslab.Price, novaslab.Weight);

        await dbContext.SaveChangesAsync();

        return slab;
    }

    //DELETE
    public async Task Deleteslab(Guid id) {
        var slab = await GetslabById(id);

        if (slab is null) throw new KeyNotFoundException("Id not found");

        dbContext.slabs.Remove(slab);

        await dbContext.SaveChangesAsync();
    }
}
