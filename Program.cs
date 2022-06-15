//per il context aggiungiamo:
using Microsoft.EntityFrameworkCore;
using csharp_webapi.Models;
//end per il context aggiungiamo:


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//per il context aggiungiamo
builder.Services.AddDbContext<SingolaAttivitaContext>(opt =>
    opt.UseInMemoryDatabase("ListaAttivita"));
//end per il context aggiungiamo:



var app = builder.Build();


if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
