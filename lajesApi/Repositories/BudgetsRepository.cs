using Microsoft.EntityFrameworkCore;

public class BudgetRepository {
    private readonly AppDbContext dbContext;

    public BudgetRepository(AppDbContext AppdbContext) {
        dbContext = AppdbContext;
    }
    
    //CREATE
    public async Task AddBudgetAsync(Budget budget) {

        await dbContext.budgets.AddAsync(budget);
        await dbContext.SaveChangesAsync();
    }

    //READ ALL
    public async Task<List<Budget>> GetAllBudget() => 
        await dbContext.budgets.ToListAsync();

    //READ ID
    public async Task<Budget> GetBudgetById(int id) {
        return await dbContext.budgets.SingleOrDefaultAsync(budget => budget.Id == id);
    }
    
    //UPDATE
    public async Task<Budget?> UpdateBudget(int id, UpdateBudgetRequest newBudget){

        var budget = await GetBudgetById(id);

        if (budget is null) throw new KeyNotFoundException("Id not found");

        budget.UpdateBudget(
            newBudget.costumerId, 
            newBudget.costumerName, 
            newBudget.footage, 
            newBudget.value, 
            newBudget.city, 
            newBudget.state, 
            newBudget.freight, 
            newBudget.budgetSlabs
        );

        await dbContext.SaveChangesAsync();

        return budget;
    }

    //DELETE
    public async Task DeleteBudget(int id) {
        var budget = await GetBudgetById(id);

        if (budget is null) throw new KeyNotFoundException("Id not found");

        dbContext.budgets.Remove(budget);

        await dbContext.SaveChangesAsync();
    }
}
