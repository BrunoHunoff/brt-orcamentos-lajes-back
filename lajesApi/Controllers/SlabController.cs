using Microsoft.AspNetCore.Mvc;

public static class SlabController
{

    public static void AddSlabEndpoints(this WebApplication app)
    {
        var slabEndpoints = app.MapGroup("slabs");

        //GET ALL
        slabEndpoints.MapGet("", async ([FromServices] slabRepository slabRepository) =>
            await slabRepository.GetAllslabs());

        //GET ID
        slabEndpoints.MapGet("{id:guid}", async ([FromRoute] Guid id, slabRepository slabRepository) =>
        {
            var slab = await slabRepository.GetslabById(id);

            if (slab is null) return Results.NotFound();

            return Results.Ok(slab);
        });

        //POST
        slabEndpoints.MapPost("", async (AddslabsRequest request, [FromServices] slabRepository slabRepository) =>
        {
            var novaslab = new Slab(request.Name, request.Price, request.Weight);

            await slabRepository.AddslabAsync(novaslab);
            return Results.Ok();
        });

        //PUT
        slabEndpoints.MapPut("{id:guid}", async ([FromRoute] Guid id, [FromBody] Slab novaslab, [FromServices] slabRepository slabRepository) =>
        {
            try
            {
                var slab = await slabRepository.Updateslab(id, novaslab);
                return Results.Ok(slab);
            }
            catch (KeyNotFoundException e)
            {
                return Results.NotFound(e.Message);
            } catch (Exception e) {
                return Results.Problem("An unexpected error occurred: " + e.Message);
            }

        });


        //DELETE
        slabEndpoints.MapDelete("{id:Guid}", async ([FromRoute] Guid id, [FromServices] slabRepository slabRepository) => {
            try 
            {
                await slabRepository.Deleteslab(id);
                return Results.NoContent();
            } catch (KeyNotFoundException e) {
                return Results.NotFound(e.Message);
            } catch (Exception e) {
                return Results.Problem("An unexpected error occurred: " + e.Message);
            }
        });
    }


}