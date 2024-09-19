public static class LajesEndpoints {
    public static void AddLajesEndpoints (this WebApplication app) {
        app.MapGet("lajes", () => "Lajes");
    }
}