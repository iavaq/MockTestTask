using Microsoft.EntityFrameworkCore;
using CSVQueries.Models;
using CSVQueries.Services;

var builder = WebApplication.CreateBuilder(args);
string connectionString = builder.Configuration.GetConnectionString("CSVQueryAPI");

// Add services to the container.
builder.Services.AddScoped<ICSVQueryService, CSVQueryService>();
builder.Services.AddControllers();
builder.Services.AddDbContext<ModelsContext>(options => options.UseNpgsql(connectionString));

//builder.Services.AddDbContext<ModelsContext>(option => option.UseInMemoryDatabase("CSVQueryDB"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddSwaggerGenNewtonsoftSupport();

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
