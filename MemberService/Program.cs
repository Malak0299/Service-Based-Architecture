using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// NY: tillad kald fra frontenden
builder.Services.AddCors(o => o.AddDefaultPolicy(p =>
    p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

var app = builder.Build();
// NY: slå reglen til, før dine MapGet/MapPost
app.UseCors();
app.MapGet("/members/{id}", (int id) =>
{
    var text = File.ReadAllText("../Data/members.json");
    var members = JsonSerializer.Deserialize<List<Member>>(text);
    var member = members.FirstOrDefault(m => m.id == id);
    
    if (member == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(member);
});

app.MapPost("/members",(string name) => 
{
    var text = File.ReadAllText("../Data/members.json");
    var members = JsonSerializer.Deserialize<List<Member>>(text);
    
    var amount = members.Count;
    members.Add(new Member(id:amount+1, name:name));
    
    var newMember = JsonSerializer.Serialize(members);
    File.WriteAllText("../Data/members.json", newMember);
    
    return Results.Ok("Medlem er oprettet");


});

app.Run();
record Member(int id, string name);