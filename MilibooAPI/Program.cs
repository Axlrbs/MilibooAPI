using Microsoft.EntityFrameworkCore;
using MilibooAPI.Models.DataManager;
using MilibooAPI.Models.EntityFramework;
using MilibooAPI.Models.Repository;
using static MilibooAPI.Models.EntityFramework.MilibooDBContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IDataRepository<Client>, ClientManager>();
builder.Services.AddScoped<IDataRepository<Professionnel>, ProfessionnelManager>();
builder.Services.AddScoped<IDataRepository<EstDeCouleur>, EstDeCouleurManager>();
builder.Services.AddScoped<IDataRepository<AvisClient>, AvisClientManager>();


builder.Services.AddDbContext<MilibooDBContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("MilibooConnection")));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();