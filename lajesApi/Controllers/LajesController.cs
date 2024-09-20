using Microsoft.AspNetCore.Mvc;

public static class LajesController {
    
    public static void AddLajesEndpoints (this WebApplication app) {
        var lajesEndpoint = app.MapGroup ("lajes");

        //GET ALL
        lajesEndpoint.MapGet("", async ([FromServices] LajeRepository lajeRepository) =>
            await lajeRepository.GetAllLajes());

        //GET ID
        lajesEndpoint.MapGet("{id:Guid}", async ([FromRoute] Guid id, LajeRepository lajeRepository) => {
            var laje = await lajeRepository.GetLajeById(id);

            if (laje == null) return Results.NotFound();

            return Results.Ok(laje);
        });

        //POST
        lajesEndpoint.MapPost("", async (AddLajesRequest request, [FromServices] LajeRepository lajeRepository) => {
            var novaLaje = new Laje(request.Name, request.Price);

            var lajeExiste = await lajeRepository.LajeExists(novaLaje.Name);

            if (lajeExiste) return Results.Conflict("Laje.name already exists");

            await lajeRepository.AddLajeAsync(novaLaje);
            return Results.Ok();
        });

        //PUT

        //DELETE
    }


}