using Microsoft.AspNetCore.Mvc;

public static class CostumerController
{

    public static void AddCostumerEndpoints(this WebApplication app)
    {
        var costumerEndpoints = app.MapGroup("costumers");

        //GET ALL
        costumerEndpoints.MapGet("", async ([FromServices] CostumersRepository costumerRepository) =>
            await costumerRepository.GetAllCostumers());

        //GET ID
        costumerEndpoints.MapGet("{id:id}", async ([FromRoute] int id, CostumersRepository costumerRepository) =>
        {
            var costumer = await costumerRepository.GetCostumerById(id);

            if (costumer is null) return Results.NotFound();

            return Results.Ok(costumer);
        });

        //POST
        costumerEndpoints.MapPost("", async (AddCostumerRequest request, [FromServices] CostumersRepository costumerRepository) =>
        {
            var newCostumer = new Costumer(request.Name, request.Pj, request.CnpjCpf, request.Cep, request.City, request.State, request.Address, request.AddressNumber, request.Email, request.PhoneNumber);

            await costumerRepository.AddCostumerAsync(newCostumer);
            return Results.Ok(newCostumer);
        });

        //PUT
        costumerEndpoints.MapPut("{id:id}", async ([FromRoute] int id, [FromBody] AddCostumerRequest newCostumer, [FromServices] CostumersRepository costumerRepository) =>
        {
            try
            {
                var costumer = await costumerRepository.Updatecostumer(id, newCostumer);
                return Results.Ok(costumer);
            }
            catch (KeyNotFoundException e)
            {
                return Results.NotFound(e.Message);
            } catch (Exception e) {
                return Results.Problem("An unexpected error occurred: " + e.Message);
            }

        });


        //DELETE
        costumerEndpoints.MapDelete("{id:id}", async ([FromRoute] int id, [FromServices] CostumersRepository costumerRepository) => {
            try 
            {
                await costumerRepository.Deletecostumer(id);
                return Results.NoContent();
            } catch (KeyNotFoundException e) {
                return Results.NotFound(e.Message);
            } catch (Exception e) {
                return Results.Problem("An unexpected error occurred: " + e.Message);
            }
        });
    }


}