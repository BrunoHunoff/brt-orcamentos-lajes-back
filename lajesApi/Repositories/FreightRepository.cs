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
    public async Task AddFreightAsync(Freight freight) {

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
    public async Task<Freight?> UpdateFreight(int id, AddFreightRequest newFreight){

        var freight = await GetFreightById(id);

        if (freight is null) throw new KeyNotFoundException("Id not found");

        freight.Updatefreight(newFreight.city, newFreight.state, newFreight.price);

        await dbContext.SaveChangesAsync();

        return freight;
    }

    //DELETE
    public async Task DeleteFreight(int id) {
        var freight = await GetFreightById(id);

        if (freight is null) throw new KeyNotFoundException("Id not found");

        dbContext.freights.Remove(freight);

        await dbContext.SaveChangesAsync();
    }
}
