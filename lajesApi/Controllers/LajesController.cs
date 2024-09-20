using Microsoft.EntityFrameworkCore;

public static class LajesController {
    public static void AddLajesEndpoints (this WebApplication app) {
        var lajesEndpoint = app.MapGroup ("lajes");

        app.MapGet("lajes", () => "Lajes");

        lajesEndpoint.MapPost("", async (AddLajesRequest request, AppDbContext dbContext) => {
            var novaLaje = new Laje(request.Name, request.Price);

            var lajeExiste = await dbContext.Lajes.AnyAsync(Laje => Laje.Name == request.Name);

            if (lajeExiste) return Results.Conflict("Laje.name already exists");

            await dbContext.Lajes.AddAsync(novaLaje);
            await dbContext.SaveChangesAsync();
            return Results.Ok();
        });
    }


}