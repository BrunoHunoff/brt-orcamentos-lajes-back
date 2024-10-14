using Microsoft.AspNetCore.Mvc;
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
        await dbContext.budgets.Include(b => b.Slabs).ToListAsync();


    //READ ID
    public async Task<Budget> GetBudgetById(int id) {
    return await dbContext.budgets
        .Include(b => b.Slabs)
        .SingleOrDefaultAsync(budget => budget.Id == id);
}

    
    //UPDATE
    public async Task<Budget?> UpdateBudget(int id, Budget newBudget){

        var budget = await GetBudgetById(id);

        if (budget is null) throw new KeyNotFoundException("Id not found");

        budget.UpdateBudget(
            newBudget.CostumerId, 
            newBudget.CostumerName, 
            newBudget.Footage, 
            newBudget.Value, 
            newBudget.City, 
            newBudget.State, 
            newBudget.FreightId
        );
        
        if (budget is null) throw new Exception("Error creating budget");


        await dbContext.SaveChangesAsync();
        
        Console.WriteLine(budget.ToString());
        Console.WriteLine("Oi");


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
