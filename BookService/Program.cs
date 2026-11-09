using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(o => o.AddDefaultPolicy(p =>
p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
var app = builder.Build();
app.UseCors();

app.MapGet("/books", () =>
{
    var text = File.ReadAllText("../Data/books.json");
    var books = JsonSerializer.Deserialize<List<Book>>(text);
    return Results.Ok(books);
});

app.MapGet("/books/{id}", (int id) =>
{
    var text = File.ReadAllText("../Data/books.json");
    var books = JsonSerializer.Deserialize<List<Book>>(text);
    var book = books.FirstOrDefault(b => b.id == id);

    if (book == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(book);
    
});
app.Run();
record Book(int id, string title);