using Microsoft.EntityFrameworkCore;

public class BudgetSlabsRepository
{
    private readonly AppDbContext dbContext;
    private readonly SlabsRepository slabsRepository;
    private readonly BudgetRepository budgetRepository;

    public BudgetSlabsRepository(
        AppDbContext AppdbContext,
        SlabsRepository SlabsRepository,
        BudgetRepository BudgetRepository
    )
    {
        dbContext = AppdbContext;
        slabsRepository = SlabsRepository;
        budgetRepository = BudgetRepository;
    }

    //CREATE
    public async Task AddBudgetSlabAsync(BudgetSlab budgetSlab)
    {
        await VerifyBudgetSlabData(budgetSlab);

        await dbContext.budgetSlabs.AddAsync(budgetSlab);
        await dbContext.SaveChangesAsync();
    }

    //READ ALL
    public async Task<List<BudgetSlab>> GetAllBudgetSlabs() =>
        await dbContext.budgetSlabs.ToListAsync();

    //READ ID
    public async Task<BudgetSlab> GetBudgetSlabById(int id)
    {
        return await dbContext.budgetSlabs.SingleOrDefaultAsync(budgetSlab => budgetSlab.Id == id);
    }

    //UPDATE
    public async Task<BudgetSlab?> UpdateBudgetSlab(int id, BudgetSlab newBudgetSlab)
    {
        var budgetSlab = await GetBudgetSlabById(id);

        if (budgetSlab is null)
            throw new KeyNotFoundException("BudgetSlab Id not found");

        await VerifyBudgetSlabData(newBudgetSlab);

        if (await slabsRepository.GetslabById(newBudgetSlab.SlabId) is null)
            throw new KeyNotFoundException("SlabId not found");

        budgetSlab.UpdateBudgetSlab(
            newBudgetSlab.SlabId,
            newBudgetSlab.SlabsNumber,
            newBudgetSlab.Overload,
            newBudgetSlab.Width,
            newBudgetSlab.Length
        );

        await dbContext.SaveChangesAsync();

        return budgetSlab;
    }

    //DELETE
    public async Task DeleteBudgetSlab(int id)
    {
        var budgetSlab = await GetBudgetSlabById(id);

        if (budgetSlab is null)
            throw new KeyNotFoundException("Id not found");

        dbContext.budgetSlabs.Remove(budgetSlab);

        await dbContext.SaveChangesAsync();
    }

    private async Task VerifyBudgetSlabData(BudgetSlab budgetSlab)
    {
        if (await budgetRepository.GetBudgetById(budgetSlab.BudgetId) is null)
            throw new KeyNotFoundException("BudgetId not found");

        if (await slabsRepository.GetslabById(budgetSlab.SlabId) is null)
            throw new KeyNotFoundException("SlabId not found");
    }
}
