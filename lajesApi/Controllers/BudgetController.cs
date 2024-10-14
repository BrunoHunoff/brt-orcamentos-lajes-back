using Microsoft.AspNetCore.Mvc;

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
        [FromBody] UpdateBudgetRequest newBudgetRequest,
        [FromServices] BudgetRepository budgetRepository,
        [FromServices] BudgetSlabsRepository budgetSlabsRepository,
        [FromServices] FreightRepository freightRepository
    ) =>
    {
        try
        {

            var existingBudget = await budgetRepository.GetBudgetById(id);
            if (existingBudget is null)
            {
                return Results.NotFound("Budget not found");
            }

            // Atualiza os dados do orçamento
            existingBudget.UpdateBudget(
                newBudgetRequest.costumerId,
                newBudgetRequest.costumerName,
                newBudgetRequest.footage,
                newBudgetRequest.value,
                newBudgetRequest.city,
                newBudgetRequest.state,
                await freightRepository.GetFreightById(newBudgetRequest.freightId)
            );

            // Obter as BudgetSlabs existentes
            var existingSlabs = existingBudget.Slabs.ToList(); // Convertemos para uma lista para manipulação

            foreach (var slabRequest in newBudgetRequest.slabs)
            {
                var existingSlab = existingSlabs.FirstOrDefault(s =>
                    s.SlabId == slabRequest.slabId
                );

                if (existingSlab != null)
                {
                    // Atualiza a BudgetSlab existente
                    existingSlab.UpdateBudgetSlab(
                        slabRequest.slabId,
                        slabRequest.slabsNumber,
                        slabRequest.overload,
                        slabRequest.length,
                        slabRequest.width
                    );
                    await budgetSlabsRepository.UpdateBudgetSlab(existingSlab.Id, existingSlab);
                }
                else
                {
                    // Cria uma nova BudgetSlab
                    var newSlab = new BudgetSlab(
                        slabRequest.slabId,
                        id,
                        slabRequest.slabsNumber,
                        slabRequest.overload,
                        slabRequest.length,
                        slabRequest.width
                    );
                    await budgetSlabsRepository.AddBudgetSlabAsync(newSlab);
                }
            }

            var slabIdsFromRequest = newBudgetRequest
                .slabs.Select(s => s.slabId)
                .ToHashSet();
            var slabsToRemove = existingSlabs
                .Where(s => !slabIdsFromRequest.Contains(s.SlabId))
                .ToList();

            foreach (var slab in slabsToRemove)
            {
                await budgetSlabsRepository.DeleteBudgetSlab(slab.Id);
            }

            // Salva as alterações
            await budgetRepository.UpdateBudget(id, existingBudget);

            return Results.Ok(existingBudget);
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
