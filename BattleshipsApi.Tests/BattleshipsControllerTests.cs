public class BattleshipsControllerTests
{
    private Board _board = new Board();
    private List<Battleship> _battleshipsInBoard = new List<Battleship>();
    private BattleshipController _controllerUnderTest = new BattleshipController();

    [SetUp]
    public void Setup()
    {
        _board.BoardLength = 10;
        _battleshipsInBoard.Clear();
    }

    [Test]
    public void ShipAlreadyExists_Detects_New_Ship_Where_One_Exists()
    {
        Battleship existingShip = new Battleship(){
            Id = 1,
            xLoc = 2,
            yLoc = 2,
            ShipLength = 4            
        };

        _battleshipsInBoard.Add(existingShip);

        Battleship newShip = new Battleship(){
            Id = 2,
            xLoc = 2,
            yLoc = 2,
            ShipLength = 4            
        };

        bool result = _controllerUnderTest.ShipAlreadyExists(newShip, _battleshipsInBoard);
        Assert.IsTrue(result);
    }

    [Test]
    public void ShipAlreadyExists_Vertical_New_Ship_Overlaps_Existing_Ship()
    {
        Battleship existingShip = new Battleship(){
            Id = 1,
            xLoc = 2,
            yLoc = 2,
            ShipLength = 4            
        };

        _battleshipsInBoard.Add(existingShip);

         // Does not overlap in xLoc and yLoc but because the ship is vertical and ship length is long it will still intersect
        Battleship newShip = new Battleship(){
            Id = 2,
            IsVertical = true,
            xLoc = 2,
            yLoc = 0,
            ShipLength = 4           
        };

        bool result = _controllerUnderTest.ShipAlreadyExists(newShip, _battleshipsInBoard);
        Assert.IsTrue(result);
    }

    [Test]
    public void ShipAlreadyExists_Short_Vertical_New_Ship_Does_Not_Overlaps_Existing_Ship()
    {
        Battleship existingShip = new Battleship(){
            Id = 1,
            xLoc = 2,
            yLoc = 2,
            ShipLength = 4            
        };

        _battleshipsInBoard.Add(existingShip);
        
         // Does not overlap in xLoc and yLoc and ship is short enough to not intersect
        Battleship newShip = new Battleship(){
            Id = 2,
            IsVertical = true,
            xLoc = 0,
            yLoc = 2,
            ShipLength = 2           
        };

        bool result = _controllerUnderTest.ShipAlreadyExists(newShip, _battleshipsInBoard);
        Assert.IsFalse(result);
    }    
}