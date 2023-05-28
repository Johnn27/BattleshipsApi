public class Board
{
    public int Id { get; set; }

    private int boardLength = 10;

    public int BoardLength { get => boardLength; set => boardLength = value; }

    public List<Battleship> BattleshipIds { get; set; } // One to Many FK relationship 

    public Board(){
        BattleshipIds = new List<Battleship>();
    }
}