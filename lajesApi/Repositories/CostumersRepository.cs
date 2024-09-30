using Microsoft.EntityFrameworkCore;

public class CostumersRepository {
    private readonly AppDbContext dbContext;

    public CostumersRepository(AppDbContext AppdbContext) {
        dbContext = AppdbContext;
    }

    private async Task<bool> CostumerExists(string name) {
        return await dbContext.costumers.AnyAsync(costumer => costumer.Name == name);
    }

    
    //CREATE
    public async Task AddCostumerAsync(Costumer costumer) {

        if (await CostumerExists(costumer.Name)) throw new Exception("name already in use");

        await dbContext.costumers.AddAsync(costumer);
        await dbContext.SaveChangesAsync();
    }

    //READ ALL
    public async Task<List<Costumer>> GetAllCostumers() => 
        await dbContext.costumers.ToListAsync();

    //READ ID
    public async Task<Costumer> GetCostumerById(int id) {
        return await dbContext.costumers.SingleOrDefaultAsync(costumer => costumer.Id == id);
    }
    
    //UPDATE
    public async Task<Costumer?> Updatecostumer(int id, AddCostumerRequest newCostumer){

        var costumer = await GetCostumerById(id);

        if (costumer is null) throw new KeyNotFoundException("Id not found");

        costumer.Updatecostumer(newCostumer.Name, newCostumer.Pj, newCostumer.CnpjCpf, newCostumer.Cep, newCostumer.City, newCostumer.State, newCostumer.Address, newCostumer.AddressNumber, newCostumer.Email, newCostumer.PhoneNumber);

        await dbContext.SaveChangesAsync();

        return costumer;
    }

    //DELETE
    public async Task Deletecostumer(int id) {
        var costumer = await GetCostumerById(id);

        if (costumer is null) throw new KeyNotFoundException("Id not found");

        dbContext.costumers.Remove(costumer);

        await dbContext.SaveChangesAsync();
    }
}
