public class Board
{
    public int Id { get; set; }

    private int boardLength = 10;
    private List<Battleship> battleShips = new List<Battleship>();

    public int BoardLength { get => boardLength; set => boardLength = value; }

    public List<Battleship> BattleShips { get => battleShips ; set => battleShips = value; }
}