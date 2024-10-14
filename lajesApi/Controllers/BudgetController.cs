using Microsoft.AspNetCore.Mvc;

public static class BudgetController
{
    public static void AddBudgetEndpoints(this WebApplication app)
    {
        var budgetEndpoints = app.MapGroup("budgets");

        // GET ALL
        budgetEndpoints.MapGet("", async ([FromServices] BudgetRepository budgetRepository) =>
            await budgetRepository.GetAllBudget());

        // GET BY ID
        budgetEndpoints.MapGet("{id:int}", async ([FromRoute] int id, BudgetRepository budgetRepository) =>
        {
            var budget = await budgetRepository.GetBudgetById(id);

            if (budget is null) return Results.NotFound();

            return Results.Ok(budget);
        });

        // POST
        budgetEndpoints.MapPost("", async (AddBudgetRequest request, [FromServices] BudgetRepository budgetRepository, [FromServices] FreightRepository freightRepository) =>
        {
            var newBudget = new Budget(
                request.costumerId,
                request.costumerName,
                request.footage,
                request.value,
                request.city,
                request.state
            );


            foreach (AddBudgetSlabRequest slabRequest in request.slabs)
            {
                var budgetSlab = new BudgetSlab(
                    slabRequest.slabId,        
                    0,                         
                    slabRequest.slabsNumber,   
                    slabRequest.length,        
                    slabRequest.width          
                );

                newBudget.Slabs.Add(budgetSlab);
            }


            if (request.freightId.HasValue)
            {
                var freight = await freightRepository.GetFreightById(request.freightId.Value);
                if (freight != null)
                {
                    newBudget.SetFreight(freight);
                }
            }


            await budgetRepository.AddBudgetAsync(newBudget);


            return Results.Ok(newBudget);
        });

        // PUT
        budgetEndpoints.MapPut("{id:int}", async ([FromRoute] int id, [FromBody] UpdateBudgetRequest newBudgetRequest, [FromServices] BudgetRepository budgetRepository) =>
        {
            try
            {
                var budget = await budgetRepository.UpdateBudget(id, newBudgetRequest);
                return Results.Ok(budget);
            }
            catch (KeyNotFoundException e)
            {
                return Results.NotFound(e.Message);
            }
            catch (Exception e)
            {
                return Results.Problem("An unexpected error occurred: " + e.Message);
            }
        });

        // DELETE
        budgetEndpoints.MapDelete("{id:int}", async ([FromRoute] int id, [FromServices] BudgetRepository budgetRepository) =>
        {
            try
            {
                await budgetRepository.DeleteBudget(id);
                return Results.NoContent();
            }
            catch (KeyNotFoundException e)
            {
                return Results.NotFound(e.Message);
            }
            catch (Exception e)
            {
                return Results.Problem("An unexpected error occurred: " + e.Message);
            }
        });
    }
}
