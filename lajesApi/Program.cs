var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<SlabsRepository>();
builder.Services.AddScoped<CostumersRepository>();
builder.Services.AddScoped<FreightRepository>();
builder.Services.AddScoped<BudgetRepository>();
builder.Services.AddScoped<BudgetSlabsRepository>();

builder.Services.AddScoped<AppDbContext>();




var app = builder.Build();

app.UseCors("AllowAllOrigins");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.AddSlabEndpoints();
app.AddBudgetEndpoints();
app.AddCostumerEndpoints();
app.AddFreightEndpoints();


app.Run();
