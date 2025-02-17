using Prueba.DB;
using Microsoft.EntityFrameworkCore;
using Prueba.Services.Interfaces;
using Prueba.Services;
using Prueba.Repositories.Interfaces;
using Prueba.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddHttpClient();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


builder.Services.AddScoped<ICarServices, CarServices>();
builder.Services.AddScoped<ICountryService, CountryService>();

builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<IContryRepository, CountryRepository>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var meuService = scope.ServiceProvider.GetRequiredService<ICountryService>();
    await meuService.ConsumeApi();
}


if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI(options =>
    options.SwaggerEndpoint("/openapi/v1.json", "my-=api"));
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();