using Microsoft.EntityFrameworkCore;

public class BudgetSlabsRepository {
    private readonly AppDbContext dbContext;

    private readonly SlabsRepository slabsRepository;

    public BudgetSlabsRepository(AppDbContext AppdbContext) {
        dbContext = AppdbContext;
    }
    
    //CREATE
    public async Task AddBudgetSlabAsync(BudgetSlab budgetSlab) {


        await dbContext.budgetSlabs.AddAsync(budgetSlab);
        await dbContext.SaveChangesAsync();
    }

    //READ ALL
    public async Task<List<BudgetSlab>> GetAllBudgetSlabs() => 
        await dbContext.budgetSlabs.ToListAsync();

    //READ ID
    public async Task<BudgetSlab> GetBudgetSlabById(int id) {
        return await dbContext.budgetSlabs.SingleOrDefaultAsync(budgetSlab => budgetSlab.Id == id);
    }
    
    //UPDATE
    public async Task<BudgetSlab?> UpdateBudgetSlab(int id, BudgetSlab newBudgetSlab){

        var budgetSlab = await GetBudgetSlabById(id);

        if (budgetSlab is null) throw new KeyNotFoundException("BudgetSlab Id not found");


        if (await slabsRepository.GetslabById(newBudgetSlab.SlabId) is null)
            throw new KeyNotFoundException("SlabId not found");

        budgetSlab.UpdateBudgetSlab(newBudgetSlab.SlabId, newBudgetSlab.SlabsNumber, newBudgetSlab.Overload ,newBudgetSlab.Width, newBudgetSlab.Length);

        await dbContext.SaveChangesAsync();

        return budgetSlab;
    }

    //DELETE
    public async Task DeleteBudgetSlab(int id) {
        var budgetSlab = await GetBudgetSlabById(id);

        if (budgetSlab is null) throw new KeyNotFoundException("Id not found");

        dbContext.budgetSlabs.Remove(budgetSlab);

        await dbContext.SaveChangesAsync();
    }
}
