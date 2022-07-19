using Microsoft.EntityFrameworkCore;
using CSVQueries.Models;
using CSVQueries.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ICSVQueryService, CSVQueryService>();
builder.Services.AddControllers();
//builder.Services.AddDbContext<ModelsContext>(option => option.UseInMemoryDatabase("CSVQueryDB"));

var ConnectionString = Environment.GetEnvironmentVariable("CSVQueryAPI");
builder.Services.AddDbContext<ModelsContext>(options => options.UseNpgsql(ConnectionString));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
