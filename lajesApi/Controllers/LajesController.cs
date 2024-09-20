using Microsoft.AspNetCore.Mvc;

public static class LajesController {
    
    public static void AddLajesEndpoints (this WebApplication app) {
        var lajesEndpoint = app.MapGroup ("lajes");


        lajesEndpoint.MapGet("", async ([FromServices] LajeRepository lajeRepository) =>
            await lajeRepository.GetAllLajes());


        lajesEndpoint.MapPost("", async (AddLajesRequest request, AppDbContext dbContext,[FromServices] LajeRepository lajeRepository) => {
            var novaLaje = new Laje(request.Name, request.Price);

            var lajeExiste = await lajeRepository.LajeExists(novaLaje.Name);

            if (lajeExiste) return Results.Conflict("Laje.name already exists");

            await lajeRepository.AddLajeAsync(novaLaje);
            return Results.Ok();
        });
    }


}