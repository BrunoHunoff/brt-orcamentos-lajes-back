using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

public static class SlabController
{
    [Authorize]
    public static void AddSlabEndpoints(this WebApplication app)
    {
        var slabEndpoints = app.MapGroup("slabs").RequireAuthorization();

        //GET ALL
        slabEndpoints.MapGet("", async ([FromServices] SlabsRepository slabRepository) =>
            await slabRepository.GetAllslabs());

        //GET ID
        slabEndpoints.MapGet("{id:int}", async ([FromRoute] int id,[FromServices] SlabsRepository slabRepository) =>
        {
            var slab = await slabRepository.GetslabById(id);

            if (slab is null) return Results.NotFound();

            return Results.Ok(slab);
        });

        //POST
        slabEndpoints.MapPost("", async (AddSlabsRequest request, [FromServices] SlabsRepository slabRepository) =>
        {
            var novaslab = new Slab(request.Name, request.Price, request.Weight);

            await slabRepository.AddslabAsync(novaslab);
            return Results.Ok();
        });

        //PUT
        slabEndpoints.MapPut("{id:int}", async ([FromRoute] int id, [FromBody] AddSlabsRequest novaslab, [FromServices] SlabsRepository slabRepository) =>
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
        slabEndpoints.MapDelete("{id:int}", async ([FromRoute] int id, [FromServices] SlabsRepository slabRepository) => {
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