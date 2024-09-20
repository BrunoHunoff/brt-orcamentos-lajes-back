using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

public class LajeRepository {
    private readonly AppDbContext dbContext;

    public LajeRepository(AppDbContext AppdbContext) {
        dbContext = AppdbContext;
    }

    private async Task<bool> LajeExists(string name) {
        return await dbContext.Lajes.AnyAsync(Laje => Laje.Name == name);
    }

    
    //CREATE
    public async Task AddLajeAsync(Laje laje) {

        if (await LajeExists(laje.Name)) throw new Exception("name already in use");

        await dbContext.Lajes.AddAsync(laje);
        await dbContext.SaveChangesAsync();
    }

    //READ ALL
    public async Task<List<Laje>> GetAllLajes() => 
        await dbContext.Lajes.ToListAsync();

    //READ ID
    public async Task<Laje> GetLajeById(Guid id) {
        return await dbContext.Lajes.SingleOrDefaultAsync(laje => laje.Id == id);
    }

    
    //UPDATE
    public async Task<Laje?> UpdateLaje(Guid id, Laje novaLaje){

        var laje = await GetLajeById(id);

        laje.UpdateLaje(novaLaje.Name, novaLaje.Price);

        await dbContext.SaveChangesAsync();

        return laje;
    }
    


    //DELETE
}
