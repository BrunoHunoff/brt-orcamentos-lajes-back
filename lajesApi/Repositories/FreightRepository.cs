using Microsoft.EntityFrameworkCore;

public class FreightRepository {
    private readonly AppDbContext dbContext;

    public FreightRepository(AppDbContext AppdbContext) {
        dbContext = AppdbContext;
    }

    private async Task<bool> FreightExists(string city, string state) {
        return await dbContext.freights.AnyAsync(freight => freight.City == city && freight.State == state);
    }

    
    //CREATE
    public async Task AddfreightAsync(Freight freight) {

        if (await FreightExists(freight.City, freight.State)) throw new Exception("city already registered");

        await dbContext.freights.AddAsync(freight);
        await dbContext.SaveChangesAsync();
    }

    //READ ALL
    public async Task<List<Freight>> GetAllFreights() => 
        await dbContext.freights.ToListAsync();

    //READ ID
    public async Task<Freight> GetFreightById(int id) {
        return await dbContext.freights.SingleOrDefaultAsync(freight => freight.Id == id);
    }
    
    //UPDATE
    public async Task<Freight?> Updatefreight(int id, Freight newFreight){

        var freight = await GetFreightById(id);

        if (freight is null) throw new KeyNotFoundException("Id not found");

        freight.Updatefreight(newFreight.City, newFreight.State, newFreight.Price);

        await dbContext.SaveChangesAsync();

        return freight;
    }

    //DELETE
    public async Task Deletefreight(int id) {
        var freight = await GetFreightById(id);

        if (freight is null) throw new KeyNotFoundException("Id not found");

        dbContext.freights.Remove(freight);

        await dbContext.SaveChangesAsync();
    }
}
