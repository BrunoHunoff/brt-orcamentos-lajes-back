using Microsoft.AspNetCore.Mvc;

public static class LajesController {
    
    public static void AddLajesEndpoints (this WebApplication app) {
        var lajesEndpoint = app.MapGroup ("lajes");

        //GET ALL
        lajesEndpoint.MapGet("", async ([FromServices] LajeRepository lajeRepository) =>
            await lajeRepository.GetAllLajes());

        //GET ID
        lajesEndpoint.MapGet("{id:guid}", async ([FromRoute] Guid id, LajeRepository lajeRepository) => {
            var laje = await lajeRepository.GetLajeById(id);

            if (laje is null) return Results.NotFound();

            return Results.Ok(laje);
        });

        //POST
        lajesEndpoint.MapPost("", async (AddLajesRequest request, [FromServices] LajeRepository lajeRepository) => {
            var novaLaje = new Laje(request.Name, request.Price);

            await lajeRepository.AddLajeAsync(novaLaje);
            return Results.Ok();
        });

        //PUT
        lajesEndpoint.MapPut("{id:guid}", async ([FromRoute] Guid id, [FromBody] Laje novaLaje, [FromServices] LajeRepository lajeRepository) => {

        var laje = await lajeRepository.UpdateLaje(id, novaLaje);

        if (laje is null) return Results.NotFound("Laje with the specified ID not found.");

        return Results.Ok(laje);
});


        //DELETE
    }


}