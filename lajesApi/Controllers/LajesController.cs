using Microsoft.AspNetCore.Mvc;

public static class LajesController
{

    public static void AddLajesEndpoints(this WebApplication app)
    {
        var lajesEndpoint = app.MapGroup("lajes");

        //GET ALL
        lajesEndpoint.MapGet("", async ([FromServices] LajeRepository lajeRepository) =>
            await lajeRepository.GetAllLajes());

        //GET ID
        lajesEndpoint.MapGet("{id:guid}", async ([FromRoute] Guid id, LajeRepository lajeRepository) =>
        {
            var laje = await lajeRepository.GetLajeById(id);

            if (laje is null) return Results.NotFound();

            return Results.Ok(laje);
        });

        //POST
        lajesEndpoint.MapPost("", async (AddLajesRequest request, [FromServices] LajeRepository lajeRepository) =>
        {
            var novaLaje = new Laje(request.Name, request.Price, request.Weight);

            await lajeRepository.AddLajeAsync(novaLaje);
            return Results.Ok();
        });

        //PUT
        lajesEndpoint.MapPut("{id:guid}", async ([FromRoute] Guid id, [FromBody] Laje novaLaje, [FromServices] LajeRepository lajeRepository) =>
        {
            try
            {
                var laje = await lajeRepository.UpdateLaje(id, novaLaje);
                return Results.Ok(laje);
            }
            catch (KeyNotFoundException e)
            {
                return Results.NotFound(e.Message);
            } catch (Exception e) {
                return Results.Problem("An unexpected error occurred: " + e.Message);
            }

        });


        //DELETE
        lajesEndpoint.MapDelete("{id:Guid}", async ([FromRoute] Guid id, [FromServices] LajeRepository lajeRepository) => {
            try 
            {
                await lajeRepository.DeleteLaje(id);
                return Results.NoContent();
            } catch (KeyNotFoundException e) {
                return Results.NotFound(e.Message);
            } catch (Exception e) {
                return Results.Problem("An unexpected error occurred: " + e.Message);
            }
        });
    }


}