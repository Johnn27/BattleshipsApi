using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TodoDb>(opt => opt.UseInMemoryDatabase("BattleshipApiDb"));
builder.Services.AddDbContext<BattleshipDb>(opt => opt.UseInMemoryDatabase("BattleshipApiDb"));
builder.Services.AddDbContext<BoardDb>(opt => opt.UseInMemoryDatabase("BattleshipApiDb"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();

if(app.Environment.IsDevelopment()){
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/battleships", async (BattleshipDb db) =>
    await db.Battleships.ToListAsync());


app.MapPost("/battleships", async (Battleship ship, BattleshipDb db) =>
{
    db.Battleships.Add(ship);
    await db.SaveChangesAsync();

    return Results.Created($"/todoitems/{ship.Id}", ship);
});

app.MapDelete("/battleships", async  (BattleshipDb db) =>
{
    if (await db.Battleships.ToListAsync() is List<Battleship> ships)
    {
        foreach(Battleship ship in ships){
        db.Battleships.Remove(ship);
        await db.SaveChangesAsync();
        }
        return Results.Ok();
    }
    return Results.NotFound();
});

app.MapGet("/todoitems", async (TodoDb db) =>
    await db.Todos.ToListAsync());

app.MapGet("/todoitems/complete", async (TodoDb db) =>
    await db.Todos.Where(t => t.IsComplete).ToListAsync());

app.MapGet("/todoitems/{id}", async (int id, TodoDb db) =>
    await db.Todos.FindAsync(id)
        is Todo todo
            ? Results.Ok(todo)
            : Results.NotFound());

app.MapPost("/todoitems", async (Todo todo, TodoDb db) =>
{
    db.Todos.Add(todo);
    await db.SaveChangesAsync();

    return Results.Created($"/todoitems/{todo.Id}", todo);
});

app.MapPut("/todoitems/{id}", async (int id, Todo inputTodo, TodoDb db) =>
{
    var todo = await db.Todos.FindAsync(id);

    if (todo is null) return Results.NotFound();

    todo.Name = inputTodo.Name;
    todo.IsComplete = inputTodo.IsComplete;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/todoitems/{id}", async (int id, TodoDb db) =>
{
    if (await db.Todos.FindAsync(id) is Todo todo)
    {
        db.Todos.Remove(todo);
        await db.SaveChangesAsync();
        return Results.Ok(todo);
    }
    return Results.NotFound();
});

app.Run();