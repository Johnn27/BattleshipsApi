using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<BattleshipDb>(opt => opt.UseInMemoryDatabase("BattleshipApiDb"));
builder.Services.AddDbContext<BoardDb>(opt => opt.UseInMemoryDatabase("BattleshipApiDb"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();
BattleshipController battleshipController = new BattleshipController();

if(app.Environment.IsDevelopment()){
    app.UseSwagger();
    app.UseSwaggerUI(c => 
    {
        c.RoutePrefix = "swagger"; //default page to swagger
    });
}

app.MapPost("/CreateBoard", async (BoardDb db) =>
{
    Board board = new Board();
    db.Boards.Add(board);
    await db.SaveChangesAsync();

    return Results.Created($"Created board of length {board.BoardLength}", board);
});

app.MapGet("/AllBattleships", async (BattleshipDb db) =>
    await db.Battleships.ToListAsync());

//Places battleship, check if location is valid, returns 200 Success created if battleship is placed
// else returns Bad request if invalid placement
app.MapPost("/PlaceBattleship", async ( int xLocation, int yLocation, int shipLength, bool isVertical, BattleshipDb db, BoardDb boardDb) =>
{
    if(shipLength <= 0){
        return Results.BadRequest("Please enter a ship length greater than 0");
    }

    //Create a board if not exists
    if(boardDb.Boards.Count() == 0){
        boardDb.Boards.Add(new Board());
        boardDb.SaveChanges();
    }

    if (await boardDb.Boards.FirstAsync() is Board board)
    {
        Battleship ship = new Battleship(){
            xLoc = xLocation, yLoc = yLocation, IsVertical = isVertical, ShipLength = shipLength, BoardId = board.Id
        };

        if(battleshipController.ShipOutOfRange(ship, board)){
            return Results.BadRequest("Ship location is out of range, please try again");
        }
        List<Battleship> battleShipsOnBoard = db.Battleships.Where(i => board.Id == i.BoardId).ToList();
        if(battleshipController.ShipAlreadyExists(ship, battleShipsOnBoard)){
            return Results.BadRequest("Existing ship already exists, please try again");
        }   

        db.Battleships.Add(ship);        
        db.SaveChanges();

        return Results.Created($"Battleship has been placed", ship);
    }
    return Results.NotFound();
});

// Fires at battleship given the location, checks if location is valid
app.MapPost("/FireAtBattleship", async (int xLoc, int yLoc, BoardDb boardDb, BattleshipDb db) =>
{    
    if(battleshipController.LocationOutOfRange(xLoc, yLoc)){
        return Results.BadRequest("Invalid Location, please enter a non-zero location");
    }
    if (await boardDb.Boards.FirstAsync() is Board board)
    {
        List<Battleship> battleShipsOnBoard = db.Battleships.Where(i => board.Id == i.BoardId).ToList();
        if(battleshipController.CoordinateContainsShip(xLoc, yLoc, battleShipsOnBoard))
        {
            //TODO: Confirm with client battleship is removed from board after hit
            return Results.Ok("Ship has been hit in current location!");
        }
        
    }

    return Results.NotFound("No ship found in location.");
});

// Delete all boards and battleships
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

//Delete all battleships
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

app.Run();