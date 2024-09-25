using Microsoft.EntityFrameworkCore;

public class BudgetSummaryRepository {
    private readonly AppDbContext dbContext;

    public BudgetSummaryRepository(AppDbContext AppdbContext) {
        dbContext = AppdbContext;
    }
    
    //CREATE
    public async Task AddBudgetSummaryAsync(BudgetSummary budgetSummary) {

        await dbContext.budgetSummaries.AddAsync(budgetSummary);
        await dbContext.SaveChangesAsync();
    }

    //READ ALL
    public async Task<List<BudgetSummary>> GetAllBudgetSummarys() => 
        await dbContext.budgetSummaries.ToListAsync();

    //READ ID
    public async Task<BudgetSummary> GetBudgetSummaryById(int id) {
        return await dbContext.budgetSummaries.SingleOrDefaultAsync(budgetSummary => budgetSummary.Id == id);
    }
    
    //UPDATE
    public async Task<BudgetSummary?> UpdateBudgetSummary(int id, BudgetSummary newBudgetSummary){

        var budgetSummary = await GetBudgetSummaryById(id);

        if (budgetSummary is null) throw new KeyNotFoundException("Id not found");

        budgetSummary.UpdateBudgetSummary(newBudgetSummary.Contribution, newBudgetSummary.Administration, newBudgetSummary.Taxes, newBudgetSummary.Extra, newBudgetSummary.FreightWeight);

        await dbContext.SaveChangesAsync();

        return budgetSummary;
    }

    //DELETE
    public async Task DeleteBudgetSummary(int id) {
        var budgetSummary = await GetBudgetSummaryById(id);

        if (budgetSummary is null) throw new KeyNotFoundException("Id not found");

        dbContext.budgetSummaries.Remove(budgetSummary);

        await dbContext.SaveChangesAsync();
    }
}
