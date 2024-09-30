using Microsoft.AspNetCore.Mvc;

public static class BudgetSummaryController
{

    public static void AddBudgetSummaryEndpoints(this WebApplication app)
    {
        var budgetSummaryEndpoints = app.MapGroup("budgetSummarys");

        //GET ALL
        budgetSummaryEndpoints.MapGet("", async ([FromServices] BudgetSummaryRepository budgetSummaryRepository) =>
            await budgetSummaryRepository.GetAllBudgetSummarys());

        //GET ID
        budgetSummaryEndpoints.MapGet("{id:int}", async ([FromRoute] int id, BudgetSummaryRepository budgetSummaryRepository) =>
        {
            var budgetSummary = await budgetSummaryRepository.GetBudgetSummaryById(id);

            if (budgetSummary is null) return Results.NotFound();

            return Results.Ok(budgetSummary);
        });

        //POST
        budgetSummaryEndpoints.MapPost("", async (AddBudgetSummaryRequest request, [FromServices] BudgetSummaryRepository budgetSummaryRepository) =>
        {
            var newBudgetSummary = new BudgetSummary(
                request.budgetId,
                request.contribution,
                request.administration,
                request.taxes,
                request.extra,
                request.freightWeight
            );

            await budgetSummaryRepository.AddBudgetSummaryAsync(newBudgetSummary);
            return Results.Ok(newBudgetSummary);
        });

        //PUT
        budgetSummaryEndpoints.MapPut("{id:int}", async ([FromRoute] int id, [FromBody] AddBudgetSummaryRequest newBudgetSummary, [FromServices] BudgetSummaryRepository budgetSummaryRepository) =>
        {
            try
            {
                var budgetSummary = await budgetSummaryRepository.UpdateBudgetSummary(id, newBudgetSummary);
                return Results.Ok(budgetSummary);
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
        budgetSummaryEndpoints.MapDelete("{id:int}", async ([FromRoute] int id, [FromServices] BudgetSummaryRepository budgetSummaryRepository) =>
        {
            try
            {
                await budgetSummaryRepository.DeleteBudgetSummary(id);
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