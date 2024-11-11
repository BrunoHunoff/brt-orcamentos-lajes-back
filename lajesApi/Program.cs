using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", builder => 
        builder
            .WithOrigins("http://localhost:5173") // Substitua pelos domínios específicos permitidos
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});


// Configuração do Swagger com autenticação
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira o token JWT no campo abaixo.\nExemplo: Bearer {seu token}"
    });

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

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();


// Registro de repositórios e serviços
builder.Services.AddScoped<SlabsRepository>();
builder.Services.AddScoped<CostumersRepository>();
builder.Services.AddScoped<BudgetRepository>();
builder.Services.AddScoped<BudgetSlabsRepository>();
builder.Services.AddScoped<UsersRepository>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<RefreshTokensRepository>();
builder.Services.AddScoped<PasswordHasher>();

// Configuração de autenticação e JWT
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

    options.TokenValidationParameters.ClockSkew = TimeSpan.FromSeconds(0);

    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = ctx =>
        {
            ctx.Request.Cookies.TryGetValue("userToken", out var accessToken);
            if (!string.IsNullOrEmpty(accessToken)) ctx.Token = accessToken;

            return Task.CompletedTask;
        }
    };
});

// Adiciona autorização para o uso do `UseAuthorization()`
builder.Services.AddAuthorization();

var app = builder.Build();

// Aplicação do CORS
app.UseCors("AllowSpecificOrigins");

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
