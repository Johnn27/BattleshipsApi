public class Board
{
    private int boardLength = 10;

    public int BoardLength { get => boardLength; set => boardLength = value; }

    public List<Battleship>? BattleShips { get; set;}
}