public class Battleship : Ship
{
    public int Id { get; set; }
    public int PlayerId { get; set;}
    public bool IsVertical { get; set; }
    public int xLoc { get; set;}
    public int yLoc { get; set;}

}