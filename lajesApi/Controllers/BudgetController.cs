using Microsoft.AspNetCore.Mvc;

public static class BudgetController
{

    public static void AddBudgetEndpoints(this WebApplication app)
    {
        var budgetEndpoints = app.MapGroup("budgets");

        //GET ALL
        budgetEndpoints.MapGet("", async ([FromServices] BudgetRepository budgetRepository) =>
            await budgetRepository.GetAllBudget());

        //GET ID
        budgetEndpoints.MapGet("{id:int}", async ([FromRoute] int id, BudgetRepository budgetRepository) =>
        {
            var budget = await budgetRepository.GetBudgetById(id);

            if (budget is null) return Results.NotFound();

            return Results.Ok(budget);
        });

        //POST
        budgetEndpoints.MapPost("", async (AddBudgetRequest request, [FromServices] BudgetRepository budgetRepository) =>
        {
            var newBudget = new Budget(
                request.costumerId,
                request.costumerName,
                request.footage,
                request.value,
                request.city,
                request.state,
                request.freight
            );

            await budgetRepository.AddBudgetAsync(newBudget);
            return Results.Ok(newBudget);
        });

        //PUT
        budgetEndpoints.MapPut("{id:int}", async ([FromRoute] int id, [FromBody] AddBudgetRequest newBudget, [FromServices] BudgetRepository budgetRepository) =>
        {
            try
            {
                var budget = await budgetRepository.UpdateBudget(id, newBudget);
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


        //DELETE
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