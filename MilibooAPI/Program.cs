using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MilibooAPI.Models;
using MilibooAPI.Models.DataManager;
using MilibooAPI.Models.EntityFramework;
using MilibooAPI.Models.Repository;
using System.Text;
using System.Text.Json.Serialization;
using static MilibooAPI.Models.EntityFramework.MilibooDBContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add CORS service with policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVueApp", policy =>
    {
        policy.WithOrigins("http://localhost:5173", "http://localhost:8978", "http://localhost:8948", "http://localhost:8951", "http://51.83.36.122:8948", "http://51.83.36.122:8953")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); 
    });
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    });

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"])),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization(config =>
{
    config.AddPolicy(Policies.Authorized, Policies.Logged());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add repository services
builder.Services.AddScoped<IDataRepository<Professionnel>, ProfessionnelManager>();
builder.Services.AddScoped<IDataRepository<EstDeCouleur>, EstDeCouleurManager>();
builder.Services.AddScoped<IDataRepository<AvisClient>, AvisClientManager>();
builder.Services.AddScoped<IDataRepositoryCategorie, CategorieManager>();
builder.Services.AddScoped<IDataRepository<TypeProduit>, TypeProduitManager>();
builder.Services.AddScoped<IDataRepository<Produit>, ProduitManager>();
builder.Services.AddScoped<IDataRepositoryClient, ClientManager>();
builder.Services.AddScoped<IDataRepositoryCommande, CommandeManager>();
builder.Services.AddScoped<IDataRepositoryEstDeCouleur, EstDeCouleurManager>();
builder.Services.AddScoped<IDataRepositoryAvisClient, AvisClientManager>();
builder.Services.AddScoped<IDataRepository<TypePaiement>, TypePaiementManager>();
builder.Services.AddScoped<IDataRepositoryEstDans, EstDansManager>();
builder.Services.AddScoped<IDataRepository<CarteBancaire>, CarteBancaireManager>();
builder.Services.AddScoped<IDataRepository<LivraisonDomicile>, LivraisonDomicileManager>();
builder.Services.AddScoped<IDataRepository<Adresse>, AdresseManager>();


// Configure DB context
builder.Services.AddDbContext<MilibooDBContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("MilibooConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use CORS policy - place this before other middleware
app.UseCors("AllowVueApp");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();