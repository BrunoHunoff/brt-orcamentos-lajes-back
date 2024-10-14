using System.Data.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public static class BudgetController
{
    public static void AddBudgetEndpoints(this WebApplication app)
    {
        var budgetEndpoints = app.MapGroup("budgets");

        // GET ALL
        budgetEndpoints.MapGet(
            "",
            async ([FromServices] BudgetRepository budgetRepository) =>
                await budgetRepository.GetAllBudget()
        );

        // GET BY ID
        budgetEndpoints.MapGet(
            "{id:int}",
            async ([FromRoute] int id, BudgetRepository budgetRepository) =>
            {
                var budget = await budgetRepository.GetBudgetById(id);

                if (budget is null)
                    return Results.NotFound();

                return Results.Ok(budget);
            }
        );

        // POST
        budgetEndpoints.MapPost(
            "",
            async (
                AddBudgetRequest request,
                [FromServices] BudgetRepository budgetRepository,
                [FromServices] FreightRepository freightRepository
            ) =>
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
                        slabRequest.overload,
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
            }
        );

        // PUT
        budgetEndpoints.MapPut(
            "{id:int}",
            async (
                [FromRoute] int id,
                [FromBody] UpdateBudgetRequest request,
                [FromServices] BudgetRepository budgetRepository,
                [FromServices] BudgetSlabsRepository budgetSlabsRepository,
                [FromServices] FreightRepository freightRepository
            ) =>
            {
                try
                {
                    var updatedBudget = new Budget(
                        request.costumerId,
                        request.costumerName,
                        request.footage,
                        request.value,
                        request.city,
                        request.state,
                        request.freightId
                    );

                    Console.WriteLine(updatedBudget.ToString());

                    if (updatedBudget is null) throw new Exception("UpdateBudget nulo");

                    var response = await budgetRepository.UpdateBudget(id, updatedBudget);

                    var existingSlabs = request.slabs.ToList();

                    foreach (var slabRequest in existingSlabs)
                    {
                        var existingSlab = await budgetSlabsRepository.GetBudgetSlabById(slabRequest.Id);

                        if (existingSlab != null)
                        {

                            await budgetSlabsRepository.UpdateBudgetSlab(
                                slabRequest.Id,
                                slabRequest
                            );
                        }
                        else
                        {
                            var newSlab = new BudgetSlab(
                                slabRequest.SlabId,
                                id,
                                slabRequest.SlabsNumber,
                                slabRequest.Overload,
                                slabRequest.Length,
                                slabRequest.Width
                            );
                            await budgetSlabsRepository.AddBudgetSlabAsync(newSlab);
                        }
                    }

                    var slabIdsFromRequest = request
                        .slabs.Select(s => s.SlabId)
                        .ToHashSet();

                    var slabsToRemove = existingSlabs
                        .Where(s => !slabIdsFromRequest.Contains(s.SlabId))
                        .ToList();

                    foreach (var slab in slabsToRemove)
                    {
                        await budgetSlabsRepository.DeleteBudgetSlab(slab.Id);
                    }


                    return Results.Ok(response);
                }
                catch (KeyNotFoundException e)
                {
                    return Results.NotFound(e.Message);
                }
                catch (DbUpdateException e)
                {
                    return Results.Problem("Error saving changes: " + e.Message);
                }
                catch (Exception e)
                {
                    return Results.Problem("An unexpected error occurred: " + e.Message);
                }
            }
        );

        // DELETE
        budgetEndpoints.MapDelete(
            "{id:int}",
            async ([FromRoute] int id, [FromServices] BudgetRepository budgetRepository) =>
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
            }
        );
    }
}
