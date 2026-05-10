using Microsoft.EntityFrameworkCore;
using PZPP_BibliotekaAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<BibliotekaContext>(opt =>
    opt.UseInMemoryDatabase("Biblioteka2"));

var app = builder.Build();

// seed danych
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<BibliotekaContext>();
    Seed.Init(context);
    Console.WriteLine("Seed dzia³a!");
}

app.MapControllers();
app.Run();
