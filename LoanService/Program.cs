using System.Net;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// NY: tillad kald fra frontenden
builder.Services.AddCors(o => o.AddDefaultPolicy(p =>
    p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));


var app = builder.Build();
// NY: slå reglen til, før dine MapGet/MapPost
app.UseCors();
app.MapPost("/loans", async (int bookId, int memberId) =>
{   
    var client = new HttpClient();
    try
    {   
        var bookAnswer = await client.GetAsync($"http://localhost:5001/books/{bookId}");
        if (bookAnswer.StatusCode == HttpStatusCode.NotFound) 
        {
            return Results.NotFound("Bogen findes ikke sorry buddy!"); 
        }
    }
    catch (HttpRequestException)
    {
        return Results.Problem("BookService er nede", statusCode: 503); 
    }

    try
    {
        var memberAnswer = await client.GetAsync($"http://localhost:5002/members/{memberId}");
        if (memberAnswer.StatusCode == HttpStatusCode.NotFound)
        {
            return Results.NotFound("Medlem findes ikke sorry buddy!");
        }
    }
    catch (HttpRequestException)
    {
        return Results.Problem("MemberService er nede", statusCode: 503); 
    }

var text = File.ReadAllText("../Data/loans.json");
var loans = JsonSerializer.Deserialize<List<Loan>>(text);

var amount = loans.Count;
loans.Add(new Loan(id:amount+1, bookId, memberId));


var newText = JsonSerializer.Serialize(loans);
File.WriteAllText("../Data/loans.json", newText);

return Results.Ok("Lån er oprettet"); 
});
app.Run();
record Loan(int id, int bookId, int memberId);
