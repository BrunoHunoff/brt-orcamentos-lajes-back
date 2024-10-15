using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Bcpg.Sig;

public class BudgetRepository
{
    private readonly AppDbContext dbContext;
    private readonly CostumersRepository costumersRepository;
    private readonly FreightRepository freightRepository;

    public BudgetRepository(
        AppDbContext AppdbContext,
        FreightRepository FreightRepository,
        CostumersRepository CostumersRepository
    )
    {
        dbContext = AppdbContext;
        freightRepository = FreightRepository;
        costumersRepository = CostumersRepository;
    }

    //CREATE
    public async Task AddBudgetAsync(Budget budget)
    {
        await VerifyBudgetData(budget);

        await dbContext.budgets.AddAsync(budget);
        await dbContext.SaveChangesAsync();
    }

    //READ ALL
    public async Task<List<Budget>> GetAllBudget() =>
        await dbContext.budgets.Include(b => b.Slabs).ToListAsync();

    //READ ID
    public async Task<Budget> GetBudgetById(int id)
    {
        return await dbContext
            .budgets.Include(b => b.Slabs)
            .SingleOrDefaultAsync(budget => budget.Id == id);
    }

    //UPDATE
    public async Task<Budget?> UpdateBudget(int id, Budget newBudget)
    {
        var budget = await GetBudgetById(id);

        if (budget is null)
            throw new KeyNotFoundException("Id not found");

        await VerifyBudgetData(newBudget);

        budget.UpdateBudget(
            newBudget.CostumerId,
            newBudget.CostumerName,
            newBudget.Footage,
            newBudget.Value,
            newBudget.City,
            newBudget.State,
            newBudget.FreightId
        );

        if (budget is null)
            throw new Exception("Error creating budget");

        await dbContext.SaveChangesAsync();

        Console.WriteLine(budget.ToString());
        Console.WriteLine("Oi");

        return budget;
    }

    //DELETE
    public async Task DeleteBudget(int id)
    {
        var budget = await GetBudgetById(id);

        if (budget is null)
            throw new KeyNotFoundException("Id not found");

        dbContext.budgets.Remove(budget);

        await dbContext.SaveChangesAsync();
    }

    private async Task VerifyBudgetData(Budget budget)
    {
        if (await costumersRepository.GetCostumerById(budget.CostumerId) is null)
            throw new KeyNotFoundException("Invalid costumerId");

        if (await freightRepository.GetFreightById(budget.FreightId) is null)
            throw new KeyNotFoundException("Invalid freightId");
    }
}
