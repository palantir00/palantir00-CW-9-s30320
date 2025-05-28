using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Palantir00CW9S30320.Data;
using Palantir00CW9S30320.Services;

var builder = WebApplication.CreateBuilder(args);

// 1) EF Core
builder.Services.AddDbContext<PharmacyContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2) DI
builder.Services.AddScoped<IPrescriptionService, PrescriptionService>();

// 3) Controllers & Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pharmacy API v1");
});

// reszta…
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();