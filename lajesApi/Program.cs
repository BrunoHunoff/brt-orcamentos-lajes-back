using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configuração de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Configuração do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Definição do esquema de segurança
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira o token JWT no campo abaixo.\nExemplo: Bearer {seu token}"
    });

    // Requisito de segurança global para todos os endpoints
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// Registro de repositórios e serviços
builder.Services.AddScoped<SlabsRepository>();
builder.Services.AddScoped<CostumersRepository>();
builder.Services.AddScoped<BudgetRepository>();
builder.Services.AddScoped<BudgetSlabsRepository>();
builder.Services.AddScoped<UsersRepository>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<AppDbContext>();
builder.Services.AddScoped<PasswordHasher>();

// Configuração de autenticação e JWT antes de `builder.Build()`
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

// Adiciona autorização para o uso do `UseAuthorization()`
builder.Services.AddAuthorization();

var app = builder.Build();

// Aplicação do CORS
app.UseCors("AllowAllOrigins");

// Configuração do Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middlewares para HTTPS, autenticação e autorização
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();


// Adicionando endpoints
app.AddSlabEndpoints();
app.AddBudgetEndpoints();
app.AddCostumerEndpoints();
app.AddUsersEndpoints();
app.AddAuthEndpoints();

app.Run();
