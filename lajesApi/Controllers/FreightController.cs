using Microsoft.AspNetCore.Mvc;

public static class FreightController
{

    public static void AddFreightEndpoints(this WebApplication app)
    {
        var freightEndpoints = app.MapGroup("freights");

        //GET ALL
        freightEndpoints.MapGet("", async ([FromServices] FreightRepository freightRepository) =>
            await freightRepository.GetAllFreights());

        //GET ID
        freightEndpoints.MapGet("{id:id}", async ([FromRoute] int id, FreightRepository freightRepository) =>
        {
            var freight = await freightRepository.GetFreightById(id);

            if (freight is null) return Results.NotFound();

            return Results.Ok(freight);
        });

        //POST
        freightEndpoints.MapPost("", async (AddFreightRequest request, [FromServices] FreightRepository freightRepository) =>
        {
            var newFreight = new Freight(
                request.city,
                request.state, 
                request.price
            );

            await freightRepository.AddFreightAsync(newFreight);
            return Results.Ok(newFreight);
        });

        //PUT
        freightEndpoints.MapPut("{id:id}", async ([FromRoute] int id, [FromBody] AddFreightRequest newFreight, [FromServices] FreightRepository freightRepository) =>
        {
            try
            {
                var freight = await freightRepository.UpdateFreight(id, newFreight);
                return Results.Ok(freight);
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
        freightEndpoints.MapDelete("{id:id}", async ([FromRoute] int id, [FromServices] FreightRepository freightRepository) =>
        {
            try
            {
                await freightRepository.DeleteFreight(id);
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