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
BattleshipController battleshipController = new BattleshipController();

if(app.Environment.IsDevelopment()){
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/CreateBoard", async (Board board, BoardDb db) =>
{
    db.Boards.Add(board);
    await db.SaveChangesAsync();

    return Results.Created($"/board/{board.Id}", board);
});

app.MapGet("/AllBattleships", async (BattleshipDb db) =>
    await db.Battleships.ToListAsync());

app.MapPost("/NewBattleship", async (Battleship ship, BattleshipDb db, BoardDb boardDb) =>
{
    if (await boardDb.Boards.FirstAsync() is Board board)
    {
        if(battleshipController.ShipOutOfRange(ship, board)){
            return Results.BadRequest("Ship location is out of range, please try again");
        }
        if(battleshipController.ShipAlreadyExists(ship, board.BattleShips)){
            return Results.BadRequest("Existing ship already exists, please try again");
        }   
        db.Battleships.Add(ship);
        board.BattleShips.Add(ship);
        await boardDb.SaveChangesAsync();
        await db.SaveChangesAsync();

        return Results.Created($"/todoitems/{ship.Id}", ship);
    }
    return Results.NotFound();
});

app.MapPost("/FireAtBattleship", async (Board board, BoardDb db) =>
{
    db.Boards.Add(board);
    await db.SaveChangesAsync();

    return Results.Created($"/board/{board.Id}", board);
});

app.MapDelete("/ClearBoardsAndBattleships", async  (BattleshipDb db, BoardDb boardDb) =>
{
    if (await db.Battleships.ToListAsync() is List<Battleship> ships)
    {
        foreach(Battleship ship in ships){
        db.Battleships.Remove(ship);
        await db.SaveChangesAsync();
        }        
    }
    if (await boardDb.Boards.ToListAsync() is List<Board> boards)
    {
        foreach(Board board in boards){
        boardDb.Boards.Remove(board);
        await db.SaveChangesAsync();
        }
        return Results.Ok();
    }    
    return Results.NotFound();
});

app.MapDelete("/ClearBattleships", async  (BattleshipDb db) =>
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

app.MapGet("/todoitems/{id}", async (int id, TodoDb db) =>
    await db.Todos.FindAsync(id)
        is Todo todo
            ? Results.Ok(todo)
            : Results.NotFound());


app.MapPut("/todoitems/{id}", async (int id, Todo inputTodo, TodoDb db) =>
{
    var todo = await db.Todos.FindAsync(id);

    if (todo is null) return Results.NotFound();

    todo.Name = inputTodo.Name;
    todo.IsComplete = inputTodo.IsComplete;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.Run();