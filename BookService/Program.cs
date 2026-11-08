using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// NY: tillad kald fra frontenden
builder.Services.AddCors(o => o.AddDefaultPolicy(p =>
p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

var app = builder.Build();
// NY: slå reglen til, før dine MapGet/MapPost
app.UseCors();
app.MapGet("/books/{id}", (int id) =>
{
    //læser filen
    var text = File.ReadAllText("../Data/books.json");
    //laver den om til en liste
    var books = JsonSerializer.Deserialize<List<Book>>(text);
    //find bogen med FistOrDefault
    var book = books.FirstOrDefault(b => b.id == id);
    //tjekker om bogen blev fundet, og svarer med statuskode
    if (book == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(book);
    
});

app.Run();
record Book(int id, string title);




// NY: slå reglen til, før dine MapGet/MapPost
