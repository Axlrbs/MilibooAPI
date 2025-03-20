using Microsoft.EntityFrameworkCore;
using MilibooAPI.Models.DataManager;
using MilibooAPI.Models.EntityFramework;
using MilibooAPI.Models.Repository;
using static MilibooAPI.Models.EntityFramework.MilibooDBContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add CORS service with policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVueApp", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add repository services
builder.Services.AddScoped<IDataRepository<Client>, ClientManager>();
builder.Services.AddScoped<IDataRepository<Professionnel>, ProfessionnelManager>();
builder.Services.AddScoped<IDataRepository<EstDeCouleur>, EstDeCouleurManager>();
builder.Services.AddScoped<IDataRepository<AvisClient>, AvisClientManager>();

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